using SitrepAPI.Models.Auth0;

namespace SitrepAPI.Services
{
    /// <summary>
    /// User Information service
    /// </summary>
    public interface IUserInformationService
    {
        /// <summary>
        /// Get Auth0 user information
        /// </summary>
        /// <param name="userId">Id of User</param>
        /// <returns> returns <see cref="Auth0UserDTO"/> </returns>
        public Task<Auth0UserDTO> GetUserInformationAsync(string userId);

    }
}
