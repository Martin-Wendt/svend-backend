using System.ComponentModel.DataAnnotations;

namespace SitrepAPI.Models
{
    /// <summary>
    /// Representation of CaseLog.
    /// </summary>
    public class CaseLogDTO
    {
        /// <summary>
        /// Id of Log
        /// </summary>
        [Required]
        public int CaseLogId { get; set; }
        /// <summary>
        /// Id of related Case
        /// </summary>
        [Required]
        public int CaseId { get; set; }
        /// <summary>
        /// Time of creation
        /// </summary>
        [Required]
        public DateTimeOffset CreatedAt { get; set; }
        /// <summary>
        /// Creator
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Creator id of log
        /// </summary>
        [Required]
        public string CreatedById { get; set; }
        /// <summary>
        /// Name of property changed
        /// </summary>
        [Required]
        public string Message { get; set; }
        /// <summary>
        /// constructor of class
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public CaseLogDTO()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }
    }
}
