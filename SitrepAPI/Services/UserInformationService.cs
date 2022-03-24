using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using SitrepAPI.Models.Auth0;

namespace SitrepAPI.Services
{
    /// <summary>
    /// Service to get information about Auth0 user
    /// </summary>
    public class UserInformationService : IUserInformationService
    {
        private readonly RestClient _restClient;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;

        private DateTimeOffset _lastTry;
        private const int _tenMinutes = 600;
        private Auth0AccessTokenDTO _apikey;

        /// <summary>
        /// Constructor of class
        /// </summary>
        /// <param name="memoryCache">In memory cache service</param>
        /// <param name="configuration">Configuration service</param>
        /// <exception cref="ArgumentNullException">Dependency injection fail to provide services requested</exception>
        public UserInformationService(IMemoryCache memoryCache, IConfiguration configuration)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            RestClientOptions restClientOptions = new();
            restClientOptions.BaseUrl = new Uri("https://dev-gsehp47x.eu.auth0.com/");
            restClientOptions.Timeout = -1;

            _apikey = new Auth0AccessTokenDTO();

            _restClient = new RestClient(restClientOptions) ?? throw new ArgumentNullException(null, nameof(_restClient));
            _restClient.AddDefaultHeader("Content-Type", "application/json");

        }

        /// <summary>
        /// Gets Auth0 user information via API endpoint
        /// </summary>
        /// <param name="userId">Id of user</param>
        /// <returns>Auth0 user</returns>
        /// <exception cref="ArgumentNullException">failure to convert http response to object</exception>
        public async Task<Auth0UserDTO> GetUserInformationAsync(string userId)
        {
            Auth0UserDTO user = new();

            if (!_memoryCache.TryGetValue(userId, out user))
            {
                await ConfigureAuthAsync();

                var response = await GetUserFromAuth0Async(userId);
#pragma warning disable
                user = JsonConvert.DeserializeObject<Auth0UserDTO>(response?.Content);
#pragma warning restore
                if (user is null)
                {
                    throw new ArgumentNullException(null, nameof(user));
                }


                AddToCache(user, userId);
            }
            return user;
        }


        private async Task<RestResponse> GetUserFromAuth0Async(string userId)
        {
            var request = new RestRequest($"api/v2/users/{userId}", Method.Get);

            RestResponse response = await _restClient.ExecuteAsync<Auth0UserDTO>(request);

            if (response is null || response.Content is null)
            {
                throw new ArgumentNullException(nameof(response.Content), "Response is null");
            }

            return response;
        }

        private async Task ConfigureAuthAsync()
        {
            //no token
            if (_apikey is null || string.IsNullOrWhiteSpace(_apikey?.AccessToken))
            {
                await GetAccessToken();
            }
            //refresh old token
            else if (_lastTry.AddSeconds(_apikey.ExpiresIn - _tenMinutes) < DateTimeOffset.Now)
            {
                await GetAccessToken();
            }
        }

        private async Task GetAccessToken()
        {
            _lastTry = DateTimeOffset.Now;

            var request = new RestRequest($"/oauth/token", Method.Post);
            request.AddHeader("content-type", "application/json");
            var body = new
            {
                client_id = _configuration["Auth0:ClientId"],
                client_secret = _configuration["Auth0:ClientSecret"],
                audience = _configuration["Auth0:Aud"],
                grant_type = _configuration["Auth0:GrantType"]

            };
            request.AddBody(body, "application/json");
            var response = await _restClient.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
#pragma warning disable
                Auth0AccessTokenDTO token = JsonConvert.DeserializeObject<Auth0AccessTokenDTO>(response.Content);
                _apikey = token;
                _restClient.Authenticator = new JwtAuthenticator(_apikey.AccessToken);
#pragma warning restore
            }
        }

        private void AddToCache(Auth0UserDTO user, string userId)
        {
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromMinutes(2)
            };
            _memoryCache.Set(userId, user, cacheExpiryOptions);
        }
    }
}
