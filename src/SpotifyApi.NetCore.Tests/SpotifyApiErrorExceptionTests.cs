using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

            var j = JsonConvert.DeserializeObject(content) as JObject;
            var t = j["error"].Type;
            Debug.WriteLine($"error type = {t}");
            Debug.WriteLine($"error.message = {j["error"].Value<string>("message")}");
            Debug.WriteLine($"error.foo = {j["error"].Value<string>("foo")}");

            // act
            var error = await SpotifyApiErrorException.ReadErrorResponse(response);

            // assert
            Assert.IsNotNull(error);
        }

        [TestMethod]
        public async Task ReadErrorResponse_AlternateErrorFormat_ReturnsNotNull()
        {
            // arrange
            const string content = "{\"error\":\"invalid_grant\",\"error_description\":\"Invalid authorization code\"}";

            var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(content, Encoding.Unicode,"application/json")
            };

            var j = JsonConvert.DeserializeObject(content) as JObject;
            var t = j["error"].Type;            
            Debug.WriteLine($"error type = {t}");
            Debug.WriteLine($"error_description = {j["error_description"].Value<string>()}");

            if (j.ContainsKey("foo"))
            {
                var t2 = j["foo"].Type;            
                Debug.WriteLine($"foo type = {t2}");
            }

            // act
            var error = await SpotifyApiErrorException.ReadErrorResponse(response);

            // assert
            Assert.IsNotNull(error);
        }

        // 
    }
}
