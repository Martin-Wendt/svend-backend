using System.ComponentModel.DataAnnotations;

namespace SitrepAPI.Models
{
#pragma warning disable
    /// <summary>
    /// Case for creation object
    /// </summary>
    public class CaseForCreationDTO
    {
        /// <summary>
        /// Title of case
        /// </summary>
        [Required]
        public string Title { get; set; }
        /// <summary>
        /// Location of case
        /// </summary>
        [Required]
        public string Location { get; set; }
        /// <summary>
        /// Description of case
        /// </summary>
        [Required]
        public string Description { get; set; }
        /// <summary>
        /// Image ids that belongs to the case
        /// </summary>
        public List<CaseImageDTO>? Images { get; set; }
    }
#pragma warning restore
}
