using System.ComponentModel.DataAnnotations;

namespace SitrepAPI.Models
{
    /// <summary>
    /// Date transfer object of a Case
    /// </summary>
    public class CaseDTO
    {
        /// <summary>
        /// Id of case
        /// </summary>
        [Required]
        public int CaseId { get; set; }
        /// <summary>
        /// UserDTO of user
        /// </summary>
        [Required]
        public string UserId { get; set; }
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
        /// Priority Id of case
        /// </summary>
        /// 
        [Required]
        [Range(1, 4)]
        public int PriorityId { get; set; }
        /// <summary>
        /// Priority name of case
        /// </summary>
        public string PriorityName { get; set; }
        /// <summary>
        /// Status id of case
        /// </summary>
        [Required]
        [Range(1, 6)]
        public int StatusId { get; set; }
        /// <summary>
        /// Amount of images uploaded to case
        /// </summary>
        [Required]
        public int ImageCount { get; set; }
        /// <summary>
        /// Image related to case, contains only Id of image.
        /// </summary>
        public List<CaseImageDTO> CaseImages { get; set; }
        /// <summary>
        /// Amount of logs related to case
        /// </summary>
        [Required]
        public int LogCount { get; set; }
        /// <summary>
        /// UserId of the creator
        /// </summary>
        public UserDTO CreatedBy { get; set; }
        /// <summary>
        /// Status name of case
        /// </summary>
        [Required]
        public string StatusName { get; set; }
        /// <summary>
        /// Creation date of case
        /// </summary>
        [Required]
        public DateTimeOffset CreatedAt { get; set; }
        /// <summary>
        /// Lastest change performed at
        /// </summary>
        public DateTimeOffset LatestChangeAt { get; set; }
        /// <summary>
        /// UserId of assigned person
        /// </summary>
        public string AssigneeId { get; set; }
        /// <summary>
        /// User object of assignee
        /// </summary>
        public UserDTO Assignee { get; set; }

        /// <summary>
        /// Constructor of class
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public CaseDTO()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }
    }
}
