using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpotifyApi.NetCore.Tests.Mocks;

namespace SpotifyApi.NetCore.Tests
{
    [TestClass]
    public class AccountsServiceTests
    {
        const string UserHash = "E11AC28538A7C0A827A726DD9B30B710FC1FCAFFFE2E86FCA853AB90E7C710D2";

        [TestMethod]
        public void AuthorizeUrl_StateParam_UrlContainsState()
        {
            // arrange
            const string state = "abc123";
            var http = new MockHttpClient().HttpClient;
            var config = new MockConfiguration().Object;
            var tokenStore = new MockRefreshTokenStore(UserHash, config).Object;
            var service = new UserAccountsService(http, config, tokenStore);

            // act
            string url = service.AuthorizeUrl(state, null);

            // assert
            Assert.IsTrue(url.Contains(state), "url result should contain state param");
        }

        [TestMethod]
        public void RequestAuthorizationUrl_Scopes_UrlContainsSpaceDelimitedScopes()
        {
            // arrange
            const string state = "abc123";

            string[] scopes = new[]
            {
                "user-modify-playback-state",
                "user-read-playback-state",
                "playlist-read-collaborative",
                "playlist-modify-public",
                "playlist-modify-private",
                "playlist-read-private"
            };

            var config = new MockConfiguration().Object;
            var http = new HttpClient();
            var tokenStore = new MockRefreshTokenStore(UserHash, config).Object;
            var service = new UserAccountsService(http, config, tokenStore);

            // act
            string url = service.AuthorizeUrl(state, scopes);

            // assert
            Assert.IsTrue(url.Contains(string.Join("%20", scopes)), "url should contain %20 (space) delimited user scopes");
            Trace.WriteLine("RequestAuthorizationUrl_Scopes_UrlContainsSpaceDelimitedScopes url =");
            Trace.WriteLine(url);
        }
    }
}