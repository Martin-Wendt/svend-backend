using SitrepAPI.Services;
using System.Linq.Dynamic.Core;
using System.Text.RegularExpressions;

namespace SitrepAPI.Helpers
{
    /// <summary>
    /// IQueryable extension
    /// </summary>
    public static class IQueryableExtensions
    {
        /// <summary>
        /// Apply sort extension method
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="source">Object itself</param>
        /// <param name="orderBy">Orderby string</param>
        /// <param name="mappingDictionary">Mapping dictionary that contains valid mappings</param>
        /// <returns>IQueryable of T</returns>
        /// <exception cref="ArgumentNullException">Null value exception</exception>
        /// <exception cref="ArgumentOutOfRangeException">Null value input exception</exception>
        /// <exception cref="ArgumentException">Mapping key not found in <paramref name="mappingDictionary"/></exception>
        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string orderBy, Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (mappingDictionary is null)
            {
                throw new ArgumentOutOfRangeException(nameof(mappingDictionary));
            }

            if (string.IsNullOrWhiteSpace(orderBy))
            {
                return source;
            }

            var orderByString = string.Empty;

            // the orderBy string is separated by ",", so we split it.
            var orderByAfterSplit = orderBy.Split(',');

            // apply each orderby clause  
            foreach (var orderByClause in orderByAfterSplit.Reverse())
            {
                // trim the orderBy clause, as it might contain leading
                // or trailing spaces. Can't trim the var in foreach,
                // so use another var.
                var trimmedOrderByClause = orderByClause.Trim();

                // if the sort option ends with with " desc", we order
                // descending, ortherwise ascending
                var orderDescending = trimmedOrderByClause.EndsWith(" desc");

                // remove " asc" or " desc" from the orderBy clause, so we 
                // get the property name to look for in the mapping dictionary
                var indexOfFirstSpace = trimmedOrderByClause.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1 ?
                    trimmedOrderByClause : trimmedOrderByClause.Remove(indexOfFirstSpace);

                // find the matching property
                if (!mappingDictionary.ContainsKey(propertyName))
                {
                    throw new ArgumentException($"Key mapping for {propertyName} is missing");
                }

                // get the PropertyMappingValue
                var propertyMappingValue = mappingDictionary[propertyName];

                if (propertyMappingValue == null)
                {
                    throw new ArgumentNullException(null,nameof(propertyMappingValue));
                }

                // Run through the property names 
                // so the orderby clauses are applied in the correct order
                foreach (var destinationProperty in
                    propertyMappingValue.DestinationProperties)
                {
                    // revert sort order if necessary
                    if (propertyMappingValue.Revert)
                    {
                        orderDescending = !orderDescending;
                    }

                    orderByString = orderByString +
                        (string.IsNullOrWhiteSpace(orderByString) ? string.Empty : ", ")
                        + destinationProperty
                        + (orderDescending ? " descending" : " ascending");
                }
            }

            return source.OrderBy(orderByString);
        }

        /// <summary>
        /// Apply Filter extension method
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="source">Object itself</param>
        /// <param name="filterBy">filterby string</param>
        /// <param name="mappingDictionary">Mapping dictionary that contains valid mappings</param>
        /// <returns>IQueryable of T</returns>
        /// <exception cref="ArgumentNullException">Null value exception</exception>
        /// <exception cref="ArgumentOutOfRangeException">Null value input exception</exception>
        /// <exception cref="ArgumentException">Mapping key not found in <paramref name="mappingDictionary"/></exception>
        /// <exception cref="BadHttpRequestException">Thrown when filtering fails</exception>
        public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> source, string filterBy, Dictionary<string, PropertyMappingValue> mappingDictionary)
        {

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (mappingDictionary is null)
            {
                throw new ArgumentOutOfRangeException(nameof(mappingDictionary));
            }

            if (string.IsNullOrWhiteSpace(filterBy))
            {
                return source;
            }


            // the filterby string is separated by ",", so we split it.
            var filterByAfterSplit = filterBy.Split(',');

            // apply each filterby clause  
            foreach (var filterByClause in filterByAfterSplit.Reverse())
            {
                // remove all the whitespaces in the filterby clause. Can't trim the var in foreach,
                // so use another var.
                string? trimmedFilterByClause = filterByClause.RemoveWhiteSpaces();


                // get the property name to look for in the mapping dictionary
                var indexOfFirstSpace = trimmedFilterByClause.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1 ?
                    trimmedFilterByClause : trimmedFilterByClause.Remove(indexOfFirstSpace);

                var filterOperation = propertyName.FilterBySeperation();

                //set prorpery
                var propertyMappingValue = mappingDictionary[filterOperation.PropertyName];
                if (propertyMappingValue == null)
                {
#pragma warning disable CA2208 // Instantiate argument exceptions correctly
                    throw new ArgumentNullException("propertyMappingValue");
#pragma warning restore CA2208 // Instantiate argument exceptions correctly
                }

                // find the matching property
                if (!mappingDictionary.ContainsKey(filterOperation.PropertyName))
                {
                    throw new ArgumentException($"Key mapping for {filterOperation.PropertyName} is missing");
                }

                foreach (var destinationProperty in
                   propertyMappingValue.DestinationProperties)
                {
                    filterBy = $"{destinationProperty} {filterOperation.Operator} \"{filterOperation.Value}\"";

                    try
                    {
                        source = source.Where(filterBy);

                    }
                    catch (Exception)
                    {

                        throw new BadHttpRequestException("Could not filter correctly, please change the filter value and try again.", 400);
                    }

                    //source = source.Where("StatusId == \"3\"");
                }
            }
            return source;

        }
    }
}
