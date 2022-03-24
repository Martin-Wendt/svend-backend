using Hellang.Middleware.ProblemDetails;

namespace SitrepAPI.ProblemDetails
{
    /// <summary>
    /// Configurations of custom exception => http responses
    /// </summary>
    public static class ProblemDetailConfiguration
    {
        /// <summary>
        /// Exception to ProblemDetail mapping
        /// </summary>
        /// <param name="options">DI parameter</param>
        public static void ProblemDetailConfigurationExecution(ProblemDetailsOptions options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }
        }
    }
}
