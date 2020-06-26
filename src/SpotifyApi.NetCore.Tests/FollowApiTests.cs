using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpotifyApi.NetCore.Authorization;

namespace SpotifyApi.NetCore.Tests
{
    [TestClass]
    public class FollowApiTests
    {
        [TestCategory("Integration")]
        [TestMethod]
        public async Task CheckCurrentUserFollowsArtists_ArtistId_AnyItems()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            var api = new FollowApi(http, accounts);

            // act
            var response = await api.CheckCurrentUserFollowsArtists(new string[] { "74ASZWbe4lXaubB36ztrGX" }, 
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsTrue(response.Any<bool>());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task CheckCurrentUserFollowsUsers_UserId_AnyItems()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            var api = new FollowApi(http, accounts);

            // act
            var response = await api.CheckCurrentUserFollowsUsers(
                new string[] { "exampleuser01" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsTrue(response.Any<bool>());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task CheckUsersFollowPlaylist_PlaylistId_UserIds_AnyItems()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            var api = new FollowApi(http, accounts);

            // act
            var response = await api.CheckUsersFollowPlaylist("2v3iNvBX8Ay1Gt2uXtUKUT", 
                new string[] { "possan", "elogain" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsTrue(response.Any<bool>());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task FollowArtists_ArtistIds_IsTrue()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            var api = new FollowApi(http, accounts);

            // act
            await api.FollowArtists(
                new string[] { "74ASZWbe4lXaubB36ztrGX" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // checking if artists were followed successfully
            var response = await api.CheckCurrentUserFollowsArtists(new string[] { "74ASZWbe4lXaubB36ztrGX" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsTrue(response.FirstOrDefault<bool>());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task FollowUsers_UserIds_IsTrue()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            var api = new FollowApi(http, accounts);

            // act
            await api.FollowUsers(
                new string[] { "exampleuser01" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // checking if users were followed successfully
            var response = await api.CheckCurrentUserFollowsUsers(
                new string[] { "exampleuser01" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsTrue(response.FirstOrDefault<bool>());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task FollowPlaylist_PlaylistId_IsPublic_IsTrue()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            var api = new FollowApi(http, accounts);

            // act
            await api.FollowPlaylist(
                "2v3iNvBX8Ay1Gt2uXtUKUT",
                true,
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // getting usersApi to get user.Id to avoid hardcoding
            var usersApi = new UsersProfileApi(http, accounts);

            // getting current users profile for user.Id
            var user = await usersApi.GetCurrentUsersProfile(
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // checking to see if user is following the playlist
            var response = await api.CheckUsersFollowPlaylist("2v3iNvBX8Ay1Gt2uXtUKUT",
                new string[] { user.Id },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            Assert.IsTrue(response.FirstOrDefault<bool>());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetUsersFollowedArtists_AnyItems()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            var api = new FollowApi(http, accounts);

            // act
            var response = await api.GetUsersFollowedArtists(
                accessToken: testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsNotNull(response);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task UnfollowArtists_ArtistIds_IsFalse()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            var api = new FollowApi(http, accounts);

            // act
            await api.UnfollowArtists(new string[] { "74ASZWbe4lXaubB36ztrGX" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // checking if user unfollowed artist id
            var response = await api.CheckCurrentUserFollowsArtists(new string[] { "74ASZWbe4lXaubB36ztrGX" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsFalse(response.FirstOrDefault<bool>());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task UnfollowUsers_UserIds_IsFalse()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            var api = new FollowApi(http, accounts);

            // act
            await api.UnfollowUsers(new string[] { "exampleuser01" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // checking if user unfollowed artist id
            var response = await api.CheckCurrentUserFollowsUsers(new string[] { "exampleuser01" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsFalse(response.FirstOrDefault<bool>());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task UnfollowPlaylist_PlaylistId_IsFalse()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            var api = new FollowApi(http, accounts);

            // act
            await api.UnfollowPlaylist("2v3iNvBX8Ay1Gt2uXtUKUT",
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // getting usersApi to get user.Id to avoid hardcoding
            var usersApi = new UsersProfileApi(http, accounts);

            // getting current users profile for user.Id
            var user = await usersApi.GetCurrentUsersProfile(
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // checking if user unfollowed artist id
            var response = await api.CheckUsersFollowPlaylist("2v3iNvBX8Ay1Gt2uXtUKUT",
                new string[] { user.Id },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsFalse(response.FirstOrDefault<bool>());
        }
    }
}
