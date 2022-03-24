namespace SitrepAPI.Models
{
    /// <summary>
    /// Filter operation seprator model
    /// </summary>
    public class FilterOperation
    {
        /// <summary>
        /// Property name 
        /// </summary>
        public string PropertyName { get;private set; }
        /// <summary>
        /// Operator
        /// </summary>
        public string Operator { get; private set; }
        /// <summary>
        /// Value
        /// </summary>
        public string Value { get;private set; }

        /// <summary>
        /// Constructor of class
        /// </summary>
        /// <param name="propertyName">Property</param>
        /// <param name="operator">Operator</param>
        /// <param name="value">Value</param>
        public FilterOperation(string propertyName, string @operator, string value)
        {
            PropertyName = propertyName;
            Operator = @operator;
            Value = value;
        }
    }
}
