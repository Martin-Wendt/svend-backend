
namespace SitrepAPI.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPropertyMappingService
    {
        /// <summary>
        /// Gets mapping from <typeparamref name="TSource"/> to <typeparamref name="TDestination"/>
        /// </summary>
        /// <typeparam name="TSource">From object</typeparam>
        /// <typeparam name="TDestination">To object</typeparam>
        /// <returns>Dictionary of string, propermappingvalue</returns>
        /// <exception cref="Exception">No valid mappings for <typeparamref name="TSource"/> to <typeparamref name="TDestination"/></exception>
        Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();

        /// <summary>
        /// Valid mapping exsists from <typeparamref name="TSource"/> to <typeparamref name="TDestination"/>
        /// </summary>
        /// <typeparam name="TSource">From object</typeparam>
        /// <typeparam name="TDestination">To object</typeparam>
        /// <param name="fields">fields to find in valid mappings</param>
        /// <returns>Boolean</returns>
        bool ValidOderByMappingExistsFor<TSource, TDestination>(string fields);

        /// <summary>
        ///  Valid mapping exsists from <typeparamref name="TSource"/> to <typeparamref name="TDestination"/>
        ///  Seperates string into Propertyname,operation,value
        /// </summary>
        /// <typeparam name="TSource">From object</typeparam>
        /// <typeparam name="TDestination">To object</typeparam>
        /// <param name="fields">fields to find in valid mappings</param>
        /// <returns>boolean</returns>
        bool ValidFilterByMappingExistsFor<TSource, TDestination>(string fields);
    }
}