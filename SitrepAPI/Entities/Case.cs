namespace SitrepAPI.Entities
{
    /// <summary>
    /// Case database entity
    /// </summary>
    public class Case
    {
        /// <summary>
        /// Id of case
        /// </summary>
        public int CaseId { get; private set; }
        /// <summary>
        /// UserId of the person who created case
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Title of the case
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// phisycal location of error descriped in case
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// Detailed desciption of error/case
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Created datetime
        /// </summary>
        public DateTimeOffset CreatedAt { get; private set; }
        /// <summary>
        /// Assigned PriorityId
        /// </summary>
        public int PriorityId { get; set; }
        /// <summary>
        /// Navigational property to assigned priority
        /// </summary>
        public Priority Priority { get; set; }
        /// <summary>
        /// Assigned StatusId
        /// </summary>
        public int StatusId { get; set; }
        /// <summary>
        /// Navigational property to assigned status
        /// </summary>
        public Status Status { get; set; }
        /// <summary>
        /// UserId of assigned person
        /// </summary>
        public string? AssigneeId { get; set; }

        /// <summary>
        /// Navigational property of images related to case
        /// </summary>
        public ICollection<CaseImage> Images { get; set; } = new List<CaseImage>();
        /// <summary>
        /// Navigational property of logs related to case
        /// </summary>
        public ICollection<CaseLog> Logs { get; set; } = new List<CaseLog>();

        /// <summary>
        /// Constructor of class
        /// </summary>
        public Case()
        {

        }
    }
}
