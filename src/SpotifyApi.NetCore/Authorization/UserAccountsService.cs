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
        private readonly ITokenStore<BearerAccessRefreshToken> _userTokenStore;
        private readonly string _scopes;

        public UserAccountsService(HttpClient httpClient, IConfiguration configuration, 
            ITokenStore<BearerAccessRefreshToken> userTokenStore, string[] scopes) : base(httpClient, configuration)
        {
            //TODO: when does this get called?
            ValidateConfig();

            if (_userTokenStore == null) throw new ArgumentNullException("userTokenStore");
            _userTokenStore = userTokenStore;

            _scopes = scopes == null || scopes.Length == 0 ? "" : string.Join(" ", scopes);
        }

        public UserAccountsService(ITokenStore<BearerAccessRefreshToken> userTokenStore, string[] scopes) : 
            base(new HttpClient(), null) { }

        public async Task<BearerAccessToken> GetUserAccessToken(string userHash)
        {
            var token = await _userTokenStore.Get(userHash);
            if (token == null || string.IsNullOrEmpty(token.RefreshToken)) 
                throw new UnauthorizedAccessException($"No refresh token found for user \"{userHash}\"");

            // if token current, return it
            var now = DateTime.UtcNow;
            if (token.Expires != null && token.Expires > now) return token;

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
            newToken.EnforceInvariants();
            await _userTokenStore.Update(userHash, newToken);
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

        private void ValidateConfig()
        {
            if (string.IsNullOrEmpty(_config["SpotifyAuthRedirectUri"]))
                throw new ArgumentNullException("SpotifyAuthRedirectUri", "Expecting configuration value for `SpotifyAuthRedirectUri`");
        }

    }
}
