namespace SitrepAPI.Models
{
    /// <summary>
    /// Link object
    /// </summary>
    public class LinkDTO
    {
        /// <summary>
        /// Url for this operation
        /// </summary>
        public string Href { get; set; }
        /// <summary>
        /// relation of operation
        /// </summary>
        public string Rel { get; set; }
        /// <summary>
        /// http method
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="href">Url</param>
        /// <param name="rel">relation to object</param>
        /// <param name="method">http method</param>
        public LinkDTO(string href, string rel, string method)
        {
            Href = href;
            Rel = rel;
            Method = method;
        }
    }
}
