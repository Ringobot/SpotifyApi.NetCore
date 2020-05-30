using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpotifyApi.NetCore.Authorization;
using SpotifyApi.NetCore.Tests.Mocks;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore.Tests
{
    [TestClass]
    public class BrowseApiTests
    {
        [TestMethod]
        [TestCategory("Integration")]
        public async Task GetCategories_Limit2_ItemsLength2()
        {
            // arrange
            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new BrowseApi(http, accounts);

            // act
            var response = await api.GetCategories(limit:2);
            Assert.AreEqual(2, response.Items.Length);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task GetCategory_FromNZCategories_SameCategoryHref()
        {
            // arrange
            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new BrowseApi(http, accounts);
            var category = (await api.GetCategories(limit: 1, country:SpotifyCountryCodes.New_Zealand)).Items[0];

            // act
            var response = await api.GetCategory(category.Id, country:SpotifyCountryCodes.New_Zealand);

            Assert.AreEqual(category.Href, response.Href);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task GetCategoryPlaylists_FromFirstNZCategoryLimit2_ItemsLength2()
        {
            // arrange
            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new BrowseApi(http, accounts);
            var category = (await api.GetCategories(limit: 1, country: SpotifyCountryCodes.New_Zealand)).Items[0];

            // act
            var response = await api.GetCategoryPlaylists(category.Id, country: SpotifyCountryCodes.New_Zealand, limit: 2);

            Assert.AreEqual(2, response.Items.Length);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task GetFeaturedPlaylists_Limit2_ItemsLength2()
        {
            // arrange
            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new BrowseApi(http, accounts);

            // act
            var response = await api.GetFeaturedPlaylists(country: SpotifyCountryCodes.New_Zealand, limit: 2);

            Assert.AreEqual(2, response.Items.Length);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task GetNewReleases_NoParams_NoError()
        {
            // arrange
            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new BrowseApi(http, accounts);

            // act
            var response = await api.GetNewReleases();
            string name = response.Items[0].Name;
            Trace.WriteLine(name);
        }

        [TestMethod]
        public async Task GetNewReleases_Country_UrlContainsCountry()
        {
            // arrange
            const string country = SpotifyCountryCodes.New_Zealand;

            var http = new MockHttpClient();
            var accounts = new MockAccountsService().Object;

            var api = new Mock<BrowseApi>(http.HttpClient, accounts) { CallBase = true };
            api.Setup(a => a.GetModelFromProperty<PagedAlbums>(
                It.IsAny<Uri>(),
                It.IsAny<string>(),
                It.IsAny<string>())).ReturnsAsync(new PagedAlbums());

            // act
            await api.Object.GetNewReleases(country: country);

            // assert
            api.Verify(a => a.GetModelFromProperty<PagedAlbums>(
                new Uri($"https://api.spotify.com/v1/browse/new-releases?country={SpotifyCountryCodes.New_Zealand}"),
                It.IsAny<string>(),
                It.IsAny<string>()));
        }

        [TestMethod]
        public async Task GetRecommendations_2SeedArtists_UrlContainsArtists()
        {
            // arrange
            string[] artists = new[] { "abc123", "def456" };

            var http = new MockHttpClient();
            var accounts = new MockAccountsService().Object;

            var api = new Mock<BrowseApi>(http.HttpClient, accounts) { CallBase = true };
            api.Setup(a => a.GetModel<RecommendationsResult>(It.IsAny<Uri>(), It.IsAny<string>()))
                .ReturnsAsync(new RecommendationsResult());

            // act
            await api.Object.GetRecommendations(seedArtists: artists);

            // assert
            api.Verify(a => a.GetModel<RecommendationsResult>(
                new Uri("https://api.spotify.com/v1/recommendations?seed_artists=abc123,def456"),
                null));
        }

        [TestMethod]
        public async Task GetRecommendations_2SeedGenres_UrlContainsGenres()
        {
            // arrange
            string[] genres = new[] { "genreabc123", "genredef456" };

            var http = new MockHttpClient();
            var accounts = new MockAccountsService().Object;

            var api = new Mock<BrowseApi>(http.HttpClient, accounts) { CallBase = true };
            api.Setup(a => a.GetModel<RecommendationsResult>(It.IsAny<Uri>(), null))
                .ReturnsAsync(new RecommendationsResult());

            // act
            await api.Object.GetRecommendations(null, genres, null);

            // assert
            api.Verify(a => a.GetModel<RecommendationsResult>(
                new Uri("https://api.spotify.com/v1/recommendations?seed_genres=genreabc123,genredef456"),
                null));
        }

        [TestMethod]
        public async Task GetRecommendations_2SeedTracks_UrlContainstracks()
        {
            // arrange
            string[] tracks = new[] { "trackabc123", "trackdef456" };

            var http = new MockHttpClient();
            var accounts = new MockAccountsService().Object;
            var api = new Mock<BrowseApi>(http.HttpClient, accounts) { CallBase = true };
            api.Setup(a => a.GetModel<RecommendationsResult>(It.IsAny<Uri>(), null))
                .ReturnsAsync(new RecommendationsResult());

            // act
            await api.Object.GetRecommendations(null, null, tracks);

            // assert
            api.Verify(a => a.GetModel<RecommendationsResult>(
                new Uri("https://api.spotify.com/v1/recommendations?seed_tracks=trackabc123,trackdef456"),
                null));
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetRecommendations_SeedArtists_NoError()
        {
            // arrange
            string[] seedArtists = new[] { "1tpXaFf2F55E7kVJON4j4G", "4Z8W4fKeB5YxbusRsdQVPb" };

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new BrowseApi(http, accounts);

            // act
            var response = await api.GetRecommendations(seedArtists, null, null);
            string name = response.Tracks[0].Name;
            Trace.WriteLine(name);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetAvailableGenreSeeds_NoParams_NoError()
        {
            // arrange
            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new BrowseApi(http, accounts);

            // act
            var response = await api.GetAvailableGenreSeeds();
            string name = response[0];
            Trace.WriteLine(name);
        }
    }
}