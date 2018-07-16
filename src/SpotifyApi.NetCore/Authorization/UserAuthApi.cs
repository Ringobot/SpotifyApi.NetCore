using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SpotifyApi.NetCore.Http;

namespace SpotifyApi.NetCore
{

    public class UserAuthApi : IAuthorizationApi
    {
        private readonly IUserAuthData _data;
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public UserAuthApi(HttpClient httpClient, IConfiguration configuration, IUserAuthData data)
        {
            if (httpClient == null) throw new ArgumentNullException("httpClient");
            if (data == null) throw new ArgumentNullException("data");
            _http = httpClient;
            _data = data;

            // if configuration is not provided, read from environment variables
            _config = configuration ?? new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();
        }

        public UserAuthApi(HttpClient httpClient, IUserAuthData data) : this(httpClient, null, data)
        {}

        public void ValidateConfig()
        {
            if (_config["SpotifyApiClientId"] == null)
                throw new ArgumentNullException("SpotifyApiClientId", "Expecting configuration value for `SpotifyApiClientId`");
            if (_config["SpotifyApiClientSecret"] == null)
                throw new ArgumentNullException("SpotifyApiClientSecret", "Expecting configuration value for `SpotifyApiClientSecret`");
            if (_config["SpotifyAuthRedirectUri"] == null)
                throw new ArgumentNullException("SpotifyAuthRedirectUri", "Expecting configuration value for `SpotifyAuthRedirectUri`");
        }

        public Task<string> GetAccessToken()
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetAccessToken(string userHash)
        {
            ValidateConfig();

            // get userAuth record
            var userAuth = await _data.Get(userHash);
            if (userAuth == null) throw new UnauthorizedAccessException($"No user auth record found for user \"{userHash}\"");

            if (userAuth.Expiry == null || userAuth.Expiry < DateTime.Now)
            {
                // if expired, refresh the token
                var now = DateTime.Now;

                var json = await _http.Post(AuthorizationApiHelper.TokenUrl,
                    $"grant_type=refresh_token&refresh_token={userAuth.RefreshToken}&redirect_uri={_config["SpotifyAuthRedirectUri"]}",
                    AuthorizationApiHelper.GetHeader(_config));

                // deserialise the token
                //TODO: Deserilaize to DTO?
                dynamic tokenData = JsonConvert.DeserializeObject(json);

                var expires = DateTime.Now.AddMilliseconds(now.AddSeconds(Convert.ToInt32(tokenData.expires_in)));
                userAuth.AccessToken = tokenData.access_token;
                userAuth.Expiry = expires;

                // update the userAuth record
                await _data.Update(userAuth);
            }

            return userAuth.AccessToken;
        }
    }
    public interface IUserAuthEntity
    {
        DateTime? Expiry { get; set; }
        string RefreshToken { get; set; }
        string AccessToken { get; set; }
    }

    public interface IUserAuthData
    {
        Task<IUserAuthEntity> Get(string userHash);
        Task Update(IUserAuthEntity userAuth);
    }
}