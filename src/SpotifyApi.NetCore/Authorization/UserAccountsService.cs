using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SpotifyApi.NetCore.Http;

namespace SpotifyApi.NetCore
{
    public class UserAccountsService : AccountsService, IUserAccountsService
    {
        private const string AccountsAuthorizeUrl = "https://accounts.spotify.com/authorize";

        private readonly IRefreshTokenProvider _refreshTokenProvider;

        public UserAccountsService(
            HttpClient httpClient,
            IConfiguration configuration,
            IRefreshTokenProvider refreshTokenProvider,
            IBearerTokenStore bearerTokenStore
            ) : base(httpClient, configuration, bearerTokenStore)
        {
            //TODO: when does this get called?
            ValidateConfig();

            if (refreshTokenProvider == null) throw new ArgumentNullException("userTokenStore");
            _refreshTokenProvider = refreshTokenProvider;
        }

        public UserAccountsService(
            HttpClient httpClient,
            IConfiguration configuration,
            IRefreshTokenProvider refreshTokenProvider
            ) : this(new HttpClient(), configuration, refreshTokenProvider, null) { }

        public UserAccountsService(
            IRefreshTokenProvider refreshTokenProvider
            ) : this(new HttpClient(), null, refreshTokenProvider, null) { }

        public async Task<BearerAccessToken> GetUserAccessToken(string userHash)
        {
            // get the refresh token for this user
            string refreshToken = await _refreshTokenProvider.GetRefreshToken(userHash);
            if (string.IsNullOrEmpty(refreshToken))
                throw new UnauthorizedAccessException($"No refresh token found for user \"{userHash}\"");

            return await GetAccessToken(userHash, 
                $"grant_type=refresh_token&refresh_token={refreshToken}&redirect_uri={_config["SpotifyAuthRedirectUri"]}");
        }

        public string AuthorizeUrl(string state, string[] scopes)
        {
            string scope = scopes == null || scopes.Length == 0 ? "" : string.Join(" ", scopes);
            return $"{AccountsAuthorizeUrl}/?client_id={_config["SpotifyApiClientId"]}&response_type=code&redirect_uri={_config["SpotifyAuthRedirectUri"]}&scope={scope}&state={state}";
        }
        public async Task<BearerAccessRefreshToken> RequestAccessRefreshToken(string userHash, string code)
        {
            var now = DateTime.UtcNow;
            // POST the code to get the tokens
            var token = await GetAuthorizationTokens(code);
            // set absolute expiry
            token.SetExpires(now);
            token.EnforceInvariants();
            await _bearerTokenStore.InsertOrReplace(userHash, token);
            return token;
        }

        protected internal virtual async Task<BearerAccessRefreshToken> GetAuthorizationTokens(string code)
        {
            var result = await _http.Post(TokenUrl,
                $"grant_type=authorization_code&code={code}&redirect_uri={_config["SpotifyAuthRedirectUri"]}",
                GetHeader(_config));
            return JsonConvert.DeserializeObject<BearerAccessRefreshToken>(result);
        }

        private void ValidateConfig()
        {
            if (string.IsNullOrEmpty(_config["SpotifyAuthRedirectUri"]))
                throw new ArgumentNullException("SpotifyAuthRedirectUri", "Expecting configuration value for `SpotifyAuthRedirectUri`");
        }

    }
}
