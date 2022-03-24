using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SitrepAPI.DbContexts;
using SitrepAPI.Entities;
using SitrepAPI.Models;

namespace SitrepAPI.Services
{
    /// <summary>
    /// Implementation of <see cref="ICaseImageRepository"/>
    /// </summary>
    public class CaseImageRepository : BaseRepository, ICaseImageRepository
    {
        private readonly SitrepDbContext _context;

        /// <summary>
        /// constructor of class
        /// </summary>
        /// <param name="context">DbContext</param>
        /// <exception cref="ArgumentNullException">Dependency injection failure</exception>
        public CaseImageRepository(SitrepDbContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));      
        }

        /// <summary>
        /// <see cref="ICaseImageRepository.AddCaseImage(CaseImage,string)"/>
        /// </summary>
        /// <param name="caseImage">Image to add</param>
        /// <param name="userId">Case id to adad iamge to</param>
        public void AddCaseImage(CaseImage caseImage, string userId)
        {
            _context.CaseImages.Add(caseImage);
        }

        /// <summary>
        /// <see cref="ICaseImageRepository.GetListOfCaseImagesIds(int)"/>
        /// </summary>
        /// <param name="caseId">Id of case</param>
        /// <returns>List of int</returns>
        public List<int> GetListOfCaseImagesIds(int caseId)
        {
            return _context.CaseImages.Where(x => x.CaseId == caseId).Select(x => x.CaseImageId).ToList();
        }

        /// <summary>
        /// <see cref="ICaseImageRepository.GetCaseImage(int)"/>
        /// </summary>
        /// <param name="caseImageId">Id of image</param>
        /// <returns><see cref="CaseImage"/></returns>
        public CaseImage GetCaseImage(int caseImageId)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _context.CaseImages.SingleOrDefault(x => x.CaseImageId == caseImageId);
#pragma warning restore CS8603 // Possible null reference return.
        }


        /// <summary>
        /// <see cref="ICaseImageRepository.GetCaseImagesForCase(int)"/>
        /// </summary>
        /// <param name="caseId">Id of case</param>
        /// <returns>List of <see cref="CaseImage"/></returns>
        public List<CaseImage> GetCaseImagesForCase(int caseId)
        {
            return _context.CaseImages.Where(x => x.CaseId == caseId).ToList();
        }
        /// <summary>
        /// Update image, does nothing <see cref="ICaseRepository.UpdateCaseAsync(Case, string)"/>
        /// </summary>
        /// <param name="image"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public void UpdateImageAsync(CaseImage image, string userId)
        {
            //does nothing
        }

        /// <summary>
        /// <see cref="ICaseImageRepository.CaseImageExists(int)"/>
        /// </summary>
        /// <param name="caseImageId">Id of image</param>
        /// <returns>Boolean</returns>
        public bool CaseImageExists(int caseImageId)
        {
            return _context.CaseImages.Any(x => x.CaseImageId == caseImageId);
        }

        /// <summary>
        /// Remove image from repo
        /// </summary>
        /// <param name="caseImage">caseImage to remove</param>
        public void DeleteCaseImage(CaseImage caseImage)
        {
            _context.CaseImages.Remove(caseImage);
        }
    }
}
