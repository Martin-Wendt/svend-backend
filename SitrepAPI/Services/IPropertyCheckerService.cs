namespace SitrepAPI.Services
{
    /// <summary>
    /// Interface for propertychecking
    /// </summary>
    public interface IPropertyCheckerService
    {
        /// <summary>
        /// properties to check for
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="fields">properties to check for</param>
        /// <returns></returns>
        bool TypeHasProperties<T>(string fields);
    }
}