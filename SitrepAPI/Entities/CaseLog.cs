namespace SitrepAPI.Entities
{
    /// <summary>
    /// CaseLog database entity
    /// </summary>
    public class CaseLog
    {
        /// <summary>
        /// Id of Log
        /// </summary>
        public int CaseLogId { get; private set; }
        /// <summary>
        /// Id of case the log is related to
        /// </summary>
        public int CaseId { get; set; }
        /// <summary>
        /// Time of creation
        /// </summary>
        public DateTimeOffset CreatedAt { get; private set; }
        /// <summary>
        /// Creator id of log
        /// </summary>
        public string CreatedById { get; set; }
        /// <summary>
        /// creator name of log
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Name of property changed
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// constructor of class
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public CaseLog()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }
    }
}
