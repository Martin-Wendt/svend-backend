namespace SitrepAPI.Entities
{
    /// <summary>
    /// Priority values
    /// </summary>
    public class Priority
    {
        /// <summary>
        /// Id of priority
        /// </summary>
        public int PriorityId { get; set; }
        /// <summary>
        /// Name of priority
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Constructor of class
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Priority()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {

        }
    }
}
