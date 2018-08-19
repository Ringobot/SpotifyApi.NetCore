using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpotifyApi.NetCore.Tests.Mocks;

namespace SpotifyApi.NetCore.Tests
{
    [TestClass]
    public class UserAccountsServiceTests
    {
        const string UserHash = "E11AC28538A7C0A827A726DD9B30B710FC1FCAFFFE2E86FCA853AB90E7C710D2";

        [TestMethod]
        public async Task GetUserAccessToken_TokenExpired_ReturnsNewToken()
        {
            // arrange
            var expiredToken = new BearerAccessToken
            {
                AccessToken = "abcd1234",
                ExpiresIn = 3600,
                Expires = new DateTime(2018, 7, 28, 9, 18, 0, DateTimeKind.Utc)
            };

            const string json = @"{
""access_token"": ""NgCXRKc...MzYjw"",
""token_type"": ""bearer"",
""expires_in"": 3600,
}";

            var mockHttp = new MockHttpClient();
            mockHttp.SetupSendAsync(json);
            var http = mockHttp.HttpClient;
            
            var bearerTokenStore = new Mock<IBearerTokenStore>();
            bearerTokenStore.Setup(s=>s.Get(It.IsAny<string>())).ReturnsAsync(expiredToken);
            var config = new MockConfiguration().Object;
            var refreshTokenStore = new MockRefreshTokenStore(UserHash, config).Object;
            var service = new UserAccountsService(http, config, refreshTokenStore, bearerTokenStore.Object);

            // act
            var token = await service.GetUserAccessToken(UserHash);

            // assert
            Assert.AreNotEqual(expiredToken, token);
        }

        [TestMethod]
        public async Task GetUserAccessToken_TokenNotExpired_ReturnsCurrentToken()
        {
            // arrange
            var currentToken = new BearerAccessToken
            {
                AccessToken = "abcd1234",
                ExpiresIn = 3600,
                Expires = DateTime.UtcNow.AddSeconds(3600)
            };

            var http = new MockHttpClient().HttpClient;
            var bearerTokenStore = new Mock<IBearerTokenStore>();
            bearerTokenStore.Setup(s=>s.Get(It.IsAny<string>())).ReturnsAsync(currentToken);

            var config = new MockConfiguration().Object;

            var refreshTokenStore = new MockRefreshTokenStore(UserHash, config).Object;
            var service = new UserAccountsService(http, config, refreshTokenStore, bearerTokenStore.Object);

            // act
            var token = await service.GetUserAccessToken(UserHash);

            // assert
            Assert.AreEqual(currentToken, token);
        }
    }
}