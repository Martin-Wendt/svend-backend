using SitrepAPI.Entities;
using SitrepAPI.Helpers;
using SitrepAPI.ResourceParameters;

namespace SitrepAPI.Services
{
    /// <summary>
    /// Interface for Case repository service
    /// /// </summary>
    public interface ICaseRepository
    {
        /// <summary>
        /// Validate if case exsists in database
        /// </summary>
        /// <param name="caseId">Id of case</param>
        /// <returns>Boolean</returns>
        bool CaseExists(int caseId);
        /// <summary>
        /// Delete case, Only shadow deletes
        /// </summary>
        /// <param name="entityToDelete">Case to delete</param>
        void Delete(Case entityToDelete);
        /// <summary>
        /// Get single case
        /// </summary>
        /// <param name="caseId">Id of case</param>
        /// <returns><see cref="Case"/></returns>
        Case GetCase(int caseId);
        /// <summary>
        /// Get <see cref="PagedList{T}"/> of cases
        /// </summary>
        /// <param name="caseResourceParameters">resource parameters</param>
        /// <param name="userId">Id of user to filter by, accepts null for all</param>
        /// <returns><see cref="PagedList{T}"/> of cases</returns>
        PagedList<Case> GetCases(CaseResourceParameters caseResourceParameters, string userId);
        /// <summary>
        /// Save changes made
        /// </summary>
        void Save();
        /// <summary>
        /// Update single case
        /// </summary>
        /// <param name="case">Id of case</param>
        /// <param name="userId">UserId for validation</param>
        Task UpdateCaseAsync(Case @case, string userId);
        /// <summary>
        /// Add single case
        /// </summary>
        /// <param name="case">Case to add</param>
        void AddCase(Case @case);
        /// <summary>
        /// Gets count of images for case
        /// </summary>
        /// <param name="CaseId">Id of case</param>
        /// <returns>Int</returns>
        int GetImageCount(int CaseId);
        /// <summary>
        /// Gets count of logs for case
        /// </summary>
        /// <param name="caseId">Id of case</param>
        /// <returns>Int</returns>
        int GetLogCount(int caseId);
    }
}