using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using SpotifyApi.NetCore.Tests.Mocks;
using SpotifyApi.NetCore.Tests.Http;

namespace SpotifyApi.NetCore.Tests
{
    [TestClass]
    public class BrowseApiTests
    {
        [TestMethod]
        public async Task GetRecommendations_2SeedArtists_UrlContainsArtists()
        {
            // arrange
            string[] artists = new[] { "abc123", "def456" };

            var http = new MockHttpClient();
            var accounts = new MockAccountsService().Object;

            var api = new Mock<BrowseApi>(http.HttpClient, accounts) { CallBase = true };
            api.Setup(a => a.Get<RecommendationsResult>(It.IsAny<string>())).ReturnsAsync(new RecommendationsResult());

            // act
            await api.Object.GetRecommendations(artists, null, null);

            // assert
            api.Verify(a => a.Get<RecommendationsResult>("https://api.spotify.com/v1/recommendations?seed_artists=abc123,def456&"));
        }

        [TestMethod]
        public async Task GetRecommendations_2SeedGenres_UrlContainsGenres()
        {
            // arrange
            string[] genres = new[] { "genreabc123", "genredef456" };

            var http = new MockHttpClient();
            var accounts = new MockAccountsService().Object;

            var api = new Mock<BrowseApi>(http.HttpClient, accounts) { CallBase = true };
            api.Setup(a => a.Get<RecommendationsResult>(It.IsAny<string>())).ReturnsAsync(new RecommendationsResult());

            // act
            await api.Object.GetRecommendations(null, genres, null);

            // assert
            api.Verify(a => a.Get<RecommendationsResult>("https://api.spotify.com/v1/recommendations?seed_genres=genreabc123,genredef456&"));
        }

        [TestMethod]
        public async Task GetRecommendations_2SeedTracks_UrlContainstracks()
        {
            // arrange
            string[] tracks = new[] { "trackabc123", "trackdef456" };

            var http = new MockHttpClient();
            var accounts = new MockAccountsService().Object;
            var api = new Mock<BrowseApi>(http.HttpClient, accounts) { CallBase = true };
            api.Setup(a => a.Get<RecommendationsResult>(It.IsAny<string>())).ReturnsAsync(new RecommendationsResult());

            // act
            await api.Object.GetRecommendations(null, null, tracks);

            // assert
            api.Verify(a => a.Get<RecommendationsResult>("https://api.spotify.com/v1/recommendations?seed_tracks=trackabc123,trackdef456&"));
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetRecommendations_SeedArtists_NoError()
        {
            // arrange
            string[] seedArtists = new[] { "1tpXaFf2F55E7kVJON4j4G", "4Z8W4fKeB5YxbusRsdQVPb" };

            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\.."))
                .AddJsonFile("appsettings.local.json", false)
                .Build();

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new BrowseApi(http, accounts);

            // act
            var response = await api.GetRecommendations(seedArtists, null, null);
            string name = response.Tracks[0].Name;
            Trace.WriteLine(name);
        }
    }
}