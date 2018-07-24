using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpotifyApi.NetCore.Authorization;

namespace SpotifyApi.NetCore.Tests
{
    [TestClass]
    public class AuthorizationCodeFlowTests 
    {
        (string UserHash, string State) _userHashstate;

        UserAccountsService _accounts;
        ITokenStore<BearerAccessRefreshToken> _tokenStore;

        [TestInitialize]
        public void Initialize()
        {
            var mockTokenStore = new Mock<ITokenStore<BearerAccessRefreshToken>>();
            //mockTokenStore.Setup(s=>s.Get(It.IsAny<string>())).Returns()
            _tokenStore = mockTokenStore.Object;
            _accounts = new UserAccountsService(new HttpClient(), TestsHelper.GetLocalConfig(), _tokenStore, null);
        }

        public void ControllerAuthorize1()
        {
            const string userHash = "E11AC28538A7C0A827A726DD9B30B710FC1FCAFFFE2E86FCA853AB90E7C710D2";

            // controller creates state, saves a hash (userHash, state)
            string state = Guid.NewGuid().ToString("N");
            _userHashstate = (userHash, state);

            // controller encodes userHash and state (this is optional)            
            // controller calls Helper to get Auth URL (userHash, state)
            string url = _accounts.AuthorizeUrl(StateHelper.EncodeState(_userHashstate));

            // controller redirects to URL
        }

        public async Task ControllerAuthorize2()
        {
            // spotify calls back to localhost /authorize/spotify
            const string codeParam = "";
            const string stateParam = "";

            // decodes state, gets hash, checks state
            var decoded = StateHelper.DecodeState(stateParam);
            Assert.AreEqual(_userHashstate.UserHash, decoded.userHash);
            Assert.AreEqual(_userHashstate.State, decoded.state);

            // controller calls Accounts Service to get access and refresh tokens
            // account service updates store
            await _accounts.RequestAccessRefreshToken(decoded.userHash, codeParam);
        }

        public async Task ControllerAuthorize3()
        {
            // AuthService provides access token to APIs, refreshes when necessary, updates userAuth record
            var player = new PlayerApi(new HttpClient(), _accounts);
            await player.PlayContext("", "");
        }
    }
}
