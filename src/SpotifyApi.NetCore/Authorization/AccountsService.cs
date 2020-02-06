using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SpotifyApi.NetCore.Http;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore.Authorization
{
    /// <summary>
    /// Provides Spotify Accounts Service functionality as described in <see cref="https://developer.spotify.com/documentation/general/guides/authorization-guide/#client-credentials-flow"/>
    /// </summary>
    public class AccountsService : IAccountsService
    {
        protected const string TokenUrl = "https://accounts.spotify.com/api/token";

        protected readonly HttpClient _http;
        protected readonly IConfiguration _config;
        protected readonly IBearerTokenStore _bearerTokenStore;

        #region constructors

        /// <summary>
        /// Instantiates an AccountsService class.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/> for making HTTP calls to the Spotify Accounts Service.</param>
        /// <param name="configuration">An instance of <see cref="IConfiguration"/> for providing Configuration.</param>
        /// <param name="bearerTokenStore">An instance of <see cref="IBearerTokenStore"/> for storing cached Access (Bearer) tokens.</param>
        public AccountsService(HttpClient httpClient, IConfiguration configuration, IBearerTokenStore bearerTokenStore)
        {
            if (httpClient == null) throw new ArgumentNullException("httpClient");
            _http = httpClient;

            // if configuration is not provided, read from environment variables
            _config = configuration ?? new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            ValidateConfig();

            _bearerTokenStore = bearerTokenStore ?? new MemoryBearerTokenStore();
        }

        /// <summary>
        /// Instantiates an AccountsService class.
        /// </summary>
        public AccountsService() : this(new HttpClient(), null, null) { }

        /// <summary>
        /// Instantiates an AccountsService class.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/> for making HTTP calls to the Spotify Accounts Service.</param>
        public AccountsService(HttpClient httpClient) : this(httpClient, null, null) { }

        /// <summary>
        /// Instantiates an AccountsService class.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/> for making HTTP calls to the Spotify Accounts Service.</param>
        /// <param name="configuration">An instance of <see cref="IConfiguration"/> for providing Configuration.</param>
        public AccountsService(HttpClient httpClient, IConfiguration configuration) : this(httpClient, configuration, null) { }

        #endregion

        /// <summary>
        /// Returns a valid access token for the Spotify Service.
        /// </summary>
        /// <returns>The token as string.</returns>
        public async Task<string> GetAccessToken()
        {
            var token = await GetAccessToken(_config["SpotifyApiClientId"], "grant_type=client_credentials");
            return token.AccessToken;
        }

        protected async Task<BearerAccessToken> GetAccessToken(string tokenKey, string body)
        {
            var token = await _bearerTokenStore.Get(tokenKey);

            // if token current, return it
            var now = DateTime.UtcNow;
            if (token != null && token.Expires != null && token.Expires > now) return token;

            string json = await _http.Post(new Uri(TokenUrl), body, GetHeader(_config));

            // deserialise the token
            var newToken = JsonConvert.DeserializeObject<BearerAccessToken>(json);
            // set absolute expiry
            newToken.SetExpires(now);

            // add to store
            newToken.EnforceInvariants();
            await _bearerTokenStore.InsertOrReplace(tokenKey, newToken);
            return newToken;
        }

        protected async Task<BearerAccessToken> RefreshAccessToken(string body)
        {
            var now = DateTime.UtcNow;
            string json = await _http.Post(new Uri(TokenUrl), body, GetHeader(_config));
            // deserialise the token
            var newToken = JsonConvert.DeserializeObject<BearerAccessToken>(json);
            // set absolute expiry
            newToken.SetExpires(now);
            return newToken;
        }

        protected static AuthenticationHeaderValue GetHeader(IConfiguration configuration)
        {
            return new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}",
                    configuration["SpotifyApiClientId"], configuration["SpotifyApiClientSecret"])))
            );
        }

        private void ValidateConfig()
        {
            if (string.IsNullOrEmpty(_config["SpotifyApiClientId"]))
                throw new ArgumentNullException("SpotifyApiClientId", "Expecting configuration value for `SpotifyApiClientId`");
            if (string.IsNullOrEmpty(_config["SpotifyApiClientSecret"]))
                throw new ArgumentNullException("SpotifyApiClientSecret", "Expecting configuration value for `SpotifyApiClientSecret`");
        }
    }
}
