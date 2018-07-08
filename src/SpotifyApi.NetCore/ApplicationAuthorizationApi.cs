using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpotifyApi.NetCore.Cache;
using SpotifyApi.NetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// An API wrapper for the Spotify Authorization API, Client Credentials flow.
    /// </summary>
    /// <remarks>https://developer.spotify.com/web-api/authorization-guide/#client-credentials-flow</remarks>
    public class ApplicationAuthorizationApi : IAuthorizationApi
    {
        private readonly ICache _cache;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Instantiates a new <see cref="ApplicationAuthorizationApi"/> object.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/>.</param>
        /// <param name="configuration"></param>
        /// <param name="cache">An instance of <see cref="ICache"/> to cache the Bearer token
        public ApplicationAuthorizationApi(HttpClient httpClient, IConfiguration configuration, ICache cache)
        {
            if (httpClient == null) throw new ArgumentNullException("httpClient");
            if (cache == null) throw new ArgumentNullException("cache");

            // if configuration is not provided, read from environment variables
            _configuration = configuration ?? new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();
            
            _httpClient = httpClient;
            _cache = cache;
        }

        /// <summary>
        /// Instantiates a new <see cref="ApplicationAuthorizationApi"/> object.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/>.</param>
        /// <param name="configuration"></param>
        public ApplicationAuthorizationApi(HttpClient httpClient, IConfiguration configuration):
            this(httpClient, configuration, new RuntimeMemoryCache(new MemoryCache(new MemoryCacheOptions())))
            {}

        /// <summary>
        /// Instantiates a new <see cref="ApplicationAuthorizationApi"/> object.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/>.</param>
        /// <param name="configuration"></param>
        public ApplicationAuthorizationApi(HttpClient httpClient):
            this(httpClient, null, new RuntimeMemoryCache(new MemoryCache(new MemoryCacheOptions())))
            {}


        /// <summary>
        /// Returns a bearer access token to use for a Request to the Spotify API.
        /// </summary>
        /// <returns>A Bearer token as (awaitable) Task of string</returns>
        public async Task<string> GetAccessToken()
        {
            const string cacheKey = "Radiostr.SpotifyWebApi.ClientCredentialsAuthorizationApi.BearerToken";

            var token = _cache == null ? null : (string) _cache.Get(cacheKey);

            if (token == null)
            {
                // post client ID and Secret to get bearer token
                string clientId = _configuration["SpotifyApiClientId"];
                string clientSecret = _configuration["SpotifyApiClientSecret"];

                if (string.IsNullOrEmpty(clientId))
                    throw new InvalidOperationException("AppSetting SpotifyApiClientId is not set.");
                if (string.IsNullOrEmpty(clientSecret))
                    throw new InvalidOperationException("AppSetting SpotifyApiClientSecret is not set.");

                // set Basic authentication header
                var header = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", clientId, clientSecret))));

                var now = DateTime.Now;
                const string url = "https://accounts.spotify.com/api/token";

                var json =
                    await
                        _httpClient.Post(url, "grant_type=client_credentials", header);

                // deserialise the token
                dynamic tokenData = JsonConvert.DeserializeObject(json);
                token = tokenData.access_token;

                // add to cache with an absolute expiry as indicated by Spotify
                if (_cache != null) _cache.Add(cacheKey, token, now.AddSeconds(Convert.ToInt32(tokenData.expires_in)));
            }

            return token;
        }

        public Task<string> GetAccessToken(string userHash)
        {
            throw new NotSupportedException();
        }
    }
}
