using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using SpotifyApi.NetCore;
using SpotifyApi.NetCore.Authorization;
using SpotifyApi.NetCore.Http;

namespace SpotifyApi.NetCore
{
    public class AuthorizationCodeService : IAuthorizationCodeService
    {
        private readonly IUserAuthData _data;
        private readonly HttpClient _http;
        private readonly IConfiguration _config;
        private readonly string _scopes;

        public AuthorizationCodeService(HttpClient httpClient, IConfiguration configuration, IUserAuthData data, string[] scopes)
        {
            if (httpClient == null) throw new ArgumentNullException("httpClient");
            if (data == null) throw new ArgumentNullException("data");
            _http = httpClient;
            _data = data;

            // if configuration is not provided, read from environment variables
            _config = configuration ?? new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            _scopes = scopes == null || scopes.Length == 0 ? "" : string.Join(" ", scopes);
        }

        public async Task<string> RequestAuthorizationUrl(string userHash)
        {
            AuthHelper.ValidateConfig(_config);

            string state = Guid.NewGuid().ToString("N");
            var userAuth = _data.Create(userHash, state);
            
            // test the DTO
            try
            {
                userAuth.AssertUserHashAndState(userHash, state);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"The DTO returned by {_data}.Create({userAuth}) is invalid. Check the implementation of `IUserAuthData`.", ex);
            }
            
            await _data.InsertOrReplace(userAuth);

            // State
            // 60BC0AC2A44EA6146D876AEC3133D230B9A9E41BACC7EA0343B16FED4CB6BE54|f26d60305
            // URI
            // https://accounts.spotify.com/authorize/?client_id=4b4a9fcb021a4d02a4acd1d8adba0bfe&response_type=code&redirect_uri=https%3A%2F%2Fexample.com%2Fcallback&scope=user-read-private%20user-read-email&state=34fFs29kd09
            return $"{AuthHelper.AuthorizeUrl}/?client_id={_config["SpotifyApiClientId"]}&response_type=code&redirect_uri={_config["SpotifyAuthRedirectUri"]}&scope={_scopes}&state={StateHelper.EncodeState(userAuth)}";
        }

        public async Task RequestToken(string queryString)
        {
            throw new NotImplementedException();

            /*
            let stateParam = req.query.state;

            if (!stateParam || stateParam.length === 0) {
                console.error("Expecting state in querystring");
                next(new errs.BadRequestError());
                return;
            }
                let error: string = req.query.error;

                if (error) {
                    // auth request was rejected or failed.
                    console.error(`Authorization failed. Spotify error = ${error}`);

                    // record error
                    entity.error = `${new Date()}: Authorization failed. Spotify error = ${error}`;
                    await _data.get(userHash);

                    next(new errs.UnauthorizedError(`Authorization failed: ${error}`));
                    return;
                }


             */
        }

        public async Task RequestToken(string state, string code)
        {
            // https://ringobot.azurewebsites.net/authorize/spotify?code=AQAYtuv4d6NsFQYBbYv-gmzI0K1_LDUjpBNe59yC1pID0Yl6LTLtcJ3kPtOu0jkRH4TxDCEXAAWbeoQ72DAmzug5LFnKyoP-cOT7NvzC4IMqlavrzgonrjSL_-B1uIA3uo8Lzgds1TWRaqPf304axiUc0ivvxWjQSjlRkj2rcHe2inCcoalRQEvAa4ZMvkVoZ7KFJcXERlGZkS17LkRIJnhuthVK55cfWGkTDgHWDWlamfz4Lb3uvQHElk-OVP6a1YTOn2IfxUGgFtAx7CC0Vqw5fS37L7ONdMmcTqDENpQ1wCGaRfHJ2b6t7JZx88DbrRiaH8KjYFShw4f2wDOI9wyEusOPhjnngCUGZu17kzcIeZcJFZRCO5hiSGcMTxb020m_mt7qK0PFJUOjHDT_gsHGsRz4dqwFjnOz12ThehFE8dvU7J9X9JOv4QeJ8Keg8kogz7keM76z1E8xi3svAUPd06m9-nsSCvZfNNS4G2QVGcOIFTsG3KyiIZkPfclNej7r&state=60BC0AC2A44EA6146D876AEC3133D230B9A9E41BACC7EA0343B16FED4CB6BE55%3A7dff3dc47b10121834e6715ae4deda2545cc8c59

            AuthHelper.ValidateConfig(_config);
            if (string.IsNullOrEmpty(state)) throw new ArgumentNullException("state");
            if (string.IsNullOrEmpty(code)) throw new ArgumentNullException("code");

            var (userHash, statePart) = StateHelper.DecodeState(state);
            var userAuth = await _data.Get(userHash);

            if (userAuth == null)
            {
                throw new InvalidOperationException($"No user auth record found for user \"{userHash}\"");
            }

            if (state != userAuth.State)
            {
                // states don't match
                StateHelper.ThrowStateException();
            }

            // update the userAuth record just in case we need to retry
            userAuth.Code = code;
            await _data.Update(userAuth);

            // POST the code to get the tokens
            var result = await GetUserAuthTokens(code);

            // Save the tokens in the auth table, delete the state and code
            userAuth.AccessToken = result.accessToken;
            userAuth.AuthUrl = result.authUrl;
            userAuth.TokenType = result.tokenType;
            userAuth.Scope = result.scope;
            userAuth.Expiry = result.expires;
            userAuth.RefreshToken = result.refreshToken;
            userAuth.State = null;
            userAuth.Code = null;

            await _data.Update(userAuth);
        }

        public async Task RequestToken(IEnumerable<KeyValuePair<String, StringValues>> queryCollection)
        {
            throw new NotImplementedException();

        }

        private async Task<dynamic> GetUserAuthTokens(string code)
        {
            return await _http.Post(AuthHelper.TokenUrl,
                $"grant_type=authorization_code&code={code}&redirect_uri=${_config["SpotifyAuthRedirectUri"]}",
                AuthHelper.GetHeader(_config));
        }

    }
}