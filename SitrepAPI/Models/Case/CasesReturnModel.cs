namespace SitrepAPI.Models
{
#pragma warning disable
    /// <summary>
    /// Representaion of object returned when requesting multiple cases
    /// </summary>
    public class CasesReturnModel
    {
        /// <summary>
        /// List with returned cases
        /// </summary>
        public List<CaseDTO> Value { get; set; }
        /// <summary>
        /// Returned link for collection
        /// </summary>
        public List<LinkDTO> Links { get; set; }
    }
#pragma warning restore
}
