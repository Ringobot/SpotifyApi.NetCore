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
using SpotifyApi.NetCore.Authorization;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// An API wrapper for the Spotify Authorization API, Client Credentials flow.
    /// </summary>
    /// <remarks>https://developer.spotify.com/web-api/authorization-guide/#client-credentials-flow</remarks>
    public class ApplicationAuthApi : IAuthorizationApi
    {
        private readonly ICache _cache;
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        /// <summary>
        /// Instantiates a new <see cref="ApplicationAuthApi"/> object.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/>.</param>
        /// <param name="configuration"></param>
        /// <param name="cache">An instance of <see cref="ICache"/> to cache the Bearer token
        public ApplicationAuthApi(HttpClient httpClient, IConfiguration configuration, ICache cache)
        {
            if (httpClient == null) throw new ArgumentNullException("httpClient");
            if (cache == null) throw new ArgumentNullException("cache");

            // if configuration is not provided, read from environment variables
            _config = configuration ?? new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();
            
            _http = httpClient;
            _cache = cache;
        }

        /// <summary>
        /// Instantiates a new <see cref="ApplicationAuthApi"/> object.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/>.</param>
        /// <param name="configuration"></param>
        public ApplicationAuthApi(HttpClient httpClient, IConfiguration configuration):
            this(httpClient, configuration, new RuntimeMemoryCache(new MemoryCache(new MemoryCacheOptions())))
            {}

        /// <summary>
        /// Instantiates a new <see cref="ApplicationAuthApi"/> object.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/>.</param>
        /// <param name="configuration"></param>
        public ApplicationAuthApi(HttpClient httpClient):
            this(httpClient, null, new RuntimeMemoryCache(new MemoryCache(new MemoryCacheOptions())))
            {}

        /// <summary>
        /// Returns a bearer access token to use for a Request to the Spotify API.
        /// </summary>
        /// <returns>A Bearer token as (awaitable) Task of string</returns>
        public async Task<string> GetAccessToken()
        {
            AuthHelper.ValidateConfig(_config, false);

            const string cacheKey = "Radiostr.SpotifyWebApi.ClientCredentialsAuthorizationApi.BearerToken";

            string token = _cache == null ? null : (string) _cache.Get(cacheKey);

            if (token == null)
            {
                var now = DateTime.Now;

                string json = await _http.Post(AuthHelper.TokenUrl, 
                    "grant_type=client_credentials", AuthHelper.GetHeader(_config));

                // deserialise the token
                //TODO: Deserilaize to DTO?
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
