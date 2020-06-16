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
                //"user-modify-playback-state",
                //"user-read-playback-state",
                //"playlist-read-collaborative",
                //"playlist-modify-public",
                //"playlist-modify-private",
                //"playlist-read-private",
                //"user-read-email",
                "user-read-private"
            });

            Trace.WriteLine(url);

            // controller redirects to URL
        }

        //[TestMethod]  // only used for manual debugging
        public async Task ControllerAuthorize2()
        {
            // spotify calls back to localhost /authorize/spotify
            const string codeParam = "AQAV2EQJ1D87ZMpPe7xE5yTIkuzI7FgX5rNMpCKjwJu2wgdPc8tXJh9l7LdKDwvG99S3bxXvE6yde3-oMpBWZWsaD_sN4xHqxCqmsns6A3Kv_Elj_TsdHDIPJjhmmYNN99VFvTXbb83_miw8ZQKPnwAV_R1Rq-eVhr6DXDHwZuMckC719NaC3vX9VVsilcqBfUZx76EJwGINGwUudeBlaaJLn9LJz8aLbIYNbraGu0B2hQNF3IyO9clWIlx1r5ApUyETnLaRdeE6QbjNw4oaPoI98hqkE4zO_LYf1osEQH8F1yupHoQTysTpLl9zu4zTYC2msnQ-lP0-_4IT5ILBuzCSTdqdPoaPinArG9PBGtA21Qnvg14yjK5M6ak_wqxVvtCEbgRedBB6XjjT2eRWonc5Vw9dLFGr38-PajKWzyoMCzzb1BFPYcni";

            // decodes state, gets hash, checks state
            //Assert.AreEqual(_userHashstate.UserHash, decoded.userHash);
            //Assert.AreEqual(_userHashstate.State, decoded.state);

            // controller calls Accounts Service to get access and refresh tokens
            // account service updates store
            var token = await _accounts.RequestAccessRefreshToken(codeParam);
            Trace.WriteLine(token.AccessToken);
        }
    }
}
