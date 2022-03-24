using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SitrepAPI.Entities;
using SitrepAPI.Helpers;
using SitrepAPI.Models;
using SitrepAPI.Models.Auth0;
using SitrepAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SitrepAPI.Controllers
{
    /// <summary>
    /// Controller for handling CaseLogs
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ICaseRepository _caseRepository;
        private readonly ICaseLogRepository _caseLogRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserInformationService _userInformationService;

        /// <summary>
        /// Constructor of controller
        /// </summary>
        /// <param name="mapper">AutoMapper</param>
        /// <param name="caseRepository">Repository for Cases</param>
        /// <param name="caseLogRepository">Respository for Logs</param>
        /// <param name="authorizationService">Authorization policy</param>
        /// <param name="userInformationService">User information service</param>
        /// <exception cref="ArgumentNullException">If Dependenct fails to provide requested services</exception>
        public LogsController(IMapper mapper, ICaseRepository caseRepository, ICaseLogRepository caseLogRepository, IAuthorizationService authorizationService, IUserInformationService userInformationService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _caseRepository = caseRepository ?? throw new ArgumentNullException(nameof(caseRepository));
            _caseLogRepository = caseLogRepository ?? throw new ArgumentNullException(nameof(caseLogRepository));
            _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
            _userInformationService = userInformationService ?? throw new ArgumentNullException(nameof(userInformationService));
        }

        [HttpGet(Name = "GetCaseLog")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CaseLogDTO))]
        public IActionResult GetCaseLog(int caseLogId)
        {
            var caseLogDTO = _mapper.Map<CaseLogDTO>(_caseLogRepository.GetLog(caseLogId));

            var shapedLog = caseLogDTO.ShapeData(null) as IDictionary<string, object>;
            var links = CreateLinksForCaseLog(caseLogDTO.CaseId, caseLogId);

            shapedLog.Add("links", links);


            return Ok(shapedLog);
        }
        /// <summary>
        /// Get Logs for Case
        /// </summary>
        /// <param name="caseId">Id of case</param>
        /// <returns>ActionResult with List of CaseLogDTO</returns>
        // GET: api/cases/{caseId}/<LogsController>
        [HttpGet("/api/Cases/{caseId}/[Controller]",Name = "GetLogsForCase")]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CaseLogDTO>))]
        public async Task<IActionResult> GetLogsAsync([FromRoute] int caseId)
        {
            var caseFromRepo = _caseRepository.GetCase(caseId);

            if (caseFromRepo is null)
            {
                return BadRequest();
            }

            //only pass if owner of case or has roles manager or operator
            var auth = await _authorizationService.AuthorizeAsync(this.User, caseFromRepo, "HasAccess");
            if (!auth.Succeeded)
            {
                return new ForbidResult();
            }

            var logEntities = _caseLogRepository.GetLogsForCase(caseId);
            var logsToReturn = _mapper.Map<IEnumerable<CaseLogDTO>>(logEntities);
            var shapedCaseLogs = logsToReturn.ShapeData(null);


            var shapedCasesWithLinks = shapedCaseLogs.Select(@case =>
            {
                //@case. = _caseReposistory.GetImageCount();
                var caseAsDictionary = @case as IDictionary<string, object>;

                //  if case id was not requested this results in a exception - 
                var userLinks = CreateLinksForCaseLog((int)caseAsDictionary["CaseId"], (int)caseAsDictionary["CaseLogId"]);
                caseAsDictionary.Add("links", userLinks);
                return caseAsDictionary;
            });


            return Ok(shapedCasesWithLinks);
        }


        /// <summary>
        /// Create Log for case
        /// </summary>
        /// <param name="caseLogForCreation">log to create</param>
        /// <param name="caseId">Id of case log is created for</param>
        /// <returns></returns>
        // POST: api/<LogsController>
        [HttpPost("/api/Cases/{caseId}/[Controller]", Name = "CreateCaseLog")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CaseLogDTO))]
        public async Task<IActionResult> PostLogAsync(CaseLogForCreationDTO caseLogForCreation, [FromRoute] int caseId)
        {
            if (caseLogForCreation is null)
            {
                return BadRequest();
            }

            var caseLog = _mapper.Map<CaseLog>(caseLogForCreation);

            var userId = User.Identity.Name;
            var userinfo = await _userInformationService.GetUserInformationAsync(userId);

            caseLog.CreatedBy = userinfo.Name;
            caseLog.CreatedById = userId;
            caseLog.CaseId = caseId;

            _caseLogRepository.AddCaseLog(caseLog);
            _caseLogRepository.Save();

            var shapedLog = caseLog.ShapeData(null) as IDictionary<string, object>;
            var links = CreateLinksForCaseLog(caseLog.CaseId, caseLog.CaseLogId);

            shapedLog.Add("links", links);

            return CreatedAtRoute(nameof(GetCaseLog), new { caseLogId = caseLog.CaseLogId}, shapedLog);

        }

        //  HEAD: api/cases/{caseId}/logs
        /// <summary>
        /// Allowed methods
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetLogsOptions()
        {
            Response.Headers.Add("Allow", "GET,POST,HEAD,OPTIONS");
            return Ok();
        }

        private IEnumerable<LinkDTO> CreateLinksForCaseLog(int caseId, int caseLogId )
        {
            var links = new List<LinkDTO>();


            links.Add(
                 new LinkDTO(Url.Link("GetCaseLog", new { caseLogId }),
                 "self",
                 "GET"));


            links.Add(
               new LinkDTO(Url.Link("GetCase", new { caseId }),
               "get_case_for_logs",
               "GET"));


            return links;
        }


    }
}
