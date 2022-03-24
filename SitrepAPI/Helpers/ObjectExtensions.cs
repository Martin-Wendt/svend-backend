using System.Dynamic;
using System.Reflection;

namespace SitrepAPI.Helpers
{
    /// <summary>
    /// Object Extension
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Shapes data to only include wanted properties
        /// </summary>
        /// <typeparam name="TSource">Type of object</typeparam>
        /// <param name="source">object to shape</param>
        /// <param name="fields">fields/propertrie to include</param>
        /// <returns>ExpandoObject</returns>
        /// <exception cref="ArgumentNullException">Null input values</exception>
        /// <exception cref="Exception">field/property wanted includes is not present</exception>
        public static ExpandoObject ShapeData<TSource>(this TSource source,
             string fields)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var dataShapedObject = new ExpandoObject();

            if (string.IsNullOrWhiteSpace(fields))
            {
                // all public properties should be in the ExpandoObject 
                var propertyInfos = typeof(TSource)
                        .GetProperties(BindingFlags.IgnoreCase |
                        BindingFlags.Public | BindingFlags.Instance);

                foreach (var propertyInfo in propertyInfos)
                {
                    // get the value of the property on the source object
                    var propertyValue = propertyInfo.GetValue(source);

                    if (propertyValue != null)
                    {
                    // add the field to the ExpandoObject
                    (dataShapedObject as IDictionary<string, object>)
                        .Add(propertyInfo.Name, propertyValue);
                    }
                }

                return dataShapedObject;
            }

            // the field are separated by ",", so we split it.
            var fieldsAfterSplit = fields.Split(',');

            foreach (var field in fieldsAfterSplit)
            {
                // trim each field, as it might contain leading 
                // or trailing spaces. Can't trim the var in foreach,
                // so use another var.
                var propertyName = field.Trim();

                // use reflection to get the property on the source object
                // we need to include public and instance, b/c specifying a 
                // binding flag overwrites the already-existing binding flags.
                var propertyInfo = typeof(TSource)
                    .GetProperty(propertyName,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo == null)
                {
                    throw new Exception($"Property {propertyName} wasn't found " +
                        $"on {typeof(TSource)}");
                }

                // get the value of the property on the source object
                var propertyValue = propertyInfo.GetValue(source);

                // add the field to the ExpandoObject
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
                ((IDictionary<string, object>)dataShapedObject)
                    .Add(propertyInfo.Name, propertyValue);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
#pragma warning restore CS8604 // Possible null reference argument.
            }

            // return the list
            return dataShapedObject;
        }
    }
}
