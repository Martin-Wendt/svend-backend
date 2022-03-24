using System.ComponentModel.DataAnnotations;

namespace SitrepAPI.Models
{
#pragma warning disable
    /// <summary>
    /// Representaion of Case object when updating/Put
    /// </summary>
    public class CaseForUpdateDTO
    {
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
        [Required]
        [Range(1,4)]
        public int PriorityId { get; set; }
        /// <summary>
        /// Status id of case
        /// </summary>
        [Required]
        [Range(1,6)]
        public int StatusId { get; set; }
        /// <summary>
        /// UserId of Operator who is assigned to the case
        /// </summary>
        public string? AssigneeId { get; set; }

    }
#pragma warning restore
}
