using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using Moq;
using System.Net.Http;

namespace SpotifyApi.NetCore.Tests
{
    [TestClass]
    public class SpotifyWebApiTests
    {
        [TestMethod]
        public async Task GetAccessToken_ParamNullFieldNotNull_ReturnsField()
        {
            // arrange
            const string accessToken = "abc123";
            var mockSpotifyWebApi = new Mock<SpotifyWebApi>(new Mock<HttpClient>().Object, accessToken)
                { CallBase = true };

            // act
            string token = await mockSpotifyWebApi.Object.GetAccessToken(null);

            // assert
            Assert.AreEqual(accessToken, token);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task GetAccessToken_ParamNullFieldNullTokenProviderNullAccountsServiceNull_ThrowsInvalidOperationException()
        {
            // arrange
            const string accessToken = null;

            var mockSpotifyWebApi = new Mock<SpotifyWebApi>(new Mock<HttpClient>().Object)
            { CallBase = true };

            // act
            string token = await mockSpotifyWebApi.Object.GetAccessToken(accessToken);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task GetAccessToken_ParamNullTokenProviderGetAccessTokenReturnsNull_ThrowsInvalidOperationException()
        {
            // arrange
            const string accessToken = null;

            var mockTokenProvider = new Mock<IAccessTokenProvider>();

            var mockSpotifyWebApi = new Mock<SpotifyWebApi>(new Mock<HttpClient>().Object, mockTokenProvider.Object)
            { CallBase = true };

            // act
            string token = await mockSpotifyWebApi.Object.GetAccessToken(accessToken);
        }

        [TestMethod]
        public async Task GetAccessToken_ParamNullTokenProviderGetAccessTokenReturnsToken_ReturnsTokenProviderToken()
        {
            // arrange
            const string accessToken = null;
            const string tokenProviderToken = "abc123";

            var mockTokenProvider = new Mock<IAccessTokenProvider>();
            mockTokenProvider.Setup(p => p.GetAccessToken()).ReturnsAsync(tokenProviderToken);

            var mockSpotifyWebApi = new Mock<SpotifyWebApi>(new Mock<HttpClient>().Object, mockTokenProvider.Object)
            { CallBase = true };

            // act
            string token = await mockSpotifyWebApi.Object.GetAccessToken(accessToken);

            // assert
            Assert.AreEqual(tokenProviderToken, token);
        }

        [TestMethod]
        public async Task GetAccessToken_ParamNullTokenProviderNullAccountsServiceGetAppAccessTokenReturnsToken_ReturnsAccountsServiceToken()
        {
            // arrange
            const string accessToken = null;
            const string accountsServiceToken = "ghi789";
            var bearerAccessToken = new BearerAccessToken { AccessToken = accountsServiceToken };

            var mockAccountsService = new Mock<IAccountsService>();
            mockAccountsService.Setup(s => s.GetAppAccessToken()).ReturnsAsync(bearerAccessToken);

            var mockSpotifyWebApi = new Mock<SpotifyWebApi>(new Mock<HttpClient>().Object, mockAccountsService.Object)
            { CallBase = true };

            // act
            string token = await mockSpotifyWebApi.Object.GetAccessToken(accessToken);

            // assert
            Assert.AreEqual(accountsServiceToken, token);
        }

    }
}
