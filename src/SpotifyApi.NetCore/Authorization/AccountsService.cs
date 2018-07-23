using System;
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
        private readonly ITokenStore<BearerAccessToken> _appTokenStore;
        private readonly ITokenStore<BearerAccessRefreshToken> _userTokenStore;
        private readonly HttpClient _http;
        private readonly IConfiguration _config;
        private readonly string _scopes;

        public AccountsService(HttpClient httpClient, IConfiguration configuration, ITokenStore<BearerAccessToken> appTokenStore,
            ITokenStore<BearerAccessRefreshToken> userTokenStore, string[] scopes)
        {
            if (httpClient == null) throw new ArgumentNullException("httpClient");
            _http = httpClient;

            if (_userTokenStore == null) throw new ArgumentNullException("userTokenStore");
            _userTokenStore = userTokenStore;

            //TODO: Default appTokenStore to Memory Cache
            _appTokenStore = appTokenStore;

            // if configuration is not provided, read from environment variables
            _config = configuration ?? new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            AuthHelper.ValidateConfig(_config);

            _scopes = scopes == null || scopes.Length == 0 ? "" : string.Join(" ", scopes);
        }

        public async Task<BearerAccessToken> GetAppAccessToken()
        {
            // retry loop
            // TODO: Polly
            for (int i = 0; i < 3; i++)
            {
                var token = _appTokenStore == null ? null : await _appTokenStore.Get(_config["SpotifyApiClientId"]);
                var now = DateTime.UtcNow;

                // if no token or it has expired
                if (token == null || token.Expires <= now)
                {
                    //TODO retry on certain failures
                    string json = await _http.Post(AuthHelper.TokenUrl, "grant_type=client_credentials", AuthHelper.GetHeader(_config));

                    // deserialise the token
                    var newToken = JsonConvert.DeserializeObject<BearerAccessToken>(json);
                    // set absolute expiry
                    newToken.SetExpires(now);

                    // add to store
                    if (_appTokenStore != null)
                    {
                        if (await _appTokenStore.TryUpdate(_config["SpotifyApiClientId"], newToken, token)) return newToken;
                    }
                }
            }

            throw new InvalidOperationException("Could not get Application Access Token after three attempts");

        }

        public async Task<BearerAccessToken> GetUserAccessToken(string userHash)
        {
            AuthHelper.ValidateConfig(_config);

            // retry loop
            // TODO: Polly
            for (int i = 0; i < 3; i++)
            {
                var token = await _userTokenStore.Get(userHash);
                if (token == null || string.IsNullOrEmpty(token.RefreshToken)) 
                    throw new UnauthorizedAccessException($"No refresh token found for user \"{userHash}\"");

                var now = DateTime.UtcNow;

                // if token has expired
                if (token.Expires == null || token.Expires <= now)
                {
                    //TODO retry on certain failures
                    string json = await _http.Post(AuthHelper.TokenUrl, 
                        $"grant_type=refresh_token&refresh_token={token.RefreshToken}&redirect_uri={_config["SpotifyAuthRedirectUri"]}",
                        AuthHelper.GetHeader(_config));

                    // deserialise the token
                    var newToken = JsonConvert.DeserializeObject<BearerAccessRefreshToken>(json);
                    // set absolute expiry
                    newToken.SetExpires(now);
                    // copy refresh token
                    newToken.RefreshToken = token.RefreshToken;

                    // add to store
                    if (_userTokenStore != null)
                    {
                        if (await _userTokenStore.TryUpdate(userHash, newToken, token)) return newToken;
                    }
                }
            }

            throw new InvalidOperationException("Could not get User Access Token after three attempts");
        }

        public string AuthorizeUrl(string state) => $"{AuthHelper.AuthorizeUrl}/?client_id={_config["SpotifyApiClientId"]}&response_type=code&redirect_uri={_config["SpotifyAuthRedirectUri"]}&scope={_scopes}&state={state}";

        public async Task<BearerAccessRefreshToken> RequestAccessRefreshToken(string userHash, string code)
        {
            var now = DateTime.UtcNow;
            // POST the code to get the tokens
            var token = await GetAuthorizationTokens(code);
            // set absolute expiry
            token.SetExpires(now);
            await _userTokenStore.InsertOrReplace(userHash, token);
            return token;
        }

        protected internal virtual async Task<BearerAccessRefreshToken> GetAuthorizationTokens(string code)
        {
            var result = await _http.Post(AuthHelper.TokenUrl,
                $"grant_type=authorization_code&code={code}&redirect_uri={_config["SpotifyAuthRedirectUri"]}",
                AuthHelper.GetHeader(_config));
            return JsonConvert.DeserializeObject<BearerAccessRefreshToken>(result);
        }
    }
}
