using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SpotifyApi.NetCore.Authorization;
using SpotifyApi.NetCore.Http;

namespace SpotifyApi.NetCore
{
    public class UserAccountsService : AccountsService, IUserAccountsService
    {
        private const string AccountsAuthorizeUrl = "https://accounts.spotify.com/authorize";

        private readonly IRefreshTokenStore _refreshTokenStore;

        public UserAccountsService(
            HttpClient httpClient,
            IConfiguration configuration,
            IRefreshTokenStore refreshTokenStore,
            IBearerTokenStore bearerTokenStore
            ) : base(httpClient, configuration, bearerTokenStore)
        {
            //TODO: when does this get called?
            ValidateConfig();

            if (refreshTokenStore == null) throw new ArgumentNullException("userTokenStore");
            _refreshTokenStore = refreshTokenStore;
        }

        public UserAccountsService(
            HttpClient httpClient,
            IConfiguration configuration,
            IRefreshTokenStore refreshTokenStore
            ) : this(new HttpClient(), configuration, refreshTokenStore, null) { }

        public UserAccountsService(
            IRefreshTokenStore refreshTokenStore
            ) : this(new HttpClient(), null, refreshTokenStore, null) { }

        // single user constructors
        public UserAccountsService(
            HttpClient httpClient,
            IConfiguration configuration,
            (string userHash, string token) userRefreshToken
            ) : base(httpClient, configuration, null)
        { 
            ValidateConfig();

            // initialise a token store with a single user's refresh token
            _refreshTokenStore = new MemoryRefreshTokenStore();
            _refreshTokenStore.InsertOrReplace(userRefreshToken.userHash, userRefreshToken.token);
        }

        public UserAccountsService(
            (string userHash, string token) userRefreshToken
            ) : base(new HttpClient(), null, null)
        { 
            ValidateConfig();

            // initialise a token store with a single user's refresh token
            _refreshTokenStore = new MemoryRefreshTokenStore();
            _refreshTokenStore.InsertOrReplace(userRefreshToken.userHash, userRefreshToken.token);
        }

        public async Task<BearerAccessToken> GetUserAccessToken(string userHash)
        {
            // get the refresh token for this user
            string refreshToken = await _refreshTokenStore.Get(userHash);
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
            await _refreshTokenStore.InsertOrReplace(userHash, token.RefreshToken);
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
