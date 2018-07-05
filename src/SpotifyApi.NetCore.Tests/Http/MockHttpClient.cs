using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;

namespace SpotifyApi.NetCore.Tests.Http
{
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
    }
}