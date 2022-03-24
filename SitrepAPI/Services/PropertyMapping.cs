namespace SitrepAPI.Services
{
    /// <summary>
    /// Implementation of PropertyMapping service <see cref="IPropertyMappingService"/>
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    public class PropertyMapping<TSource, TDestination> : IPropertyMapping
    {
        /// <summary>
        /// Valid mappings
        /// </summary>
        public Dictionary<string, PropertyMappingValue> MappingDictionary { get; private set; }

        /// <summary>
        /// Constructor of class
        /// </summary>
        /// <param name="mappingDictionary"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public PropertyMapping(Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            MappingDictionary = mappingDictionary ?? throw new ArgumentNullException(nameof(mappingDictionary));
        }
    }
}
