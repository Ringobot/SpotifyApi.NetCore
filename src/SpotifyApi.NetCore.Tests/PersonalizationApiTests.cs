using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpotifyApi.NetCore.Authorization;
using SpotifyApi.NetCore.Models;
using SpotifyApi.NetCore.Helpers;

namespace SpotifyApi.NetCore.Tests
{
    [TestClass]
    public class PersonalizationApiTests
    {
        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetUserTopArtists_TimeRange_LongTerm_IsNotNull()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            var api = new PersonalizationApi(http, accounts);

            // act
            PagedArtists response = await api.GetUsersTopArtists(timeRange: TimeRange.LongTerm,
                accessToken: testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsNotNull(value: response);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetUserTopArtists_TimeRange_MediumTerm_IsNotNull()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            var api = new PersonalizationApi(http, accounts);

            // act
            PagedArtists response = await api.GetUsersTopArtists(timeRange: TimeRange.MediumTerm,
                accessToken: testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsNotNull(value: response);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetUserTopArtists_TimeRange_ShortTerm_IsNotNull()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            var api = new PersonalizationApi(http, accounts);

            // act
            PagedArtists response = await api.GetUsersTopArtists(timeRange: TimeRange.ShortTerm,
                accessToken: testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsNotNull(value: response);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetUserTopTracks_TimeRange_LongTerm_IsNotNull()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            var api = new PersonalizationApi(http, accounts);

            // act
            PagedTracks response = await api.GetUsersTopTracks(
                timeRange: TimeRange.LongTerm,
                accessToken: testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsNotNull(value: response);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetUserTopTracks_TimeRange_MediumTerm_IsNotNull()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            var api = new PersonalizationApi(http, accounts);

            // act
            PagedTracks response = await api.GetUsersTopTracks(
                timeRange: TimeRange.MediumTerm,
                accessToken: testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsNotNull(value: response);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetUserTopTracks_TimeRange_ShortTerm_IsNotNull()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            var api = new PersonalizationApi(http, accounts);

            // act
            PagedTracks response = await api.GetUsersTopTracks(
                timeRange: TimeRange.ShortTerm,
                accessToken: testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsNotNull(value: response);
        }
    }
}
