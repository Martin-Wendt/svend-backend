namespace SitrepAPI.Services
{
    /// <summary>
    /// Found matching property mappings class
    /// </summary>
    public class PropertyMappingValue
    {
        /// <summary>
        /// Property names
        /// </summary>
        public IEnumerable<string> DestinationProperties { get; private set; }
        /// <summary>
        /// Apply in reverse direction
        /// </summary>
        public bool Revert { get; private set; }

        /// <summary>
        /// Constructor of class
        /// </summary>
        /// <param name="destinationProperties">Propertynames</param>
        /// <param name="revert">Apply in reverse direction</param>
        /// <exception cref="ArgumentNullException">Dependency Injection failure</exception>
        public PropertyMappingValue(IEnumerable<string> destinationProperties, bool revert = false)
        {
            DestinationProperties = destinationProperties ?? throw new ArgumentNullException(nameof(destinationProperties));
            Revert = revert;
        }

    }
}
