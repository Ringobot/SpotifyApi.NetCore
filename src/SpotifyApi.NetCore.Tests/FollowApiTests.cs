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
        FollowApi api;
        UsersProfileApi usersApi;
        string bearerAccessToken;

        [TestInitialize]
        public void Initialize()
        {
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            bearerAccessToken = testConfig.GetValue(typeof(string), 
                "SpotifyUserBearerAccessToken").ToString();
            var accounts = new AccountsService(http, testConfig);
            api = new FollowApi(http, accounts);
            usersApi = new UsersProfileApi(http, accounts);
        }

        [TestCategory("Integration")]
        [TestCategory("User")]
        [TestMethod]
        public async Task CheckCurrentUserFollowsArtists_ArtistId_AnyItems()
        {
            // assert
            Assert.IsTrue(condition: (await api.CheckCurrentUserFollowsArtists(
                new string[] { "74ASZWbe4lXaubB36ztrGX" },
                bearerAccessToken)).Any());
        }

        [TestCategory("Integration")]
        [TestCategory("User")]
        [TestMethod]
        public async Task CheckCurrentUserFollowsUsers_UserId_AnyItems()
        {
            // assert.
            Assert.IsTrue(condition: (await api.CheckCurrentUserFollowsUsers(
                new string[] { "exampleuser01" },
                bearerAccessToken)).Any());
        }

        [TestCategory("Integration")]
        [TestCategory("User")]
        [TestMethod]
        public async Task CheckUsersFollowPlaylist_PlaylistId_UserIds_AnyItems()
        {
            // assert
            Assert.IsTrue(condition: (await api.CheckUsersFollowPlaylist("2v3iNvBX8Ay1Gt2uXtUKUT",
                new string[] { "possan", "elogain" },
                bearerAccessToken)).Any());
        }

        [TestCategory("Integration")]
        [TestCategory("User")]
        [TestMethod]
        public async Task FollowArtists_ArtistIds_IsTrue()
        {
            // act. Follow artists.
            await api.FollowArtists(
                new string[] { "74ASZWbe4lXaubB36ztrGX" },
                bearerAccessToken);

            // assert. 
            // checking if artists were successfully followed.
            Assert.IsTrue(condition: (await api.CheckCurrentUserFollowsArtists(
                new string[] { "74ASZWbe4lXaubB36ztrGX" },
                bearerAccessToken)).FirstOrDefault());
        }

        [TestCategory("Integration")]
        [TestCategory("User")]
        [TestMethod]
        public async Task FollowArtists_Unfollowed_CheckUserFollowsTrue()
        {
            // arrange
            string[] artistIds = new string[] { "74ASZWbe4lXaubB36ztrGX" };

            // unfollow
            await api.UnfollowArtists(artistIds, bearerAccessToken);

            // check if artist has been unfollowed
            Assert.IsFalse((await api.CheckCurrentUserFollowsArtists(
                artistIds,
                bearerAccessToken)).First(),
                "Artist should have been unfollowed at this point.");

            // act
            await api.FollowArtists(artistIds, bearerAccessToken);

            // assert
            // check if artist was followed successfully
            Assert.IsTrue((await api.CheckCurrentUserFollowsArtists(
                artistIds,
                bearerAccessToken)).First(),
                "Artist should have been followed at this point");
        }

        [TestCategory("Integration")]
        [TestCategory("User")]
        [TestMethod]
        public async Task FollowUsers_Unfollowed_CheckUserFollowsTrue()
        {
            // arrange
            string[] userIds = new string[] { "exampleuser01" };

            // unfollow
            await api.UnfollowUsers(userIds, bearerAccessToken);

            // check if user was unfollowed
            Assert.IsFalse((await api.CheckCurrentUserFollowsUsers(
                userIds,
                bearerAccessToken)).First(),
                "User should have been unfollowed at this point");

            // act
            await api.FollowUsers(userIds, bearerAccessToken);

            // assert
            // check if user was followed successfully
            Assert.IsTrue((await api.CheckCurrentUserFollowsUsers(
                userIds,
                bearerAccessToken)).First(),
                "User should have been followed at this point");
        }

        [TestCategory("Integration")]
        [TestCategory("User")]
        [TestMethod]
        public async Task FollowUsers_UserIds_IsTrue()
        {
            // act
            await api.FollowUsers(
                new string[] { "exampleuser01" },
                bearerAccessToken);

            // assert
            // checking if users were followed successfully.
            Assert.IsTrue(condition: (await api.CheckCurrentUserFollowsUsers(
                new string[] { "exampleuser01" },
                bearerAccessToken)).FirstOrDefault());
        }

        [TestCategory("Integration")]
        [TestCategory("User")]
        [TestMethod]
        public async Task FollowPlaylist_PlaylistId_IsPublic_IsTrue()
        {
            // act
            await api.FollowPlaylist(
                "2v3iNvBX8Ay1Gt2uXtUKUT",
                true,
                bearerAccessToken);

            // assert
            // get current users profile for user.Id
            // checking to see if user is following the playlist
            Assert.IsTrue(condition: (await api.CheckUsersFollowPlaylist(
                "2v3iNvBX8Ay1Gt2uXtUKUT",
                new string[] { (await usersApi.GetCurrentUsersProfile(
                bearerAccessToken)).Id },
                bearerAccessToken)).FirstOrDefault());
        }

        [TestCategory("Integration")]
        [TestCategory("User")]
        [TestMethod]
        public async Task GetUsersFollowedArtists_IsNotNull()
        {
            // assert
            Assert.IsNotNull(value: await api.GetUsersFollowedArtists(
                accessToken: bearerAccessToken));
        }

        [TestCategory("Integration")]
        [TestCategory("User")]
        [TestMethod]
        public async Task UnfollowArtists_ArtistIds_IsFalse()
        {
            // act
            await api.UnfollowArtists(new string[] { "74ASZWbe4lXaubB36ztrGX" },
                bearerAccessToken);

            // assert
            // checking if user unfollowed artist id
            Assert.IsFalse(condition: (await api.CheckCurrentUserFollowsArtists(
                new string[] { "74ASZWbe4lXaubB36ztrGX" },
                bearerAccessToken)).FirstOrDefault());
        }

        [TestCategory("Integration")]
        [TestCategory("User")]
        [TestMethod]
        public async Task UnfollowUsers_UserIds_IsFalse()
        {
            // act
            await api.UnfollowUsers(new string[] { "exampleuser01" },
                bearerAccessToken);

            // assert
            // checking if user unfollowed artist id
            Assert.IsFalse(condition: (await api.CheckCurrentUserFollowsUsers(
                new string[] { "exampleuser01" },
                bearerAccessToken)).FirstOrDefault());
        }

        [TestCategory("Integration")]
        [TestCategory("User")]
        [TestMethod]
        public async Task UnfollowPlaylist_PlaylistId_IsFalse()
        {
            // act
            await api.UnfollowPlaylist("2v3iNvBX8Ay1Gt2uXtUKUT",
                bearerAccessToken);

            // assert
            // get current users profile for user.Id
            // checking if user unfollowed artist id
            Assert.IsFalse(condition: (await api.CheckUsersFollowPlaylist(
                "2v3iNvBX8Ay1Gt2uXtUKUT",
                new string[] { (await usersApi.GetCurrentUsersProfile(
                bearerAccessToken)).Id },
                bearerAccessToken)).FirstOrDefault());
        }
    }
}
