using System;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SpotifyApi.NetCore.Tests.Authorization
{
    [TestClass]
    public class AuthorizationCodeServiceTests
    {
        const string UserHash = "E11AC28538A7C0A827A726DD9B30B710FC1FCAFFFE2E86FCA853AB90E7C710D2";

        [TestMethod]
        public async Task RequestAuthorizationUrl_UserHash_UrlContainsUserHash()
        {
            // arrange
            var http = new HttpClient();
            var service = new AuthorizationCodeService(http, TestsHelper.GetLocalConfig(), MockData().Object, null);

            // act
            string url = await service.RequestAuthorizationUrl(UserHash);
            
            // assert
            Assert.IsTrue(url.Contains(UserHash), "url should contain userHash");
        }

        [TestMethod]
        public async Task RequestAuthorizationUrl_Scopes_UrlContainsSpaceDelimitedScopes()
        {
            // arrange
            string[] scopes = new []
            { 
                "user-modify-playback-state", 
                "user-read-playback-state", 
                "playlist-read-collaborative", 
                "playlist-modify-public",
                "playlist-modify-private",
                "playlist-read-private"
            };

            var http = new HttpClient();
            var service = new AuthorizationCodeService(http, TestsHelper.GetLocalConfig(), MockData().Object, scopes);

            // act
            string url = await service.RequestAuthorizationUrl(UserHash);
            
            // assert
            Assert.IsTrue(url.Contains(string.Join(" ", scopes)), "url should contain space delimited user scopes");
            Trace.WriteLine("RequestAuthorizationUrl_Scopes_UrlContainsSpaceDelimitedScopes url =");
            Trace.WriteLine(url);
        }

        private Mock<IUserAuthData> MockData() {
            var mockAuthUser = new Mock<IUserAuthEntity>();
            mockAuthUser.SetupAllProperties();
            mockAuthUser.SetupGet(u=>u.UserHash).Returns(UserHash);

            var setState = new Func<string, string, IUserAuthEntity>( (h, s) => 
            {
                mockAuthUser.Object.State = s; 
                return mockAuthUser.Object; 
            });

            var mockData = new Mock<IUserAuthData>();
            mockData.Setup(d=>d.Create(It.IsAny<string>(), It.IsAny<string>())).Returns(setState);
            mockData.Setup(d=>d.Get(It.IsAny<string>())).ReturnsAsync(mockAuthUser.Object);
            return mockData;
        }
    }
}