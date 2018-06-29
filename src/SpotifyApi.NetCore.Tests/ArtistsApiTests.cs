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

namespace SpotifyApi.NetCore.Tests
{
    [TestClass]
    public class ArtistsApiTests
    {
        [TestMethod]
        public async Task GetArtist_ArtistsId_CorrectArtistName()
        {
            // arrange
            const string artistId = "XXX";

            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            var http = new HttpClient();
            //TODO: Add spotify env vars 
            var cache = new RuntimeMemoryCache(new MemoryCache(new MemoryCacheOptions()));
            var auth = new ClientCredentialsAuthorizationApi(http, config);
            var api = new ArtistsApi(http, auth);

            // act
            var response = await api.GetArtist(artistId);
            
            // assert
            Assert.AreEqual(response.artist.name, "Radiohead");
        }
    }
}