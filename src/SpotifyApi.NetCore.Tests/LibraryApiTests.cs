using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpotifyApi.NetCore.Authorization;

namespace SpotifyApi.NetCore.Tests
{
    [TestClass]
    public class LibraryApiTests
    {
        [TestCategory("Integration")]
        [TestMethod]
        public async Task CheckUserSavedAlbums_AlbumIds_AnyItems()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            var api = new LibraryApi(http, accounts);

            // act
            var response = await api.CheckUserSavedAlbums(new string[] { "07bYtmE3bPsLB6ZbmmFi8d" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsTrue(response.Any<bool>());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task CheckUserSavedShows_ShowIds_AnyItems()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            var api = new LibraryApi(http, accounts);

            // act
            var response = await api.CheckUserSavedShows(new string[] { "5AvwZVawapvyhJUIx71pdJ" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsTrue(response.Any<bool>());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task CheckUserSavedTracks_TrackIds_AnyItems()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            var api = new LibraryApi(http, accounts);

            // act
            var response = await api.CheckUserSavedTracks(new string[] { "0udZHhCi7p1YzMlvI4fXoK" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsTrue(response.Any<bool>());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetUserSavedAlbums_IsNotNull()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            var api = new LibraryApi(http, accounts);

            // act
            var response = await api.GetUserSavedAlbums(market: "",
                accessToken: testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsNotNull(response);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetUserSavedShows_IsNotNull()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            var api = new LibraryApi(http, accounts);

            // act
            var response = await api.GetUserSavedShows(
                accessToken: testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsNotNull(response);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetUserSavedTracks_IsNotNull()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            var api = new LibraryApi(http, accounts);

            // act
            var response = await api.GetUserSavedTracks(
                accessToken: testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsNotNull(response);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task RemoveAlbumsForCurrentUser_AlbumIds_IsFalse()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            var api = new LibraryApi(http, accounts);

            // act
            await api.RemoveAlbumsForCurrentUser(new string[] { "07bYtmE3bPsLB6ZbmmFi8d" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // checking if user unfollowed artist id
            var response = await api.CheckUserSavedAlbums(new string[] { "07bYtmE3bPsLB6ZbmmFi8d" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsFalse(response.FirstOrDefault<bool>());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task RemoveUserSavedShows_ShowIds_IsFalse()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            var api = new LibraryApi(http, accounts);

            // act
            await api.RemoveUserSavedShows(new string[] { "5AvwZVawapvyhJUIx71pdJ" }, "ES",
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // checking if user unfollowed artist id
            var response = await api.CheckUserSavedShows(new string[] { "5AvwZVawapvyhJUIx71pdJ" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsFalse(response.FirstOrDefault<bool>());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task RemoveUserSavedTracks_TrackIds_IsFalse()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            var api = new LibraryApi(http, accounts);

            // act
            await api.RemoveUserSavedTracks(new string[] { "4iV5W9uYEdYUVa79Axb7Rh" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // checking if user unfollowed artist id
            var response = await api.CheckUserSavedTracks(new string[] { "4iV5W9uYEdYUVa79Axb7Rh" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsFalse(response.FirstOrDefault<bool>());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task SaveAlbumsForCurrentUser_AlbumIds_IsTrue()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            var api = new LibraryApi(http, accounts);

            // act
            await api.SaveAlbumsForCurrentUser(
                new string[] { "07bYtmE3bPsLB6ZbmmFi8d" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // checking if users were followed successfully
            var response = await api.CheckUserSavedAlbums(
                new string[] { "07bYtmE3bPsLB6ZbmmFi8d" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsTrue(response.FirstOrDefault<bool>());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task SaveShowsForCurrentUser_ShowIds_IsTrue()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            var api = new LibraryApi(http, accounts);

            // act
            await api.SaveShowsForCurrentUser(
                new string[] { "5AvwZVawapvyhJUIx71pdJ" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // checking if users were followed successfully
            var response = await api.CheckUserSavedShows(
                new string[] { "5AvwZVawapvyhJUIx71pdJ" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsTrue(response.FirstOrDefault<bool>());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task SaveTracksForCurrentUser_TrackIds_IsTrue()
        {
            // arrange
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            var accounts = new AccountsService(http, testConfig);

            var api = new LibraryApi(http, accounts);

            // act
            await api.SaveTracksForCurrentUser(
                new string[] { "7ouMYWpwJ422jRcDASZB7P" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // checking if users were followed successfully
            var response = await api.CheckUserSavedTracks(
                new string[] { "7ouMYWpwJ422jRcDASZB7P" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsTrue(response.FirstOrDefault<bool>());
        }
    }
}
