using SitrepAPI.Entities;
using SitrepAPI.Helpers;
using SitrepAPI.Models;

namespace SitrepAPI.Services
{
    /// <summary>
    /// Implementation of <see cref="IPropertyMappingService"/>
    /// </summary>
    public class PropertyMappingService : IPropertyMappingService
    {
        private readonly Dictionary<string, PropertyMappingValue> _casePropertyMapping = new(StringComparer.OrdinalIgnoreCase)
        {
            { "CaseId", new PropertyMappingValue(new List<string>() {"CaseId"} ) },
            { "UserId", new PropertyMappingValue(new List<string>() {"UserId"}) },
            { "Title", new PropertyMappingValue(new List<string>() {"Title"} ) },
            { "Location", new PropertyMappingValue(new List<string>() {"Location"}) },
            { "Description", new PropertyMappingValue(new List<string>() {"Description"} ) },
            { "CreatedAt", new PropertyMappingValue(new List<string>() {"CreatedAt"}, true) },
            { "PriorityId", new PropertyMappingValue(new List<string>() {"PriorityId"}) },
            { "Priority", new PropertyMappingValue(new List<string>() {"Priority"}) },
            { "StatusId", new PropertyMappingValue(new List<string>() {"StatusId"}) },
            { "Status", new PropertyMappingValue(new List<string>() {"Status"}) },
            { "AssigneeId", new PropertyMappingValue(new List<string>() {"AssigneeId"})},
            { "ImageCount", new PropertyMappingValue(new List<string>() {"ImageCount"}) }
        };
        private readonly IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        /// <summary>
        /// Constructor of class
        /// </summary>
        public PropertyMappingService()
        {
            _propertyMappings.Add(new PropertyMapping<CaseDTO, Case>(_casePropertyMapping));
        }

        /// <summary>
        /// <see cref="IPropertyMappingService.ValidOderByMappingExistsFor{TSource, TDestination}(string)"/>
        /// </summary>
        /// <typeparam name="TSource">From object</typeparam>
        /// <typeparam name="TDestination">To object</typeparam>
        /// <param name="fields">fields to find in valid mappings</param>
        /// <returns>Boolean</returns>
        public bool ValidOderByMappingExistsFor<TSource, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<TSource, TDestination>();


            //no orderBy clause, no need to find fields
            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            var fieldsAfterSplit = fields.Split(',');

            //run through each field
            foreach (var field in fieldsAfterSplit)
            {
                //trim
                var trimmedField = field.Trim();
                //remove everthing after the first " " - if the fields are coming from an orderBy string, this part must be ingnored
                var indexOfFirstSpace = trimmedField.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1 ? trimmedField : trimmedField.Remove(indexOfFirstSpace);

                //find the matching property
                if (!propertyMapping.ContainsKey(propertyName))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// <see cref="IPropertyMappingService.ValidFilterByMappingExistsFor{TSource, TDestination}(string)"/>
        /// </summary>
        /// <typeparam name="TSource">From object</typeparam>
        /// <typeparam name="TDestination">To object</typeparam>
        /// <param name="fields">fields to find in valid mappings</param>
        /// <returns>Boolean</returns>
        public bool ValidFilterByMappingExistsFor<TSource, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<TSource, TDestination>();


            //no orderBy clause, no need to find fields
            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            var fieldsAfterSplit = fields.Split(',');

            //run through each field
            foreach (var field in fieldsAfterSplit)
            {
                //trim
                var trimmedField = field.RemoveWhiteSpaces();

                var filterOperation = trimmedField.FilterBySeperation();
                //remove everthing after the first " " - if the fields are coming from an orderBy string, this part must be ingnored
                var indexOfFirstSpace = filterOperation.PropertyName.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1 ? filterOperation.PropertyName : filterOperation.PropertyName.Remove(indexOfFirstSpace);

                //find the matching property
                if (!propertyMapping.ContainsKey(propertyName))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// <see cref="IPropertyMappingService.GetPropertyMapping{TSource, TDestination}"/>
        /// </summary>
        /// <typeparam name="TSource">From object</typeparam>
        /// <typeparam name="TDestination">To object</typeparam>
        /// <returns>Dictionary of string, propermappingvalue</returns>
        /// <exception cref="Exception">No valid mappings for <typeparamref name="TSource"/> to <typeparamref name="TDestination"/></exception>
        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {
            //Get matching mapping
            var matchingMapping = _propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();

            if (matchingMapping.Count() == 1)
            {
                return matchingMapping.First().MappingDictionary;
            }

            throw new Exception($"Cannot find exact property mapping instance for <{typeof(TSource)},{typeof(TDestination)}");
        }


    }
}
