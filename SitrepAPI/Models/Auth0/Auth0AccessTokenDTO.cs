using Newtonsoft.Json;

namespace SitrepAPI.Models.Auth0
{
    /// <summary>
    /// Representation of access token needed to communicate with auth0 userinformation endpoint
    /// </summary>
    public class Auth0AccessTokenDTO
    {
        /// <summary>
        /// access token
        /// </summary>
        [JsonProperty("access_token")]
        public string? AccessToken { get; set; }

        /// <summary>
        /// scope of token
        /// </summary>
        [JsonProperty("scope")]
        public string? Scope { get; set; }

        /// <summary>
        /// token expiration in seconds from retrival time
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// type of token
        /// </summary>
        [JsonProperty("token_type")]
        public string? TokenType { get; set; }

        /// <summary>
        /// Creation time of token
        /// </summary>
        public DateTimeOffset CreatedAt { get; private set; }

        /// <summary>
        /// constructor of class
        /// </summary>
        public Auth0AccessTokenDTO()
        {
            CreatedAt = DateTimeOffset.Now;
        }

        /// <summary>
        /// Is token expired
        /// </summary>
        /// <returns>bool</returns>
        public bool IsExpired()
        {
            return CreatedAt.AddSeconds(ExpiresIn) < DateTimeOffset.Now;
        }
    }

}
