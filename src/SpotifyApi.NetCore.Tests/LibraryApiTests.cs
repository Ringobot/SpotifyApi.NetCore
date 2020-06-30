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
    public class LibraryApiTests
    {
        [TestCategory("Integration")]
        [TestCategory("User")]
        [TestMethod]
        public async Task CheckUserSavedAlbums_AlbumIds_AnyItems()
        {
            // arrange
            HttpClient http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            AccountsService accounts = new AccountsService(http, testConfig);

            LibraryApi api = new LibraryApi(http, accounts);

            // act
            bool[] response = await api.CheckUsersSavedAlbums(new string[] { "07bYtmE3bPsLB6ZbmmFi8d" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsTrue(response.Any<bool>());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task CheckUserSavedShows_ShowIds_AnyItems()
        {
            // arrange
            HttpClient http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            AccountsService accounts = new AccountsService(http, testConfig);

            LibraryApi api = new LibraryApi(http, accounts);

            // act
            bool[] response = await api.CheckUsersSavedShows(new string[] { "5AvwZVawapvyhJUIx71pdJ" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsTrue(response.Any<bool>());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task CheckUserSavedTracks_TrackIds_AnyItems()
        {
            // arrange
            HttpClient http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            AccountsService accounts = new AccountsService(http, testConfig);

            LibraryApi api = new LibraryApi(http, accounts);

            // act
            bool[] response = await api.CheckUsersSavedTracks(new string[] { "0udZHhCi7p1YzMlvI4fXoK" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsTrue(response.Any<bool>());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetUserSavedAlbums_IsNotNull()
        {
            // arrange
            HttpClient http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            AccountsService accounts = new AccountsService(http, testConfig);

            LibraryApi api = new LibraryApi(http, accounts);

            // act
            PagedAlbums response = await api.GetCurrentUsersSavedAlbums(market: "",
                accessToken: testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsNotNull(response);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetUserSavedShows_IsNotNull()
        {
            // arrange
            HttpClient http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            AccountsService accounts = new AccountsService(http, testConfig);

            LibraryApi api = new LibraryApi(http, accounts);

            // act
            PagedShows response = await api.GetUsersSavedShows(
                accessToken: testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsNotNull(response);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetUserSavedTracks_IsNotNull()
        {
            // arrange
            HttpClient http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            AccountsService accounts = new AccountsService(http, testConfig);

            LibraryApi api = new LibraryApi(http, accounts);

            // act
            PagedTracks response = await api.GetUsersSavedTracks(
                accessToken: testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsNotNull(response);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task RemoveAlbumsForCurrentUser_AlbumIds_IsFalse()
        {
            // arrange
            HttpClient http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            AccountsService accounts = new AccountsService(http, testConfig);

            LibraryApi api = new LibraryApi(http, accounts);

            // act
            await api.RemoveAlbumsForCurrentUser(new string[] { "07bYtmE3bPsLB6ZbmmFi8d" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // checking if user unfollowed artist id
            bool[] response = await api.CheckUsersSavedAlbums(new string[] { "07bYtmE3bPsLB6ZbmmFi8d" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsFalse(response.FirstOrDefault<bool>());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task RemoveUserSavedShows_ShowIds_IsFalse()
        {
            // arrange
            HttpClient http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            AccountsService accounts = new AccountsService(http, testConfig);

            LibraryApi api = new LibraryApi(http, accounts);

            // act
            await api.RemoveUsersSavedShows(new string[] { "5AvwZVawapvyhJUIx71pdJ" }, "ES",
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // checking if user unfollowed artist id
            bool[] response = await api.CheckUsersSavedShows(new string[] { "5AvwZVawapvyhJUIx71pdJ" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsFalse(response.FirstOrDefault<bool>());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task RemoveUserSavedTracks_TrackIds_IsFalse()
        {
            // arrange
            HttpClient http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            AccountsService accounts = new AccountsService(http, testConfig);

            LibraryApi api = new LibraryApi(http, accounts);

            // act
            await api.RemoveUsersSavedTracks(new string[] { "4iV5W9uYEdYUVa79Axb7Rh" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // checking if user unfollowed artist id
            bool[] response = await api.CheckUsersSavedTracks(new string[] { "4iV5W9uYEdYUVa79Axb7Rh" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsFalse(response.FirstOrDefault<bool>());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task SaveAlbumsForCurrentUser_AlbumIds_IsTrue()
        {
            // arrange
            HttpClient http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            AccountsService accounts = new AccountsService(http, testConfig);

            LibraryApi api = new LibraryApi(http, accounts);

            // act
            await api.SaveAlbumsForCurrentUser(
                new string[] { "07bYtmE3bPsLB6ZbmmFi8d" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // checking if users were followed successfully
            bool[] response = await api.CheckUsersSavedAlbums(
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
            HttpClient http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            AccountsService accounts = new AccountsService(http, testConfig);

            LibraryApi api = new LibraryApi(http, accounts);

            // act
            await api.SaveShowsForCurrentUser(
                new string[] { "5AvwZVawapvyhJUIx71pdJ" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // checking if users were followed successfully
            bool[] response = await api.CheckUsersSavedShows(
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
            HttpClient http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            AccountsService accounts = new AccountsService(http, testConfig);

            LibraryApi api = new LibraryApi(http, accounts);

            // act
            await api.SaveTracksForUser(
                new string[] { "7ouMYWpwJ422jRcDASZB7P" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // checking if users were followed successfully
            bool[] response = await api.CheckUsersSavedTracks(
                new string[] { "7ouMYWpwJ422jRcDASZB7P" },
                testConfig.GetValue(typeof(string), "SpotifyUserBearerAccessToken").ToString());

            // assert
            Assert.IsTrue(response.FirstOrDefault<bool>());
        }
    }
}
