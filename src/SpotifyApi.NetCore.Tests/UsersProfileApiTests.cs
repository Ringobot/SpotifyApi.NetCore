using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpotifyApi.NetCore.Authorization;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore.Tests
{
    [TestClass]
    public class UsersProfileApiTests
    {
        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task GetUsersProfile_NoUserId_DeserializedResponse()
        {
            // arrange
            var config = TestsHelper.GetLocalConfig();
            var accessToken = config["SpotifyUserBearerAccessToken"];
            var http = new HttpClient();
            var accounts = new UserAccountsService(http, config);

            var api = new UsersProfileApi(http, accounts);

            // act
            // must use a User Access Token for this call
            var response = await api.GetCurrentUsersProfile(accessToken: accessToken);

            Assert.IsNotNull(response);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task GetUsersProfile_UserId_IdEqualsUserId()
        {
            // arrange
            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());
            const string userId = "daniellarsennz";

            var api = new UsersProfileApi(http, accounts);

            // act
            var response = await api.GetUsersProfile(userId);

            Assert.AreEqual(userId.ToLower(), response.Id.ToLower());
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task GetUsersProfile_UserId_EmailIsNull()
        {
            // arrange
            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());
            const string userId = "daniellarsennz";

            var api = new UsersProfileApi(http, accounts);

            // act
            var response = await api.GetUsersProfile(userId);

            Assert.IsNull(response.Email);
        }
    }
}
