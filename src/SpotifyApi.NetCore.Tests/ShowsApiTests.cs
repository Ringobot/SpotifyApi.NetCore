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
        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetShow_ShowId_IsNotNull()
        {
            // arrange
            HttpClient http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            AccountsService accounts = new AccountsService(http, testConfig);

            ShowsApi api = new ShowsApi(http, accounts);

            // act
            Show response = await api.GetShow("38bS44xjbVVZ3No3ByF1dJ",
                accessToken: testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsNotNull(response);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetSeveralShows_ShowIds_IsNotNull()
        {
            // arrange
            HttpClient http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            AccountsService accounts = new AccountsService(http, testConfig);

            ShowsApi api = new ShowsApi(http, accounts);

            // act
            PagedShows response = await api.GetSeveralShows(new string[] { "5CfCWKI5pZ28U0uOzXkDHe" },
                accessToken: testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsNotNull(response);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetShowEpisodes_ShowId_IsNotNull()
        {
            // arrange
            HttpClient http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            AccountsService accounts = new AccountsService(http, testConfig);

            ShowsApi api = new ShowsApi(http, accounts);

            // act
            PagedEpisodes response = await api.GetShowEpisodes("38bS44xjbVVZ3No3ByF1dJ",
                accessToken: testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsNotNull(response);
        }
    }
}
