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
    public class UserAccountsService : AccountsService, IUserAccountsService
    {
        private readonly IRefreshTokenStore _refreshTokenStore;
        private readonly string _scopes;

        public UserAccountsService(
            HttpClient httpClient, 
            IConfiguration configuration, 
            IRefreshTokenStore refreshTokenStore, 
            IBearerTokenStore bearerTokenStore, 
            string[] scopes
            ) : base(httpClient, configuration, bearerTokenStore)
        {
            //TODO: when does this get called?
            ValidateConfig();

            if (refreshTokenStore == null) throw new ArgumentNullException("userTokenStore");
            _refreshTokenStore = refreshTokenStore;

            _scopes = scopes == null || scopes.Length == 0 ? "" : string.Join(" ", scopes);
        }

        public UserAccountsService(
            HttpClient httpClient, 
            IConfiguration configuration, 
            IRefreshTokenStore refreshTokenStore, 
            string[] scopes
            ) : this(new HttpClient(), configuration, refreshTokenStore, null, scopes)  { }

        public UserAccountsService(
            IRefreshTokenStore refreshTokenStore, 
            string[] scopes
            ) : this(new HttpClient(), null, refreshTokenStore, null, scopes)  { }

        public async Task<BearerAccessToken> GetUserAccessToken(string userHash)
        {
            var token = await _bearerTokenStore.Get(userHash);

            // if token current, return it
            var now = DateTime.UtcNow;
            if (token != null && token.Expires != null && token.Expires > now) return token;

            // get the refresh token for this user
            string refreshToken = await _refreshTokenStore.Get(userHash);
            if (string.IsNullOrEmpty(refreshToken)) 
                throw new UnauthorizedAccessException($"No refresh token found for user \"{userHash}\"");

            string json = await _http.Post(AuthHelper.TokenUrl, 
                $"grant_type=refresh_token&refresh_token={refreshToken}&redirect_uri={_config["SpotifyAuthRedirectUri"]}",
                AuthHelper.GetHeader(_config));

            // deserialise the token
            var newToken = JsonConvert.DeserializeObject<BearerAccessToken>(json);
            // set absolute expiry
            newToken.SetExpires(now);

            // add to store
            newToken.EnforceInvariants();
            await _bearerTokenStore.InsertOrReplace(userHash, newToken);
            return newToken;
        }

        public string AuthorizeUrl(string state) => $"{AuthHelper.AuthorizeUrl}/?client_id={_config["SpotifyApiClientId"]}&response_type=code&redirect_uri={_config["SpotifyAuthRedirectUri"]}&scope={_scopes}&state={state}";

        public async Task<BearerAccessRefreshToken> RequestAccessRefreshToken(string userHash, string code)
        {
            var now = DateTime.UtcNow;
            // POST the code to get the tokens
            var token = await GetAuthorizationTokens(code);
            // set absolute expiry
            token.SetExpires(now);
            token.EnforceInvariants();
            await _refreshTokenStore.InsertOrReplace(userHash, token.RefreshToken);
            await _bearerTokenStore.InsertOrReplace(userHash, token);
            return token;
        }

        protected internal virtual async Task<BearerAccessRefreshToken> GetAuthorizationTokens(string code)
        {
            var result = await _http.Post(AuthHelper.TokenUrl,
                $"grant_type=authorization_code&code={code}&redirect_uri={_config["SpotifyAuthRedirectUri"]}",
                AuthHelper.GetHeader(_config));
            return JsonConvert.DeserializeObject<BearerAccessRefreshToken>(result);
        }

        private void ValidateConfig()
        {
            if (string.IsNullOrEmpty(_config["SpotifyAuthRedirectUri"]))
                throw new ArgumentNullException("SpotifyAuthRedirectUri", "Expecting configuration value for `SpotifyAuthRedirectUri`");
        }

    }
}
