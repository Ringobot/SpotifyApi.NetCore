using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SpotifyApi.NetCore.Authorization;
using SpotifyApi.NetCore.Http;

//TODO: Move to SpotifyApi.NetCore.Authorization in vNext
namespace SpotifyApi.NetCore
{
    public class AccountsService : IAccountsService, IAccessTokenProvider
    {
        protected const string TokenUrl = "https://accounts.spotify.com/api/token";

        protected readonly HttpClient _http;
        protected readonly IConfiguration _config;
        protected readonly IBearerTokenStore _bearerTokenStore;

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

        public AccountsService() : this(new HttpClient(), null, null) { }
        public AccountsService(HttpClient httpClient) : this(httpClient, null, null) { }
        public AccountsService(HttpClient httpClient, IConfiguration configuration) : this(new HttpClient(), configuration, null) { }

        public async Task<BearerAccessToken> GetAppAccessToken()
        {
            return await GetAccessToken(_config["SpotifyApiClientId"], "grant_type=client_credentials");
        }

        protected async Task<BearerAccessToken> GetAccessToken(string tokenKey, string body)
        {
            var token = await _bearerTokenStore.Get(tokenKey);

            // if token current, return it
            var now = DateTime.UtcNow;
            if (token != null && token.Expires != null && token.Expires > now) return token;

            string json = await _http.Post(TokenUrl, body, GetHeader(_config));

            // deserialise the token
            var newToken = JsonConvert.DeserializeObject<BearerAccessToken>(json);
            // set absolute expiry
            newToken.SetExpires(now);

            // add to store
            newToken.EnforceInvariants();
            await _bearerTokenStore.InsertOrReplace(tokenKey, newToken);
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

        public async Task<string> GetAccessToken() => (await GetAppAccessToken()).AccessToken;
    }
}
