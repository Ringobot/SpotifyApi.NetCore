using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpotifyApi.NetCore.Authorization;
using SpotifyApi.NetCore.Tests.Mocks;

namespace SpotifyApi.NetCore.Tests.Integration
{
    [TestClass]
    public class AuthorizationCodeFlowTests 
    {
        UserAccountsService _accounts;
        //IRefreshTokenProvider _refreshTokenProvider;

        //const string UserHash = "E11AC28538A7C0A827A726DD9B30B710FC1FCAFFFE2E86FCA853AB90E7C710D2";

        [TestInitialize]
        public void Initialize()
        {
            //_refreshTokenProvider = new MockRefreshTokenStore(UserHash).Object;
            _accounts = new UserAccountsService(new HttpClient(), TestsHelper.GetLocalConfig());
        }

        //[TestMethod]  // only used for manual debugging
        public void ControllerAuthorize1()
        {
            // controller creates state, saves a hash (userHash, state)
            string state = Guid.NewGuid().ToString("N");
     
            // controller encodes userHash and state (this is optional)            
            // controller calls Helper to get Auth URL (userHash, state)
            string url = _accounts.AuthorizeUrl(state, new[]
            {
                "user-modify-playback-state",
                "user-read-playback-state",
                "user-read-currently-playing",

                "user-library-modify",
                "user-library-read",
                "user-top-read",
                "user-read-playback-position",
                "user-read-recently-played",
                "user-follow-read",
                "user-follow-modify"

                //"playlist-read-collaborative",
                //"playlist-modify-public",
                //"playlist-modify-private",
                //"playlist-read-private",
                //"user-read-email",
                //"user-read-private",

            });

            Trace.WriteLine(url);

            // controller redirects to URL
        }

        //[TestMethod]  // only used for manual debugging
        public async Task ControllerAuthorize2()
        {
            // spotify calls back to localhost /authorize/spotify
            const string codeParam = "AQArbSU1WPqZfcCiwsUUihPDqC4fc2_UPA-BZ3vgdMuJ6005YFgQsJjEaUS28g9Cq8ifBo9K-EaV2uL7APHgzrFPNxTO6hkWQFKay9YoFxXyNmhRZx1wQ9u7WsWLXyur-GrYxYLrz6mMfPI0zEy2frLV8a9uoCfZXcZqpVGLseWUYSltraikCX78VlsMyxs0oqp_h8-MdWk0-GjzOdUMKRUs5PAQArpE81GCGhsyyT4J28PUv50dPzGbb5dWnI0oQc2xEpFdRVhMdai0-uWQO9Ej9vUN3wp7YBjC_LlOuYsw8PMAPpEhaAHSvZOjOpRgt7DMdvLOCJr7Fc44mgHe26eeOuqHpVIKNLSYjCOMjpJecblnEbHE5uly4XimJ-ZuYzns5tUs-swmMi2zWl0RADEfqwL9C9K-TzBgMR8umTKcV_rMrGMNbLtGvODUtDKg51Uq8TqrzMp_b84aQDdzbgeXecJez7N1vRGHPLPhtLln6s8&state=000ad7aba70a453490ecb0228bbf39e9";

            // controller calls Accounts Service to get access and refresh tokens
            // account service updates store
            var token = await _accounts.RequestAccessRefreshToken(codeParam);
            Trace.WriteLine(token.AccessToken);
        }
    }
}
