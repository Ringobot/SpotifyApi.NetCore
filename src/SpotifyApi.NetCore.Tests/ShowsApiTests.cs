using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpotifyApi.NetCore.Authorization;
using SpotifyApi.NetCore.Models;

namespace SpotifyApi.NetCore.Tests
{
    [TestClass]
    public class ShowsApiTests
    {
        ShowsApi api;
        string bearerAccessToken;

        [TestInitialize]
        public void Initialize()
        {
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            bearerAccessToken = testConfig.GetValue(typeof(string),
                "SpotifyUserBearerAccessToken").ToString();
            var accounts = new AccountsService(http, testConfig);
            api = new ShowsApi(http, accounts);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetShow_ShowId_IsNotNull()
        {
            // assert
            Assert.IsNotNull(await api.GetShow("38bS44xjbVVZ3No3ByF1dJ",
                accessToken: bearerAccessToken));
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetSeveralShows_ShowIds_IsNotNull()
        {
            // assert
            Assert.IsNotNull(await api.GetSeveralShows(new string[] { "5CfCWKI5pZ28U0uOzXkDHe" },
                accessToken: bearerAccessToken));
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetShowEpisodes_ShowId_IsNotNull()
        {
            // assert
            Assert.IsNotNull(await api.GetShowEpisodes("38bS44xjbVVZ3No3ByF1dJ",
                accessToken: bearerAccessToken));
        }
    }
}
