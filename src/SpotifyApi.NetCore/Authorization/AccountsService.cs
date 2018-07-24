using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using SpotifyApi.NetCore;
using SpotifyApi.NetCore.Authorization;
using SpotifyApi.NetCore.Http;

namespace SpotifyApi.NetCore
{
    public class AccountsService : IAccountsService
    {
        protected readonly HttpClient _http;
        protected readonly IConfiguration _config;
        protected internal virtual ITokenStore<BearerAccessToken> _appTokenStore { get; set; }

        public AccountsService(HttpClient httpClient, IConfiguration configuration)
        {
            if (httpClient == null) throw new ArgumentNullException("httpClient");
            _http = httpClient;

            // if configuration is not provided, read from environment variables
            _config = configuration ?? new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();
            ValidateConfig();

            _appTokenStore = new MemoryTokenStore<BearerAccessToken>();
        }

        public AccountsService() : this(new HttpClient(), null) { }

        public async Task<BearerAccessToken> GetAppAccessToken()
        {
            var token = await _appTokenStore.Get(_config["SpotifyApiClientId"]);

            // if token current, return it
            var now = DateTime.UtcNow;
            if (token != null && token.Expires != null && token.Expires > now) return token;

            string json = await _http.Post(AuthHelper.TokenUrl,
                "grant_type=client_credentials",
                AuthHelper.GetHeader(_config));

            // deserialise the token
            var newToken = JsonConvert.DeserializeObject<BearerAccessToken>(json);
            // set absolute expiry
            newToken.SetExpires(now);

            // add to store
            newToken.EnforceInvariants();
            await _appTokenStore.InsertOrReplace(_config["SpotifyApiClientId"], newToken);
            return newToken;
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
