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
        public async Task GetUserTopArtists_IsNotNull()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            var api = new PersonalizationApi(http, accounts);

            // act
            PagedArtists response = await api.GetUsersTopArtists(
                accessToken: testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsNotNull(value: response);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetUserTopTracks_IsNotNull()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            var api = new PersonalizationApi(http, accounts);

            // act
            PagedTracks response = await api.GetUsersTopTracks(
                accessToken: testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsNotNull(value: response);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public void TimeRangeString_LongTerm_IsTrue()
        {
            // assert
            Assert.IsTrue(condition: TimeRangeHelper.TimeRangeString(TimeRange.LongTerm) == "long_term");
        }

        [TestCategory("Integration")]
        [TestMethod]
        public void TimeRangeString_MediumTerm_IsTrue()
        {
            // assert
            Assert.IsTrue(condition: TimeRangeHelper.TimeRangeString(TimeRange.MediumTerm) == "medium_term");
        }

        [TestCategory("Integration")]
        [TestMethod]
        public void TimeRangeString_ShortTerm_IsTrue()
        {
            // assert
            Assert.IsTrue(condition: TimeRangeHelper.TimeRangeString(TimeRange.ShortTerm) == "short_term");
        }
    }
}
