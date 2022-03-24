namespace SitrepAPI.Entities
{
    /// <summary>
    /// Status value
    /// </summary>
    public class Status
    {
        /// <summary>
        /// Id of status
        /// </summary>
        public int StatusId { get; set; }
        /// <summary>
        /// Name of status
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Constructor of class
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Status()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }
    }
}
