using SitrepAPI.Entities;

namespace SitrepAPI.Services
{
    /// <summary>
    /// Interface for CaseImage repository service
    /// </summary>
    public interface ICaseImageRepository
    {

        /// <summary>
        /// Add single case to db
        /// </summary>
        /// <param name="caseImage">Image Id to add</param>
        /// <param name="userId">Person who adds picture</param>
        void AddCaseImage(CaseImage caseImage, string userId);

        /// <summary>
        /// Get Image
        /// </summary>
        /// <param name="caseImageId">Image id</param>
        /// <returns>list of int</returns>
        CaseImage GetCaseImage(int caseImageId);
        
        /// <summary>
        /// Get images for case
        /// </summary>
        /// <param name="caseId">Case Id</param>
        /// <returns>List of <see cref="CaseImage"/></returns>
        List<CaseImage> GetCaseImagesForCase(int caseId);
        /// <summary>
        /// Gets a list of CaseImages
        /// </summary>
        /// <param name="caseId">CaseId that images relate to</param>
        /// <returns>List of <see cref="CaseImage"/></returns>
        List<int> GetListOfCaseImagesIds(int caseId);
        
        /// <summary>
        /// Saves change to context
        /// </summary>
        void Save();

        /// <summary>
        /// update image with new properties
        /// </summary>
        /// <param name="image">imageDTO to update</param>
        /// <param name="userId">id of updater</param>
        /// <returns></returns>
        void UpdateImageAsync(CaseImage image, string userId);

        /// <summary>
        /// CaseImage Exsists in database
        /// </summary>
        /// <param name="caseImageId">Id of image</param>
        /// <returns>Boolean</returns>
        bool CaseImageExists(int caseImageId);

        /// <summary>
        /// Delete image with id
        /// </summary>
        /// <param name="caseImage">Id of image</param>
        void DeleteCaseImage(CaseImage caseImage);
    }
}