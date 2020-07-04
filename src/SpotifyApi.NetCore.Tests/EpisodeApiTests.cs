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
        EpisodesApi api;
        string bearerAccessToken;

        public EpisodeApiTests()
        {
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            bearerAccessToken = testConfig.GetValue(typeof(string),
                "SpotifyUserBearerAccessToken").ToString();
            var accounts = new AccountsService(http, testConfig);
            api = new EpisodesApi(http, accounts);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetEpisode_EpisodeId_IsNotNull()
        {
            // assert
            Assert.IsNotNull(value: await api.GetEpisode("512ojhOuo1ktJprKbVcKyQ", "ES",
                bearerAccessToken));
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetSeveralEpisodes_EpisodeId_IsNotNull()
        {
            // assert
            Assert.IsNotNull(value: await api.GetSeveralEpisodes(
                new string[] { "77o6BIVlYM3msb4MMIL1jH" },
                "ES",
                bearerAccessToken));
        }
    }
}
