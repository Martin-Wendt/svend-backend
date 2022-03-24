using System.ComponentModel.DataAnnotations;

namespace SitrepAPI.Models
{
    /// <summary>
    /// Create log DTO
    /// </summary>
    public class CaseLogForCreationDTO
    {
        /// <summary>
        /// Name of property changed
        /// </summary>
        [Required]
        public string Message { get; set; }

        /// <summary>
        /// Contructor of class
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public CaseLogForCreationDTO()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }
    }
}
