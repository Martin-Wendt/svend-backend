#nullable disable
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SitrepAPI.Entities;
using SitrepAPI.Helpers;
using SitrepAPI.Models;
using SitrepAPI.ResourceParameters;
using SitrepAPI.Services;
using System.Text.Json;

namespace SitrepAPI.Controllers
{
    /// <summary>
    /// API endpoint for cases methods: GET,PUT,POST,HEAD,OPTIONS,PATCH,DELETE
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CasesController : ControllerBase
    {
        private readonly ICaseRepository _caseReposistory;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IPropertyCheckerService _propertyCheckerService;
        private readonly IMapper _mapper;
        private readonly IUserInformationService _userInformationService;
        private readonly IAuthorizationService _authorizationHandler;
        private readonly ICaseImageRepository _caseImageRepository;
        private readonly ICaseLogRepository _caseLogRepository;

        /// <summary>
        /// Contructor of the class
        /// </summary>
        /// <param name="caseReposistory">Case repository service</param>
        /// <param name="propertyMappingService">Part of filter and selected fields operation</param>
        /// <param name="propertyCheckerService">Part of filter and selected fields operation</param>
        /// <param name="mapper">Mapping service that handlels model to model mapping</param>
        /// <param name="userInformationService">Gets user information</param>
        /// <param name="authorizationService">Authorization policy</param>
        /// <param name="caseImageRepository">Image repository service</param>
        /// <param name="caseLogRepository">Log repository service</param>
        /// <exception cref="ArgumentNullException">If Dependency Injection fails.</exception>
        public CasesController(ICaseRepository caseReposistory,
                               IPropertyMappingService propertyMappingService,
                               IPropertyCheckerService propertyCheckerService,
                               IMapper mapper,
                               IUserInformationService userInformationService,
                               IAuthorizationService authorizationService,
                               ICaseImageRepository caseImageRepository,
                               ICaseLogRepository caseLogRepository)
        {
            _propertyCheckerService = propertyCheckerService ?? throw new ArgumentNullException(nameof(propertyCheckerService));
            _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
            _caseReposistory = caseReposistory ?? throw new ArgumentNullException(nameof(caseReposistory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userInformationService = userInformationService ?? throw new ArgumentNullException(nameof(userInformationService));
            _authorizationHandler = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
            _caseImageRepository = caseImageRepository ?? throw new ArgumentNullException(nameof(caseImageRepository));
            _caseLogRepository = caseLogRepository ?? throw new ArgumentNullException(nameof(caseLogRepository));
        }

        /// <summary>
        /// Gets a list of cases
        /// </summary>
        /// <param name="caseResourceParameters">Holds querystring parameters</param>
        /// <returns cref="CasesReturnModel">object with list of cases with HATEOAS links</returns>
        // GET: api/Cases
        [HttpGet(Name = "GetCases")]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CasesReturnModel))]
        public async Task<IActionResult> GetCases([FromQuery] CaseResourceParameters caseResourceParameters)
        {
            if (!_propertyMappingService.ValidOderByMappingExistsFor<CaseDTO, Case>(caseResourceParameters.OrderBy))
            {
                return BadRequest("Not valid orderBy");
            }
            if (!_propertyMappingService.ValidFilterByMappingExistsFor<CaseDTO, Case>(caseResourceParameters.Filter))
            {
                return BadRequest("Not valid filter");
            }
            if (!_propertyCheckerService.TypeHasProperties<CaseDTO>(caseResourceParameters.Fields))
            {
                return BadRequest();
            }

            PagedList<Case> casesFromRepo;

            //only pass if owner of case or has roles manager or operator
            if (User.IsInRole("Operator") || User.IsInRole("Manager"))
            {
                // get all cases
                casesFromRepo = _caseReposistory.GetCases(caseResourceParameters, null);
            }
            else
            {
                // get requesting users cases
                casesFromRepo = _caseReposistory.GetCases(caseResourceParameters, User.Identity.Name);
            }

            var paginationMetaData = new
            {
                totalCount = casesFromRepo.TotalCount,
                pageSize = casesFromRepo.PageSize,
                currentPage = casesFromRepo.CurrentPage,
                totalPages = casesFromRepo.TotalPages
            };

            Response.Headers.Add("X.Pagination", JsonSerializer.Serialize(paginationMetaData));

            var links = CreateLinksForCases(caseResourceParameters, casesFromRepo.HasNext, casesFromRepo.HasPrevious);

            var casesDTO = _mapper.Map<IEnumerable<CaseDTO>>(casesFromRepo);

            if (caseResourceParameters.Fields is null || caseResourceParameters.Fields.ToLower().Contains("createdby"))
            {
                foreach (var @case in casesDTO)
                {
                    @case.CreatedBy = await GetUserInformation(@case.UserId);
                }
            }

            if (caseResourceParameters.Fields is null || caseResourceParameters.Fields.ToLower().Contains("assignee"))
            {
                foreach (var @case in casesDTO)
                {
                    if (@case.AssigneeId != null)
                    {
                        @case.Assignee = await GetUserInformation(@case.AssigneeId);
                    }
                }
            }

            casesDTO = GetImageCount(casesDTO);
            casesDTO = GetLogCount(casesDTO);

            var shapedCases = casesDTO.ShapeData(caseResourceParameters.Fields);

            var shapedCasesWithLinks = shapedCases.Select(@case =>
            {
                //@case. = _caseReposistory.GetImageCount();
                var caseAsDictionary = @case as IDictionary<string, object>;

                //  if case id was not requested this results in a exception - 
                var userLinks = CreateLinksForCase((int)caseAsDictionary["CaseId"], null);
                caseAsDictionary.Add("links", userLinks);
                return caseAsDictionary;
            });



            var linkedCollectionResource = new
            {
                value = shapedCasesWithLinks,
                links
            };
            return Ok(linkedCollectionResource);
        }

        /// <summary>
        /// Get a single case
        /// </summary>
        /// <param name="caseId">Id of case</param>
        /// <param name="fields">Returned fields</param>
        /// <returns cref="CaseDTO">Case with HATEOAS links</returns>
        // GET: api/Cases/5
        [HttpGet("{caseId}", Name = "GetCase")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCase(int caseId, string fields)
        {
            if (!_propertyCheckerService.TypeHasProperties<CaseDTO>(fields))
            {
                return BadRequest();
            }

            var caseFromRepo = _caseReposistory.GetCase(caseId);

            if (caseFromRepo == null)
            {
                return NotFound();
            }
            //only pass if owner of case or has roles manager or operator
            var auth = await _authorizationHandler.AuthorizeAsync(this.User, caseFromRepo, "HasAccess");
            if (!auth.Succeeded)
            {
                return new ForbidResult();
            }

            var links = CreateLinksForCase(caseId, fields);

            var caseDTO = _mapper.Map<CaseDTO>(caseFromRepo);


            if (fields is null || fields.ToLower().Contains("createdby"))
            {
                caseDTO.CreatedBy = await GetUserInformation(caseDTO.UserId);
            }
            if (fields is null || fields.ToLower().Contains("assigneeid"))
            {
                if (!string.IsNullOrWhiteSpace(caseDTO.AssigneeId))
                {
                    caseDTO.Assignee = await GetUserInformation(caseDTO.AssigneeId);
                }
            }


            var linkedResourceToReturn = caseDTO.ShapeData(fields) as IDictionary<string, object>;

            linkedResourceToReturn.Add("links", links);

            return Ok(linkedResourceToReturn);
        }

        /// <summary>
        /// Update a single case
        /// </summary>
        /// <remarks>
        /// Example of PUT request:
        /// ```
        /// {
        ///     "userId": "userid",
        ///     "title": "string",
        ///     "location": "string",
        ///     "description": "string",
        ///     "priorityId": 0,
        ///     "statusId": 0,
        ///     "assigneeId": "string"
        ///}
        /// ```
        /// </remarks>
        /// <param name="caseId">Íd of case</param>
        /// <param name="caseForUpdateDTO">Case object with wanted updates</param>
        /// <returns>No Content</returns>
        // PUT: api/Cases/5
        [HttpPut("{caseId}", Name = "UpdateCase")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json")]
        public async Task<IActionResult> PutCaseAsync(int caseId, CaseForUpdateDTO caseForUpdateDTO)
        {
            var caseFromRepo = _caseReposistory.GetCase(caseId);

            if (caseFromRepo == null)
            {
                return NotFound();
            }

            //only pass if owner of case or has roles manager or operator
            var auth = await _authorizationHandler.AuthorizeAsync(this.User, caseFromRepo, "HasAccess");
            if (!auth.Succeeded)
            {
                return new ForbidResult();
            }

            _mapper.Map(caseForUpdateDTO, caseFromRepo);
            await _caseReposistory.UpdateCaseAsync(caseFromRepo, User.Identity.Name);
            _caseReposistory.Save();

            return NoContent();
        }

        /// <summary>
        /// Create a new case
        /// </summary>
        /// <remarks>
        /// Post api/Cases
        /// ```
        /// Example of case to create:
        /// {
        ///     "title" : "wanted title",
        ///     "description" : "wanted description"
        ///     "location" : "wanted location"
        /// }    
        /// ```
        /// </remarks>
        /// <param name="caseForCreationDTO">Case to create</param>
        /// <returns cref="CaseDTO">Created case</returns>
        // POST: api/Cases
        [HttpPost(Name = "CreateCase")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CaseDTO))]
        [Consumes("application/json")]
        public async Task<ActionResult<Case>> PostCase(CaseForCreationDTO caseForCreationDTO)
        {
            var caseImages = caseForCreationDTO.Images;
            var caseEntity = _mapper.Map<Entities.Case>(caseForCreationDTO);
            caseEntity.UserId = User.Identity.Name;

            _caseReposistory.AddCase(caseEntity);
            _caseReposistory.Save();

            var user = await GetUserInformation(caseEntity.UserId);
            if (caseImages is not null)
            {
                foreach (var image in caseImages)
                {
                    var imageFromRepo = _caseImageRepository.GetCaseImage(image.CaseImageId);

                    if (imageFromRepo == null) return BadRequest();

                    imageFromRepo.CaseId = caseEntity.CaseId;
                    _caseImageRepository.UpdateImageAsync(imageFromRepo, User.Identity.Name);

                    CaseLogDTO log = new()
                    {
                        CaseId = caseEntity.CaseId,
                        CreatedAt = DateTimeOffset.Now,
                        CreatedBy = user.Name,
                        CreatedById = caseEntity.UserId,
                        Message = "Billed tilføjet"

                    };
                    _caseLogRepository.AddCaseLog(_mapper.Map<CaseLog>(log));
                }

                _caseImageRepository.Save();
            }

            var caseToReturn = _mapper.Map<CaseDTO>(_caseReposistory.GetCase(caseEntity.CaseId));

            caseToReturn.CreatedBy = user;

            var links = CreateLinksForCase(caseToReturn.CaseId, null);

            var linkedResourceToReturn = caseToReturn.ShapeData(null) as IDictionary<string, object>;
            linkedResourceToReturn.Add("links", links);

            return CreatedAtAction("GetCase", new { caseId = linkedResourceToReturn["CaseId"] }, linkedResourceToReturn);
        }

        /// <summary>
        /// Patch a user 
        /// </summary>
        /// <remarks>
        /// Exsample of patch request:
        /// ```
        /// [
        ///     {
        ///         "op" : "replace",
        ///         "path" : "/title"
        ///         "value" : "new title"
        ///     },
        ///     {
        ///         "op" : "move",
        ///         "from" : "/description"
        ///         "path" : "/location"
        ///     }
        /// ]
        /// ```
        /// </remarks>
        /// <param name="caseId">Id of case</param>
        /// <param name="jsonPatchDocument">Array of json patch operations</param>
        /// <returns>204 No Content</returns>
        //  PATCH: api/Users/5
        [HttpPatch("{caseId}", Name = "PatchCase")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Consumes("application/json-patch+json")]
        public async Task<ActionResult> PatchCaseAsync(int caseId, JsonPatchDocument<CaseForUpdateDTO> jsonPatchDocument)
        {
            // get and verify that case exsists
            var caseFromRepo = _caseReposistory.GetCase(caseId);
            if (caseFromRepo == null)
            {
                return NotFound();
            }

            //only pass if owner of case or has roles manager or operator
            var auth = await _authorizationHandler.AuthorizeAsync(this.User, caseFromRepo, "HasAccess");
            if (!auth.Succeeded)
            {
                return new ForbidResult();
            }

            var caseToPatch = _mapper.Map<CaseForUpdateDTO>(caseFromRepo);

            jsonPatchDocument.ApplyTo(caseToPatch, ModelState);

            if (!TryValidateModel(caseToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(caseToPatch, caseFromRepo);
            await _caseReposistory.UpdateCaseAsync(caseFromRepo, User.Identity.Name);
            _caseReposistory.Save();

            return NoContent();
        }

        /// <summary>
        /// Delete a case
        /// </summary>
        /// <param name="caseId">Id of case to delete</param>
        /// <returns>204 No Contet</returns>
        [HttpDelete("{caseId}", Name = "DeleteCase")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteCaseAsync(int caseId)
        {
            var caseFromRepo = _caseReposistory.GetCase(caseId);

            if (caseFromRepo == null)
            {
                return NotFound();
            }

            //only pass if owner of case or has roles manager or operator
            var auth = await _authorizationHandler.AuthorizeAsync(this.User, caseFromRepo, "HasAccess");
            if (!auth.Succeeded)
            {
                return new ForbidResult();
            }

            _caseReposistory.Delete(caseFromRepo);

            _caseReposistory.Save();

            return NoContent();
        }

        //  HEAD: api/Users
        /// <summary>
        /// Allowed methods
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpOptions]
        public IActionResult GetUserOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST,DELETE,PUT,PATCH,HEAD");
            return Ok();
        }

        private async Task<UserDTO> GetUserInformation(string userId)
        {
            var userInfo = _mapper.Map<UserDTO>(await _userInformationService.GetUserInformationAsync(userId));

            return userInfo;
        }

        private IEnumerable<LinkDTO> CreateLinksForCase(int caseId, string fields)
        {
            var links = new List<LinkDTO>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                links.Add(
                  new LinkDTO(Url.Link("GetCase", new { caseId }),
                  "self",
                  "GET"));
            }
            else
            {
                links.Add(
                  new LinkDTO(Url.Link("GetCase", new { caseId, fields }),
                  "self",
                  "GET"));
            }

            links.Add(
                new LinkDTO(Url.Link("DeleteCase", new { caseId }),
                "delete_case",
                "DELETE"));

            links.Add(
                new LinkDTO(Url.Link("UpdateCase", new { caseId }),
                "update_case",
                "PUT"));

            links.Add(
                new LinkDTO(Url.Link("PatchCase", new { caseId }),
                "patch_case",
                "PATCH"));

            links.Add(
                new LinkDTO(Url.Link("UploadImageWithCaseId", new { caseId }),
                "upload_image_for_case",
                "POST"));

            links.Add(
               new LinkDTO(Url.Link("GetLogsForCase", new { caseId }),
               "get_logs_for_case",
               "GET"));


            return links;
        }

        private IEnumerable<LinkDTO> CreateLinksForCases(CaseResourceParameters caseResourceParameters, bool hasNext, bool hasPrevious)
        {
            var links = new List<LinkDTO>
            {
                //self
                new LinkDTO(CreateCaseResourceUri(caseResourceParameters, ResourceUriType.Current), "self", "GET")
            };

            if (hasNext)
            {
                links.Add(new LinkDTO(CreateCaseResourceUri(caseResourceParameters, ResourceUriType.NextPage), "nextPage", "GET"));
            }

            if (hasPrevious)
            {
                links.Add(new LinkDTO(CreateCaseResourceUri(caseResourceParameters, ResourceUriType.PreviousPage), "previousPage", "GET"));
            }

            return links;
        }

        private string CreateCaseResourceUri(CaseResourceParameters caseResourceParameters, ResourceUriType resourceUriType)
        {
            return resourceUriType switch
            {
                ResourceUriType.PreviousPage => Url.Link("GetCases",
                    new
                    {
                        fields = caseResourceParameters.Fields,
                        orderBy = caseResourceParameters.OrderBy,
                        pageNumber = caseResourceParameters.PageNumber - 1,
                        pageSize = caseResourceParameters.PageSize,
                        searchQuery = caseResourceParameters.SearchQuery,
                        filter = caseResourceParameters.Filter
                    }),
                ResourceUriType.NextPage => Url.Link("GetCases",
                    new
                    {
                        fields = caseResourceParameters.Fields,
                        orderBy = caseResourceParameters.OrderBy,
                        pageNumber = caseResourceParameters.PageNumber + 1,
                        pageSize = caseResourceParameters.PageSize,
                        searchQuery = caseResourceParameters.SearchQuery,
                        filter = caseResourceParameters.Filter
                    }),
                _ => Url.Link("GetCases",
                    new
                    {
                        fields = caseResourceParameters.Fields,
                        orderBy = caseResourceParameters.OrderBy,
                        pageNumber = caseResourceParameters.PageNumber,
                        pageSize = caseResourceParameters.PageSize,
                        searchQuery = caseResourceParameters.SearchQuery,
                        filter = caseResourceParameters.Filter
                    }),
            };
        }

        private IEnumerable<LinkDTO> CreateLinksForImage(int imageId, int caseId)
        {

            var links = new List<LinkDTO>();


            links.Add(
                 new LinkDTO(Url.Link("GetCaseImage", new { imageId }),
                 "self",
                 "GET"));
            links.Add(
                new LinkDTO(Url.Link("DeleteImage", new { imageId }),
                "delete_image",
                "DELETE"
                ));
            links.Add(
               new LinkDTO(Url.Link("GetCase", new { caseId }),
               "get_case_for_image",
               "GET"));

            return links;
        }


        private IEnumerable<CaseDTO> GetImageCount(IEnumerable<CaseDTO> caseList)
        {
            foreach (var @case in caseList)
            {
                @case.ImageCount = _caseReposistory.GetImageCount(@case.CaseId);
            }

            return caseList;
        }

        private IEnumerable<CaseDTO> GetLogCount(IEnumerable<CaseDTO> caseList)
        {
            foreach (var @case in caseList)
            {
                @case.LogCount = _caseReposistory.GetLogCount(@case.CaseId);
            }

            return caseList;
        }

    }
}
