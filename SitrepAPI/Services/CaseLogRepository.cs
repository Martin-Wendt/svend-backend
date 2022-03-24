using Microsoft.EntityFrameworkCore;
using SitrepAPI.DbContexts;
using SitrepAPI.Entities;

namespace SitrepAPI.Services
{
    /// <summary>
    /// Implementation of <see cref="ICaseLogRepository"/>
    /// </summary>
    public class CaseLogRepository : BaseRepository, ICaseLogRepository
    {
        private readonly SitrepDbContext _context;
        private readonly IUserInformationService _userInformationService;


        /// <summary>
        /// Constructor of class
        /// </summary>
        /// <param name="context">DbContext</param>
        /// <param name="userInformationService">User information service</param>
        /// <exception cref="ArgumentNullException">Dependency injection failure</exception>
        public CaseLogRepository(SitrepDbContext context, IUserInformationService userInformationService) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userInformationService = userInformationService ?? throw new ArgumentNullException(nameof(userInformationService));

        }



        /// <summary>
        /// Add new log for case
        /// </summary>
        /// <param name="caseLog">Case to add</param>
        /// <exception cref="ArgumentNullException">Null input</exception>
        public void AddCaseLog(CaseLog caseLog)
        {
            if (caseLog == null)
            {
                throw new ArgumentNullException(null,nameof(CaseLog));
            }
            _context.Add(caseLog);
        }

        /// <summary>
        /// Gets single log
        /// </summary>
        /// <param name="caseLogId">Id of log</param>
        /// <returns>CaseLog</returns>
        /// <exception cref="ArgumentNullException">Null input</exception>
        public CaseLog GetLog(int caseLogId)
        {
            var log = _context.CaseLogs.SingleOrDefault(x => x.CaseLogId == caseLogId);
            if (log is null)
            {
                throw new ArgumentNullException(null,nameof(log));
            }
            return log;
        }

        /// <summary>
        /// Gets all logs for a case
        /// </summary>
        /// <param name="caseId">Id of case</param>
        /// <returns>List of <see cref="CaseLog"/></returns>
        public List<CaseLog> GetLogsForCase(int caseId)
        {
            return _context.CaseLogs.Where(x => x.CaseId == caseId).ToList();
        }

    }
}
