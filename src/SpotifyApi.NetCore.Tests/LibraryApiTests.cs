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
        LibraryApi api;
        string bearerAccessToken;

        [TestInitialize]
        public void Initialize()
        {
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            bearerAccessToken = testConfig.GetValue(typeof(string),
                "SpotifyUserBearerAccessToken").ToString();
            var accounts = new AccountsService(http, testConfig);
            api = new LibraryApi(http, accounts);
        }

        [TestCategory("Integration")]
        [TestCategory("User")]
        [TestMethod]
        public async Task CheckUserSavedAlbums_AlbumIds_AnyItems()
        {
            // assert
            Assert.IsTrue((await api.CheckUsersSavedAlbums(
                new string[] { "07bYtmE3bPsLB6ZbmmFi8d" },
                bearerAccessToken)).Any<bool>());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task CheckUserSavedShows_ShowIds_AnyItems()
        {
            // assert
            Assert.IsTrue((await api.CheckUsersSavedShows(
                new string[] { "5AvwZVawapvyhJUIx71pdJ" },
                bearerAccessToken)).Any<bool>());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task CheckUserSavedTracks_TrackIds_AnyItems()
        {
            // assert
            Assert.IsTrue((await api.CheckUsersSavedTracks(
                new string[] { "0udZHhCi7p1YzMlvI4fXoK" },
                bearerAccessToken)).Any<bool>());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetUserSavedAlbums_IsNotNull()
        {
            // assert
            Assert.IsNotNull(await api.GetCurrentUsersSavedAlbums(market: "",
                accessToken: bearerAccessToken));
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetUserSavedShows_IsNotNull()
        {
            // assert
            Assert.IsNotNull(await api.GetUsersSavedShows(
                accessToken: bearerAccessToken));
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetUserSavedTracks_IsNotNull()
        {
            // assert
            Assert.IsNotNull(await api.GetUsersSavedTracks(
                accessToken: bearerAccessToken));
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task RemoveAlbumsForCurrentUser_AlbumIds_IsFalse()
        {
            // act
            await api.RemoveAlbumsForCurrentUser(
                new string[] { "07bYtmE3bPsLB6ZbmmFi8d" },
                bearerAccessToken);

            // assert
            // checking if album was removed for the current user.
            Assert.IsFalse((await api.CheckUsersSavedAlbums(
                new string[] { "07bYtmE3bPsLB6ZbmmFi8d" },
                bearerAccessToken)).FirstOrDefault<bool>());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task RemoveUserSavedShows_ShowIds_IsFalse()
        {
            // act
            await api.RemoveUsersSavedShows(
                new string[] { "5AvwZVawapvyhJUIx71pdJ" }, "ES",
                bearerAccessToken);

            // assert
            // checking if show was removed for the current user
            Assert.IsFalse((await api.CheckUsersSavedShows(
                new string[] { "5AvwZVawapvyhJUIx71pdJ" },
                bearerAccessToken)).FirstOrDefault<bool>());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task RemoveUserSavedTracks_TrackIds_IsFalse()
        {
            // act
            await api.RemoveUsersSavedTracks(new string[] { "4iV5W9uYEdYUVa79Axb7Rh" },
                bearerAccessToken);

            // assert
            // checking if track was removed for the current user
            Assert.IsFalse((await api.CheckUsersSavedTracks(
                new string[] { "4iV5W9uYEdYUVa79Axb7Rh" },
                bearerAccessToken)).FirstOrDefault<bool>());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task SaveAlbumsForCurrentUser_AlbumIds_IsTrue()
        {
            // act
            await api.SaveAlbumsForCurrentUser(
                new string[] { "07bYtmE3bPsLB6ZbmmFi8d" },
                bearerAccessToken);

            // assert
            // checking if album was saved for the current user
            Assert.IsTrue((await api.CheckUsersSavedAlbums(
                new string[] { "07bYtmE3bPsLB6ZbmmFi8d" },
                bearerAccessToken)).FirstOrDefault<bool>());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task SaveShowsForCurrentUser_ShowIds_IsTrue()
        {
            // act
            await api.SaveShowsForCurrentUser(
                new string[] { "5AvwZVawapvyhJUIx71pdJ" },
                bearerAccessToken);

            // assert
            // checking if show was saved for the current user
            Assert.IsTrue((await api.CheckUsersSavedShows(
                new string[] { "5AvwZVawapvyhJUIx71pdJ" },
                bearerAccessToken)).FirstOrDefault<bool>());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task SaveTracksForCurrentUser_TrackIds_IsTrue()
        {
            // act
            await api.SaveTracksForUser(
                new string[] { "7ouMYWpwJ422jRcDASZB7P" },
                bearerAccessToken);

            // assert
            // checking if track was saved for the current user
            Assert.IsTrue((await api.CheckUsersSavedTracks(
                new string[] { "7ouMYWpwJ422jRcDASZB7P" },
                bearerAccessToken)).FirstOrDefault<bool>());
        }
    }
}
