using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpotifyApi.NetCore.Authorization;
using SpotifyApi.NetCore.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore.Tests
{
    [TestClass]
    public class PersonalizationApiTests
    {
        PersonalizationApi api;
        string bearerAccessToken;

        [TestInitialize]
        public void Initialize()
        {
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            bearerAccessToken = testConfig.GetValue(typeof(string),
                "SpotifyUserBearerAccessToken").ToString();
            var accounts = new AccountsService(http, testConfig);
            api = new PersonalizationApi(http, accounts);
        }

        [TestCategory("Integration")]
        [TestCategory("User")]
        [TestMethod]
        public async Task GetUserTopArtists_TimeRange_LongTerm_IsNotNull()
        {
            // assert
            Assert.IsNotNull(value: await api.GetUsersTopArtists(
                timeRange: TimeRange.LongTerm,
                accessToken: bearerAccessToken));
        }

        [TestCategory("Integration")]
        [TestCategory("User")]
        [TestMethod]
        public async Task GetUserTopArtists_TimeRange_MediumTerm_IsNotNull()
        {
            // assert
            Assert.IsNotNull(value: await api.GetUsersTopArtists(
                timeRange: TimeRange.MediumTerm,
                accessToken: bearerAccessToken));
        }

        [TestCategory("Integration")]
        [TestCategory("User")]
        [TestMethod]
        public async Task GetUserTopArtists_TimeRange_ShortTerm_IsNotNull()
        {
            // assert
            Assert.IsNotNull(value: await api.GetUsersTopArtists(
                timeRange: TimeRange.ShortTerm,
                accessToken: bearerAccessToken));
        }

        [TestCategory("Integration")]
        [TestCategory("User")]
        [TestMethod]
        public async Task GetUserTopTracks_TimeRange_LongTerm_IsNotNull()
        {
            // assert
            Assert.IsNotNull(value: await api.GetUsersTopTracks(
                timeRange: TimeRange.LongTerm,
                accessToken: bearerAccessToken));
        }

        [TestCategory("Integration")]
        [TestCategory("User")]
        [TestMethod]
        public async Task GetUserTopTracks_TimeRange_MediumTerm_IsNotNull()
        {
            // assert
            Assert.IsNotNull(value: await api.GetUsersTopTracks(
                timeRange: TimeRange.MediumTerm,
                accessToken: bearerAccessToken));
        }

        [TestCategory("Integration")]
        [TestCategory("User")]
        [TestMethod]
        public async Task GetUserTopTracks_TimeRange_ShortTerm_IsNotNull()
        {
            // assert
            Assert.IsNotNull(value: await api.GetUsersTopTracks(
                timeRange: TimeRange.ShortTerm,
                accessToken: bearerAccessToken));
        }
    }
}
