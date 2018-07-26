using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using SpotifyApi.NetCore.Http;
using SpotifyApi.NetCore.Tests.Http;

namespace SpotifyApi.NetCore.Tests
{
    [TestClass]
    public class SpotifyApiErrorExceptionTests
    {
        [TestMethod]
        public async Task ReadErrorResponse_EmptyPlainText_ReturnsNull()
        {
            // arrange
            var response = new HttpResponseMessage(HttpStatusCode.NotFound)
            {
                Content = new StringContent("", Encoding.Unicode,"text/plain")
            };

            // act
            var error = await SpotifyApiErrorException.ReadErrorResponse(response);

            // assert
            Assert.IsNull(error);
        }
        
        [TestMethod]
        public async Task ReadErrorResponse_NoContent_ReturnsNull()
        {
            // arrange
            var response = new HttpResponseMessage(HttpStatusCode.NotFound);

            // act
            var error = await SpotifyApiErrorException.ReadErrorResponse(response);

            // assert
            Assert.IsNull(error);
        }

        [TestMethod]
        public async Task ReadErrorResponse_ValidJson_ReturnsNotNull()
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
            var error = await SpotifyApiErrorException.ReadErrorResponse(response);

            // assert
            Assert.IsNotNull(error);
        }
    }
}
