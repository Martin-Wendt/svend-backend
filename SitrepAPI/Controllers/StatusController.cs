#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SitrepAPI.DbContexts;
using SitrepAPI.Entities;

namespace SitrepAPI.Controllers
{
    /// <summary>
    /// Controller for Status
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        // TODO: DI denpendcy for context
        private readonly SitrepDbContext _context;

        /// <summary>
        /// Constructor of class
        /// </summary>
        /// <param name="context"></param>
        public StatusController(SitrepDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get list of status'
        /// </summary>
        /// <returns>List of <see cref="Status"/></returns>
        // GET: api/Status
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Status>>> GetStatus()
        {
            return await _context.Status.ToListAsync();
        }

        /// <summary>
        /// Get single status
        /// </summary>
        /// <param name="id">Id of status</param>
        /// <returns></returns>
        // GET: api/Status/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Status>> GetStatus(int id)
        {
            var status = await _context.Status.FindAsync(id);

            if (status == null)
            {
                return NotFound();
            }

            return status;
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
            Response.Headers.Add("Allow", "GET,OPTIONS,HEAD");
            return Ok();
        }
    }
}
