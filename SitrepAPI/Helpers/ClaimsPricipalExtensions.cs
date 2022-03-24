using System.Security.Claims;

namespace SitrepAPI.Helpers
{
    /// <summary>
    /// Static string extension method
    /// </summary>
    public static class ClaimsPricipalExtensions
    {
        /// <summary>
        /// Removes Auth0 prefix from userId
        /// </summary>
        /// <param name="source">string</param>
        /// <returns>string</returns>
        public static string GetUserId(this ClaimsPrincipal source)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return source.Identity.Name.Split('|')[1];
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}
