using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SitrepAPI.Entities;
using SitrepAPI.Helpers;
using SitrepAPI.Models;
using SitrepAPI.Services;
using System.Dynamic;

namespace SitrepAPI.Controllers
{
    /// <summary>
    /// Image Controller 
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ICaseImageRepository _caseImageRepository;
        private readonly ICaseRepository _caseRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserInformationService _userInformationService;
        private readonly ICaseLogRepository _caseLogRepository;

        /// <summary>
        /// Constructor of class
        /// </summary>
        /// <param name="caseRepository">Case repository service</param>
        /// <param name="caseImageRepository">CaseImage repository service</param>
        /// <param name="mapper">AutoMapper</param>
        /// <param name="authorizationService">Authorization policy</param>
        /// <param name="userInformationService">User information service</param>
        /// <param name="caseLogRepository">CaseLog repo service</param>
        /// <exception cref="ArgumentNullException">If dependency injection fails to provide service</exception>
        public ImagesController(ICaseRepository caseRepository, ICaseImageRepository caseImageRepository, IMapper mapper, IAuthorizationService authorizationService, IUserInformationService userInformationService, ICaseLogRepository caseLogRepository)
        {
            _caseRepository = caseRepository ?? throw new ArgumentNullException(nameof(caseRepository));
            _caseImageRepository = caseImageRepository ?? throw new ArgumentNullException(nameof(caseImageRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
            _userInformationService = userInformationService ?? throw new ArgumentNullException(nameof(userInformationService));
            _caseLogRepository = caseLogRepository ?? throw new ArgumentNullException(nameof(caseLogRepository));
        }
        /// <summary>
        /// Get a single Image
        /// </summary>
        /// <param name="imageId">Id of Image</param>
        /// <returns></returns>
        [HttpGet("{imageId}", Name = "GetImage")]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetImage(int imageId)
        {
            if (!_caseImageRepository.CaseImageExists(imageId))
            {
                return BadRequest();
            }

            var caseImages = _caseImageRepository.GetCaseImage(imageId);

            return File(caseImages.Image, caseImages.Type);
        }

        /// <summary>
        /// Upload image without caseid
        /// </summary>
        /// <param name="file">image to upload</param>
        /// <returns>Id of case</returns>
        [HttpPost(Name = "UploadImage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            CaseImageForUploadDTO caseImage = new();


            if (!ValidImageType(file))
            {
                return BadRequest("Not Supported format. Only supports image/jpeg and image/png");
            }

            using (MemoryStream ms = new())
            {
                await file.CopyToAsync(ms);
                caseImage.Name = Path.GetRandomFileName();
                caseImage.Image = ms.ToArray();
                caseImage.Type = file.ContentType;
            }


            var caseImageEntity = _mapper.Map<CaseImage>(caseImage);


#pragma warning disable CS8604 // Possible null reference argument.
            _caseImageRepository.AddCaseImage(caseImageEntity, User.Identity.Name);
#pragma warning restore CS8604 // Possible null reference argument.
            _caseImageRepository.Save();


            return Ok(new { caseImageId = caseImageEntity.CaseImageId });

        }

        /// <summary>
        /// Post a image to Case
        /// </summary>
        /// <param name="file">Image file</param>
        /// <param name="caseId">Id of case</param>
        /// <returns>200</returns>
        [HttpPost("Case/{caseId}", Name = "UploadImageWithCaseId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UploadImageForCase(IFormFile file, [FromRoute] int caseId)
        {
            CaseImageForUploadDTO caseImage = new();
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

            if (!ValidImageType(file))
            {
                return BadRequest("Not Supported format. Only supports image/jpeg and image/png");
            }

            using (MemoryStream ms = new())
            {
                await file.CopyToAsync(ms);
                caseImage.Name = Path.GetRandomFileName();
                caseImage.Image = ms.ToArray();
                caseImage.CaseId = caseId;
                caseImage.Type = file.ContentType;
            }


            var caseImageEntity = _mapper.Map<CaseImage>(caseImage);

#pragma warning disable CS8604 // Possible null reference argument.
            var userInfo = await _userInformationService.GetUserInformationAsync(User.Identity.Name);
#pragma warning restore CS8604 // Possible null reference argument.

            _caseImageRepository.AddCaseImage(caseImageEntity, User.Identity.Name);

#pragma warning disable CS8601 // Possible null reference assignment.
            CaseLogDTO caseLogDTO = new()
            {
                CaseId = caseId,
                CreatedAt = DateTimeOffset.Now,
                CreatedById = User.Identity.Name,
                CreatedBy = userInfo.Name,
                Message = "Billed tilføjet"
            };
#pragma warning restore CS8601 // Possible null reference assignment.
            _caseLogRepository.AddCaseLog(_mapper.Map<CaseLog>(caseLogDTO));
            _caseImageRepository.Save();


            return Ok(new { caseImageId = caseImageEntity.CaseImageId });

        }

        //  Delete: api/Images/ImageId
        /// <summary>
        /// Delete Image
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpDelete("{imageId}", Name = "DeleteImage")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCaseImageAsync(int imageId)
        {
            var imageFromRepo = _caseImageRepository.GetCaseImage(imageId);

            if (imageFromRepo is null)
            {
                return BadRequest();
            }

            var userInfo = await _userInformationService.GetUserInformationAsync(User.Identity.Name);

            if (imageFromRepo.CaseId is not null)
            {
                CaseLogDTO caseLogDTO = new()
                {
                    CaseId = (int)imageFromRepo?.CaseId,
                    CreatedAt = DateTimeOffset.Now,
                    CreatedById = User.Identity.Name,
                    Message = "Billed fjernet",
                    CreatedBy = userInfo.Name
                };

                _caseLogRepository.AddCaseLog(_mapper.Map<CaseLog>(caseLogDTO));

            }
            _caseImageRepository.DeleteCaseImage(imageFromRepo);
            _caseImageRepository.Save();

            return NoContent();
        }

        //  HEAD: api/cases/{caseId}/logs
        /// <summary>
        /// Allowed methods
        /// </summary>
        /// <returns>IActionResult</returns>
        [HttpOptions]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetUserOptions()
        {
            Response.Headers.Add("Allow", "GET,POST,OPTIONS,HEAD");
            return Ok();
        }

        private static bool ValidImageType(IFormFile file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            if (file.ContentType == "image/png")
            {
                return true;
            }
            if (file.ContentType == "image/jpeg")
            {
                return true;
            }

            return false;
        }

    }
}
