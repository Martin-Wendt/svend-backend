using SitrepAPI.Entities;

namespace SitrepAPI.Services
{
    /// <summary>
    /// Interface for CaseLog repository service
    /// </summary>
    public interface ICaseLogRepository
    {
        /// <summary>
        /// Get Logs for case
        /// </summary>
        /// <param name="caseId">Case Id</param>
        /// <returns>List of <see cref="CaseLog"/></returns>
        List<CaseLog> GetLogsForCase(int caseId);

        CaseLog GetLog(int caseLogId);

        /// <summary>
        /// Save changes made to DbContext
        /// </summary>
        void Save();

        /// <summary>
        /// Add caselog to case 
        /// </summary>
        /// <param name="caseLog">Log to add</param>
        void AddCaseLog(CaseLog caseLog);
    }
}