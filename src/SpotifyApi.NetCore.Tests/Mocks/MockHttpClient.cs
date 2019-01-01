using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;

namespace SpotifyApi.NetCore.Tests.Mocks
{
    /// <summary>
    /// A Mock HttpClient helper
    /// </summary>
    internal class MockHttpClient
    {
        private Mock<HttpMessageHandler> _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        public HttpClient HttpClient { get; private set; }

        public Mock<HttpMessageHandler> MockHttpMessageHandler { get { return _mockHttpMessageHandler; } }

        public MockHttpClient()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            HttpClient = new HttpClient(_mockHttpMessageHandler.Object);
        }

        /// <summary>
        /// Sets up the mock message handler to return the given `responseContent` and status 200 OK.
        /// </summary>
        /// <param name="responseContent">The response as string</param>
        /// <returns>Mock handler for further setup and validation if required</returns>
        internal Moq.Language.Flow.IReturnsResult<HttpMessageHandler> SetupSendAsync(string responseContent)
        {
            return _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Returns(Task<HttpResponseMessage>.Factory.StartNew(() =>
                    new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(responseContent)
                    }));
        }
        
        /// <summary>
        /// Sets up the mock message handler to return status 200 OK with no content.
        /// </summary>
        /// <returns>Mock handler for further setup and validation if required</returns>
        internal Moq.Language.Flow.IReturnsResult<HttpMessageHandler> SetupSendAsync()
        {
            return _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Returns(Task<HttpResponseMessage>.Factory.StartNew(() =>
                    new HttpResponseMessage(HttpStatusCode.OK)));
        }
    }
}