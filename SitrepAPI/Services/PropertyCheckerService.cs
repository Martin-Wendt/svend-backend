using System.Reflection;

namespace SitrepAPI.Services
{
    /// <summary>
    /// Implementation of Propertry checking service
    /// </summary>
    public class PropertyCheckerService : IPropertyCheckerService
    {
        /// <summary>
        /// <see cref="IPropertyCheckerService.TypeHasProperties{T}(string)"/>
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="fields">Properties to check for</param>
        /// <returns>Bool</returns>
        public bool TypeHasProperties<T>(string fields)
        {
            if (string.IsNullOrWhiteSpace(fields))
            {
                return true;
            }

            var fieldsAfterSplit = fields.Split(',');

            foreach (var field in fieldsAfterSplit)
            {
                var propertyName = field.Trim();

                var propertyInfo = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo == null)
                {
                    return false;
                }

            }
            return true;
        }
    }
}
