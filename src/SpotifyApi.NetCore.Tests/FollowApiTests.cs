using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using SpotifyApi.NetCore.Authorization;
using SpotifyApi.NetCore.Tests.Mocks;

namespace SpotifyApi.NetCore.Tests
{
    [TestClass]
    public class FollowApiTests
    {
        /* For new users:: You need to provide the scope "user-follow-read". If you dont have this in your
         * bearer token then it will fail with authentication errors. */

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
                new string[] { "jmperezperez", "thelinmichael", "wizzler" },
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
    }
}
