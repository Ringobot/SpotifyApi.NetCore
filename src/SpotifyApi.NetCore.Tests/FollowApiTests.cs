using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpotifyApi.NetCore.Authorization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using SpotifyApi.NetCore.Models;

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
            HttpClient http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            AccountsService accounts = new AccountsService(http, testConfig);

            FollowApi api = new FollowApi(http, accounts);

            // act
            bool[] response = await api.CheckCurrentUserFollowsArtists(new string[] { "74ASZWbe4lXaubB36ztrGX" }, 
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsTrue(condition: response.Any());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task CheckCurrentUserFollowsUsers_UserId_AnyItems()
        {
            // arrange
            HttpClient http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            AccountsService accounts = new AccountsService(http, testConfig);

            FollowApi api = new FollowApi(http, accounts);

            // act
            bool[] response = await api.CheckCurrentUserFollowsUsers(
                new string[] { "exampleuser01" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsTrue(condition: response.Any());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task CheckUsersFollowPlaylist_PlaylistId_UserIds_AnyItems()
        {
            // arrange
            HttpClient http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            AccountsService accounts = new AccountsService(http, testConfig);

            FollowApi api = new FollowApi(http, accounts);

            // act
            bool[] response = await api.CheckUsersFollowPlaylist("2v3iNvBX8Ay1Gt2uXtUKUT", 
                new string[] { "possan", "elogain" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsTrue(condition: response.Any());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task FollowArtists_ArtistIds_IsTrue()
        {
            // arrange
            HttpClient http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            AccountsService accounts = new AccountsService(http, testConfig);

            FollowApi api = new FollowApi(http, accounts);

            // act
            await api.FollowArtists(
                new string[] { "74ASZWbe4lXaubB36ztrGX" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // checking if artists were followed successfully
            bool[] response = await api.CheckCurrentUserFollowsArtists(new string[] { "74ASZWbe4lXaubB36ztrGX" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsTrue(condition: response.FirstOrDefault());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task FollowUsers_UserIds_IsTrue()
        {
            // arrange
            HttpClient http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            AccountsService accounts = new AccountsService(http, testConfig);

            FollowApi api = new FollowApi(http, accounts);

            // act
            await api.FollowUsers(
                new string[] { "exampleuser01" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // checking if users were followed successfully
            bool[] response = await api.CheckCurrentUserFollowsUsers(
                new string[] { "exampleuser01" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsTrue(condition: response.FirstOrDefault());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task FollowPlaylist_PlaylistId_IsPublic_IsTrue()
        {
            // arrange
            HttpClient http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            AccountsService accounts = new AccountsService(http, testConfig);

            FollowApi api = new FollowApi(http, accounts);

            // act
            await api.FollowPlaylist(
                "2v3iNvBX8Ay1Gt2uXtUKUT",
                true,
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // getting usersApi to get user.Id to avoid hardcoding
            UsersProfileApi usersApi = new UsersProfileApi(http, accounts);

            // getting current users profile for user.Id
            User user = await usersApi.GetCurrentUsersProfile(
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // checking to see if user is following the playlist
            bool[] response = await api.CheckUsersFollowPlaylist("2v3iNvBX8Ay1Gt2uXtUKUT",
                new string[] { user.Id },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            Assert.IsTrue(condition: response.FirstOrDefault());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetUsersFollowedArtists_IsNotNull()
        {
            // arrange
            HttpClient http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            AccountsService accounts = new AccountsService(http, testConfig);

            FollowApi api = new FollowApi(http, accounts);

            // act
            PagedArtists response = await api.GetUsersFollowedArtists(
                accessToken: testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsNotNull(value: response);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task UnfollowArtists_ArtistIds_IsFalse()
        {
            // arrange
            HttpClient http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            AccountsService accounts = new AccountsService(http, testConfig);

            FollowApi api = new FollowApi(http, accounts);

            // act
            await api.UnfollowArtists(new string[] { "74ASZWbe4lXaubB36ztrGX" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // checking if user unfollowed artist id
            bool[] response = await api.CheckCurrentUserFollowsArtists(new string[] { "74ASZWbe4lXaubB36ztrGX" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsFalse(condition: response.FirstOrDefault());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task UnfollowUsers_UserIds_IsFalse()
        {
            // arrange
            HttpClient http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            AccountsService accounts = new AccountsService(http, testConfig);

            FollowApi api = new FollowApi(http, accounts);

            // act
            await api.UnfollowUsers(new string[] { "exampleuser01" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // checking if user unfollowed artist id
            bool[] response = await api.CheckCurrentUserFollowsUsers(new string[] { "exampleuser01" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsFalse(condition: response.FirstOrDefault());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task UnfollowPlaylist_PlaylistId_IsFalse()
        {
            // arrange
            HttpClient http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            AccountsService accounts = new AccountsService(http, testConfig);

            FollowApi api = new FollowApi(http, accounts);

            // act
            await api.UnfollowPlaylist("2v3iNvBX8Ay1Gt2uXtUKUT",
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // getting usersApi to get user.Id to avoid hardcoding
            UsersProfileApi usersApi = new UsersProfileApi(http, accounts);

            // getting current users profile for user.Id
            User user = await usersApi.GetCurrentUsersProfile(
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // checking if user unfollowed artist id
            bool[] response = await api.CheckUsersFollowPlaylist("2v3iNvBX8Ay1Gt2uXtUKUT",
                new string[] { user.Id },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsFalse(condition: response.FirstOrDefault());
        }
    }
}
