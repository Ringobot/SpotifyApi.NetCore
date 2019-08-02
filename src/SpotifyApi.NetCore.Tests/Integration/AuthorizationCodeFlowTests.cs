using System;
using System.Collections.Concurrent;
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
        IRefreshTokenProvider _refreshTokenProvider;

        const string UserHash = "E11AC28538A7C0A827A726DD9B30B710FC1FCAFFFE2E86FCA853AB90E7C710D2";

        [TestInitialize]
        public void Initialize()
        {
            _refreshTokenProvider = new MockRefreshTokenStore(UserHash).Object;
            _accounts = new UserAccountsService(new HttpClient(), TestsHelper.GetLocalConfig(), _refreshTokenProvider);
        }

        //[TestMethod]  // only used for manual debugging
        public void ControllerAuthorize1()
        {
            // controller creates state, saves a hash (userHash, state)
            string state = Guid.NewGuid().ToString("N");
     
            // controller encodes userHash and state (this is optional)            
            // controller calls Helper to get Auth URL (userHash, state)
            string url = _accounts.AuthorizeUrl(state, null);

            // controller redirects to URL
        }

        //[TestMethod]  // only used for manual debugging
        public async Task ControllerAuthorize2()
        {
            // spotify calls back to localhost /authorize/spotify
            const string codeParam = "AQAiwTkH_Awh4L4LH7sb9B5sK2OpfhxSoRlpjIUgciObsb3qip6OeLVSYOXmbbidVHXPZyWJOMYnUDOdKG2iWJqK9xkrZ-MSW0WF32jw40IZ9JgPF74ZIzPa0Og5eB1cKL80pJq9jVXjOi3aPDe-JNz0q9a3M_5pioD6ErRyZW-9mm-mf1uS_GeRHTIxgmdZo5Aio5tSMoZrf-_ajg";

            // decodes state, gets hash, checks state
            //Assert.AreEqual(_userHashstate.UserHash, decoded.userHash);
            //Assert.AreEqual(_userHashstate.State, decoded.state);

            // controller calls Accounts Service to get access and refresh tokens
            // account service updates store
            await _accounts.RequestAccessRefreshToken(UserHash, codeParam);
        }
    }
}
