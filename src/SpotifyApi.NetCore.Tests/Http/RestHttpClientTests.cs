using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using SpotifyApi.NetCore.Http;

namespace SpotifyApi.NetCore.Tests.Http
{
    [TestClass]
    public class RestHttpClientTests
    {
        // how to mock HttpClient: https://github.com/PeteGoo/UnitTestingHttpClient/blob/master/UnitTestingHttpClient/MyCoolServiceTests.cs

        [TestMethod]
        public async Task Get_RequestUrlAndAuthHeader_RequestMessageUriSet()
        {
            // Arrange
            var requestUrl = new Uri("http://abc123.def/456");
            
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

            var http = new HttpClient(mockHttpMessageHandler.Object);

            // Act
            await http.Get(requestUrl);

            // Assert
            Assert.AreEqual(requestUrl, message.RequestUri.ToString());
        }

        [TestMethod]
        public async Task Get_RequestUrlAndAuthHeader_RequestMessageMethodIsGet()
        {
            // Arrange
            var requestUrl = new Uri("http://abc123.def/456");

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

            var http = new HttpClient(mockHttpMessageHandler.Object);

            // Act
            await http.Get(requestUrl);

            // Assert
            Assert.AreEqual(HttpMethod.Get, message.Method);
        }

        [TestMethod]
        public async Task Post_RequestUrlAndAuthHeader_RequestMessageUriSet()
        {
            // Arrange
            var requestUrl = new Uri("http://abc123.def/456");
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

            var http = new HttpClient(mockHttpMessageHandler.Object);

            // Act
            await http.Post(requestUrl, formData);

            // Assert
            Assert.AreEqual(requestUrl, message.RequestUri.ToString());
        }

        [TestMethod]
        public async Task Post_RequestUrlAndAuthHeader_RequestMessageMethodIsGet()
        {
            // Arrange
            var requestUrl = new Uri("http://abc123.def/456");
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

            var http = new HttpClient(mockHttpMessageHandler.Object);

            // Act
            await http.Post(requestUrl, formData);

            // Assert
            Assert.AreEqual(HttpMethod.Post, message.Method);
        }

        [TestMethod]
        public async Task Get_RequestUrlNullHeader_NoError()
        {
            // Arrange
            var requestUrl = new Uri("http://abc123.def/456");

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

            var http = new HttpClient(mockHttpMessageHandler.Object);

            // Act
            await http.Get(requestUrl);
        }

        [TestMethod]
        [ExpectedException(typeof(SpotifyApiErrorException))]
        public async Task CheckForErrors_SpotifyError_ThrowsSpotifyApiErrorException()
        {
            // arrange
            const string content = @"{
    ""error"": {
        ""status"": 404,
        ""message"": ""No active device found""
    }
}";

            var response = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent(content, Encoding.Unicode,"application/json")
            };

            // act
            await RestHttpClient.CheckForErrors(response);
        }
    }
}
