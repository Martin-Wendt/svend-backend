#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SitrepAPI.DbContexts;
using SitrepAPI.Entities;

namespace SitrepAPI.Controllers
{
    /// <summary>
    /// Priorities controller
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PrioritiesController : ControllerBase
    {
        // TODO: DI denpendcy for context
        private readonly SitrepDbContext _context;

        /// <summary>
        /// Constructor of controller
        /// </summary>
        /// <param name="context"></param>
        public PrioritiesController(SitrepDbContext context)
        {
            _context = context;
        }

        // GET: api/Priorities
        /// <summary>
        /// Gets List of Priorities
        /// </summary>
        /// <returns>List of <see cref="Priority"/></returns>
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Priority>>> GetPriority()
        {
            return await _context.Priority.ToListAsync();
        }

        /// <summary>
        /// Get single priority
        /// </summary>
        /// <param name="id">Id of priority</param>
        /// <returns></returns>
        // GET: api/Priorities/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Priority>> GetPriority(int id)
        {
            var priority = await _context.Priority.FindAsync(id);

            if (priority == null)
            {
                return NotFound();
            }

            return priority;
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
            Response.Headers.Add("Allow", "GET,HEAD,OPTIONS");
            return Ok();
        }
    }
}
