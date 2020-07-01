using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpotifyApi.NetCore.Authorization;
using SpotifyApi.NetCore.Models;

namespace SpotifyApi.NetCore.Tests
{
    [TestClass]
    public class EpisodeApiTests
    {
        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetEpisode_EpisodeId_IsNotNull()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            EpisodesApi api = new EpisodesApi(http, accounts);

            // act
            Episode response = await api.GetEpisode("512ojhOuo1ktJprKbVcKyQ", market: "ES",
                accessToken: testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsNotNull(value: response);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetSeveralEpisodes_EpisodeId_IsNotNull()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            var api = new EpisodesApi(http, accounts);

            // act
            PagedEpisodes response = await api.GetSeveralEpisodes(
                new string[] { "77o6BIVlYM3msb4MMIL1jH" },
                "ES",
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsNotNull(value: response);
        }
    }
}
