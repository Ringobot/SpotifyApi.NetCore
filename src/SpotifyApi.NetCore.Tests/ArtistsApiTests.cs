using System.Diagnostics;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpotifyApi.NetCore.Http;
using SpotifyApi.NetCore;
using System.Net.Http;
using System.Collections.Specialized;
using SpotifyApi.NetCore.Cache;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.IO;

namespace SpotifyApi.NetCore.Tests
{
    [TestClass]
    public class ArtistsApiTests
    {
        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetArtist_ArtistsId_CorrectArtistName()
        {
            // arrange
            const string artistId = "1tpXaFf2F55E7kVJON4j4G";

            var http = new HttpClient();
            var store = new Mock<ITokenStore<BearerAccessRefreshToken>>().Object;
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig(), store, null);

            var api = new ArtistsApi(http, accounts);

            // act
            dynamic response = await api.GetArtist(artistId);

            // assert
            Assert.AreEqual("Black Rebel Motorcycle Club", response.name.ToString());
        }
    }
}