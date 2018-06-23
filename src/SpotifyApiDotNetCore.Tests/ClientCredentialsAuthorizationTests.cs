using System;
using System.Collections.Specialized;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpotifyApiDotNetCore.Cache;
using SpotifyApiDotNetCore.Http;

namespace SpotifyApiDotNetCore.Tests
{
    [TestClass]
    public class ClientCredentialsAuthorizationTests
    {
        // ReSharper disable InconsistentNaming

        [TestMethod]
        public async Task GetAccessToken_CacheNotNullAndItemDoesNotExist_CacheAddCalled()
        {
            // Arrange
            var mockCache = new Mock<ICache>();
            
            var mockHttp = new Mock<IHttpClient>();
            mockHttp.Setup(h => h.Post(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<AuthenticationHeaderValue>()))
                .ReturnsAsync("{\"access_token\":\"ghi678\", \"expires_in\":3600}");
            
            var settings = new NameValueCollection
            {
                {"SpotifyApiClientId", "abc123"},
                {"SpotifyApiClientSecret", "def345"}
            };

            var auth = new ClientCredentialsAuthorizationApi(mockHttp.Object, settings, mockCache.Object);

            // Act
            await auth.GetAccessToken();

            // Assert
            mockCache.Verify(m => m.Add("Radiostr.SpotifyWebApi.ClientCredentialsAuthorizationApi.BearerToken", "ghi678", It.IsAny<DateTime>()));
        }

        [TestMethod]
        public async Task GetAccessToken_CacheNotNullAndItemDoesExist_CacheAddNotCalled()
        {
            // Arrange
            var mockCache = new Mock<ICache>();
            mockCache.Setup(c => c.Get("Radiostr.SpotifyWebApi.ClientCredentialsAuthorizationApi.BearerToken"))
                .Returns("jkl901");

            var mockHttp = new Mock<IHttpClient>();
            mockHttp.Setup(h => h.Post(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<AuthenticationHeaderValue>()))
                .ReturnsAsync("{\"access_token\":\"ghi678\", \"expires_in\":3600}");
            
            var settings = new NameValueCollection
            {
                {"SpotifyApiClientId", "abc123"},
                {"SpotifyApiClientSecret", "def345"}
            };

            var auth = new ClientCredentialsAuthorizationApi(mockHttp.Object, settings, mockCache.Object);

            // Act
            await auth.GetAccessToken();

            // Assert
            mockCache.Verify(m => m.Add(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<DateTime>()), Times.Never());
        }

        [TestMethod]
        public async Task GetAccessToken_CacheItemDoesNotExist_PostResponseReturned()
        {
            // Arrange
            var mockCache = new Mock<ICache>();

            var mockHttp = new Mock<IHttpClient>();
            mockHttp.Setup(h => h.Post(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<AuthenticationHeaderValue>()))
                .ReturnsAsync("{\"access_token\":\"ghi678\", \"expires_in\":3600}");
            
            var settings = new NameValueCollection
            {
                {"SpotifyApiClientId", "abc123"},
                {"SpotifyApiClientSecret", "def345"}
            };

            var auth = new ClientCredentialsAuthorizationApi(mockHttp.Object, settings, mockCache.Object);

            // Act
            string token = await auth.GetAccessToken();

            // Assert
            Assert.AreEqual("ghi678", token);
        }

        [TestMethod]
        public async Task GetAccessToken_CacheItemDoesNotExist_PostResponseAdded()
        {
            // Arrange
            var mockCache = new Mock<ICache>();

            var mockHttp = new Mock<IHttpClient>();
            mockHttp.Setup(h => h.Post(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<AuthenticationHeaderValue>()))
                .ReturnsAsync("{\"access_token\":\"ghi678\", \"expires_in\":3600}");
            
            var settings = new NameValueCollection
            {
                {"SpotifyApiClientId", "abc123"},
                {"SpotifyApiClientSecret", "def345"}
            };

            var auth = new ClientCredentialsAuthorizationApi(mockHttp.Object, settings, mockCache.Object);

            // Act
            string token = await auth.GetAccessToken();

            // Assert
            Assert.AreEqual("ghi678", token);
        }

        [TestMethod]
        public async Task GetAccessToken_CacheItemDoesNotExist_CacheItemExpiresBeforeOrAtSameTimeAsTokenExpires()
        {
            // Arrange
            var tokenExpires = DateTime.Now;
            var cacheExpires = DateTime.Now;
            var mockCache = new Mock<ICache>();
            mockCache.Setup(c => c.Get("Radiostr.SpotifyWebApi.ClientCredentialsAuthorizationApi.BearerToken"))
                .Returns("jkl901");
            mockCache.Setup(c => c.Add(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<DateTime>()))
                .Callback((string k, object v, DateTime e) => cacheExpires = e);

            var mockHttp = new Mock<IHttpClient>();
            mockHttp.Setup(h => h.Post(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<AuthenticationHeaderValue>()))
                .ReturnsAsync("{\"access_token\":\"ghi678\", \"expires_in\":3600}")
                .Callback(() => tokenExpires = DateTime.Now.AddSeconds(3600));
            
            var settings = new NameValueCollection
            {
                {"SpotifyApiClientId", "abc123"},
                {"SpotifyApiClientSecret", "def345"}
            };

            var auth = new ClientCredentialsAuthorizationApi(mockHttp.Object, settings, mockCache.Object);

            // Act
            await auth.GetAccessToken();

            // Assert
            Assert.IsTrue(cacheExpires <= tokenExpires);
        }

        // ReSharper restore InconsistentNaming

	
    }
}
