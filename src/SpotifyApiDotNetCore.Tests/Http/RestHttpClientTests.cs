using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using SpotifyApiDotNetCore.Http;

namespace SpotifyApiDotNetCore.Tests.Http
{
    [TestClass]
    public class RestHttpClientTests
    {
        // how to mock HttpClient: https://github.com/PeteGoo/UnitTestingHttpClient/blob/master/UnitTestingHttpClient/MyCoolServiceTests.cs

        [TestMethod]
        public async Task Get_RequestUrlAndAuthHeader_RequestMessageUriSet()
        {
            // Arrange
            const string requestUrl = "http://abc123.def/456";
            
            HttpRequestMessage message = null;
            
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Returns(Task<HttpResponseMessage>.Factory.StartNew(() =>
                    new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("{}")
                    }))
                .Callback((HttpRequestMessage m, CancellationToken t) => message = m);

            var http = new RestHttpClient(new HttpClient(mockHttpMessageHandler.Object));

            // Act
            await http.Get(requestUrl);

            // Assert
            Assert.AreEqual(requestUrl, message.RequestUri.ToString());
        }

        [TestMethod]
        public async Task Get_RequestUrlAndAuthHeader_RequestMessageMethodIsGet()
        {
            // Arrange
            const string requestUrl = "http://abc123.def/456";
            
            HttpRequestMessage message = null;
            
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Returns(Task<HttpResponseMessage>.Factory.StartNew(() =>
                    new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("{}")
                    }))
                .Callback((HttpRequestMessage m, CancellationToken t) => message = m);

            var http = new RestHttpClient(new HttpClient(mockHttpMessageHandler.Object));

            // Act
            await http.Get(requestUrl);

            // Assert
            Assert.AreEqual(HttpMethod.Get, message.Method);
        }

        [TestMethod]
        public async Task Post_RequestUrlAndAuthHeader_RequestMessageUriSet()
        {
            // Arrange
            const string requestUrl = "http://abc123.def/456";
            const string formData = "ghi=789";
            
            HttpRequestMessage message = null;
            
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Returns(Task<HttpResponseMessage>.Factory.StartNew(() =>
                    new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("{}")
                    }))
                .Callback((HttpRequestMessage m, CancellationToken t) => message = m);

            var http = new RestHttpClient(new HttpClient(mockHttpMessageHandler.Object));

            // Act
            await http.Post(requestUrl, formData);

            // Assert
            Assert.AreEqual(requestUrl, message.RequestUri.ToString());
        }

        [TestMethod]
        public async Task Post_RequestUrlAndAuthHeader_RequestMessageMethodIsGet()
        {
            // Arrange
            const string requestUrl = "http://abc123.def/456";
            const string formData = "ghi=789";

            HttpRequestMessage message = null;
            
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Returns(Task<HttpResponseMessage>.Factory.StartNew(() =>
                    new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("{}")
                    }))
                .Callback((HttpRequestMessage m, CancellationToken t) => message = m);

            var http = new RestHttpClient(new HttpClient(mockHttpMessageHandler.Object));

            // Act
            await http.Post(requestUrl, formData);

            // Assert
            Assert.AreEqual(HttpMethod.Post, message.Method);
        }
    }
}
