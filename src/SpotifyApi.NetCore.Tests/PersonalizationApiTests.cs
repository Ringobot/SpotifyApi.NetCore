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
    public class PersonalizationApiTests
    {
        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetUserTopArtists_IsNotNull()
        {
            // arrange
            HttpClient http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            AccountsService accounts = new AccountsService(http, testConfig);

            PersonalizationApi api = new PersonalizationApi(http, accounts);

            // act
            PagedArtists response = await api.GetUserTopArtists(
                accessToken: testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsNotNull(response);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetUserTopTracks_IsNotNull()
        {
            // arrange
            HttpClient http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            AccountsService accounts = new AccountsService(http, testConfig);

            PersonalizationApi api = new PersonalizationApi(http, accounts);

            // act
            PagedTracks response = await api.GetUserTopTracks(
                accessToken: testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsNotNull(response);
        }
    }
}
