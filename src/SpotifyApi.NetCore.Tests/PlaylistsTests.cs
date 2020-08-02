using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpotifyApi.NetCore.Authorization;
using SpotifyApi.NetCore.Tests.Mocks;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Castle.Core.Internal;

namespace SpotifyApi.NetCore.Tests
{
    [TestClass]
    public class PlaylistsTests
    {
        PlaylistsApi api;
        UsersProfileApi usersApi;
        string bearerAccessToken;

        public void Initialize()
        {
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            bearerAccessToken = testConfig.GetValue(typeof(string),
                "SpotifyUserBearerAccessToken").ToString();
            var accounts = new AccountsService(http, testConfig);
            api = new PlaylistsApi(http, accounts);
            usersApi = new UsersProfileApi(http, accounts);
        }

        [TestMethod]
        public async Task BasicUsage()
        {
            // Arrange
            const string username = "abc123";

            var mockHttp = new MockHttpClient();
            mockHttp.SetupSendAsync("{\"Id\":\"def456\",\"Name\":\"ghi789\"}");
            var accounts = new MockAccountsService().Object;
            var playlists = new PlaylistsApi(mockHttp.HttpClient, accounts);

            // Act
            var result1 = await playlists.GetPlaylists(username);
            Trace.WriteLine(result1);
        }

        [TestMethod]
        public async Task GetPlaylists_Username_GetAccessTokenCalled()
        {
            // Arrange
            const string username = "abc123";

            var mockHttp = new MockHttpClient();
            mockHttp.SetupSendAsync("{\"Id\":\"def456\",\"Name\":\"ghi789\"}");
            var mockAccounts = new MockAccountsService();

            var api = new PlaylistsApi(mockHttp.HttpClient, mockAccounts.Object);

            // Act
            await api.GetPlaylists(username);

            // Assert
            mockAccounts.Verify(a => a.GetAccessToken());
        }

        [TestMethod]
        public async Task GetTracks_UsernameAndPlaylistId_GetAccessTokenCalled()
        {
            // Arrange
            const string playlistId = "jkl012";

            var mockHttp = new MockHttpClient();
            mockHttp.SetupSendAsync("{\"Id\":\"def456\",\"Name\":\"ghi789\"}");
            var mockAccounts = new MockAccountsService();

            var api = new PlaylistsApi(mockHttp.HttpClient, mockAccounts.Object);

            // Act
            await api.GetTracks(playlistId);

            // Assert
            mockAccounts.Verify(a => a.GetAccessToken());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task SearchPlaylists_PlaylistName_AnyItems()
        {
            // arrange
            const string query = "dance";

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new PlaylistsApi(http, accounts);

            // act
            var response = await api.SearchPlaylists(query);

            // assert
            Assert.IsTrue(response.Items.Any());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetTracks_ReturnsValidPlaylistTracks()
        {
            // Arrange
            const string playlistId = "37i9dQZF1DX3WvGXE8FqYX";

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new PlaylistsApi(http, accounts);

            // Act
            var response = await api.GetTracks(playlistId);

            // Assert
            Assert.IsNotNull(response.Items);
            Assert.IsTrue(response.Items.Length > 0);
            Assert.IsTrue(response.Items[0].Track.Name.Length > 0);
            Assert.IsTrue(response.Items[0].Track.Album.Name.Length > 0);
            Assert.IsTrue(response.Items[0].Track.Artists.Length > 0);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetTracks_NewEndpoint_ReturnsValidPlaylistTracks()
        {
            // Arrange
            const string playlistId = "37i9dQZF1DX3WvGXE8FqYX";

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new PlaylistsApi(http, accounts);

            // Act
            var response = await api.GetTracks(playlistId);

            // Assert
            Assert.IsNotNull(response.Items);
            Assert.IsTrue(response.Items.Length > 0);
            Assert.IsTrue(response.Items[0].Track.Name.Length > 0);
            Assert.IsTrue(response.Items[0].Track.Album.Name.Length > 0);
            Assert.IsTrue(response.Items[0].Track.Artists.Length > 0);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetTracks_NewAccountsService_ReturnsTracks()
        {
            // Testing @DanixPC's use case
            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());
            var plapi = new PlaylistsApi(http, accounts);
            var pl = await plapi.GetTracks("4h4urfIy5cyCdFOc1Ff4iN");

            var pl_tr = pl.Items.ElementAt(0).Track.Name;

            var pl_page_2 = await plapi.GetTracks("4h4urfIy5cyCdFOc1Ff4iN", offset: 100);
            var pl_page_2_tr = pl_page_2.Items.ElementAt(0).Track.Name;
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task AddItemsToPlaylist_PlaylistId_Uris_IsTrue()
        {
            Initialize();
            // assert
            Assert.IsTrue(!(await api.AddItemsToPlaylist(
                "7drl9CTMIGaISOxYrWpgkX",   //akshay
                //"36VigQERhufZcP4Sh7rG5I", //dan
                new string[] { "spotify:track:4iV5W9uYEdYUVa79Axb7Rh", "spotify:track:1301WleyT98MSxVHPZCA6M" },
                accessToken: bearerAccessToken)).SnapshotId.IsNullOrEmpty());
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task ChangePlaylistDetails_PlaylistId()
        {
            Initialize();
            // assert
            await api.ChangePlaylistDetails("7drl9CTMIGaISOxYrWpgkX", "Akshay Srinivasan's Great Music",
                accessToken: bearerAccessToken);
            Assert.IsTrue(true);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task CreatePlaylist_UserId()
        {
            Initialize();
            // assert
            Playlist playlist = await api.CreatePlaylist((await usersApi.GetCurrentUsersProfile(
                bearerAccessToken)).Id, "Akshay Srinivasan Test Playlist",
                accessToken: bearerAccessToken);
            Assert.IsTrue(playlist.Name == "Akshay Srinivasan Test Playlist");
        }
    }
}
