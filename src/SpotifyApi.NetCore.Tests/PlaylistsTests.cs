using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpotifyApi.NetCore.Authorization;
using SpotifyApi.NetCore.Tests.Mocks;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Castle.Core.Internal;
using System;

namespace SpotifyApi.NetCore.Tests
{
    [TestClass]
    public class PlaylistsTests
    {
        PlaylistsApi api;
        UsersProfileApi usersApi;

        [TestInitialize]
        public void Initialize()
        {
            var http = new HttpClient();
            IConfiguration testConfig = TestsHelper.GetLocalConfig();
            api = new PlaylistsApi(http);
            usersApi = new UsersProfileApi(http);
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

        private async Task<Playlist> CreatePlaylist(string accessToken, string description = null)
        {
            string playlistName = $"_Test{Guid.NewGuid():N}";
            var details = new PlaylistDetails { Name = playlistName, Description = description };
            

            return await api.CreatePlaylist(
                (await usersApi.GetCurrentUsersProfile(accessToken)).Id,
                details,
                accessToken: accessToken);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task AddItemsToPlaylist_PlaylistId_SnapshotIdIsNotNull()
        {
            // arrange
            string accessToken = await TestsHelper.GetUserAccessToken();
            var playlist = await CreatePlaylist(accessToken);

            // assert
            Assert.IsNotNull((await api.AddItemsToPlaylist(
                playlist.Id,
                new string[] { "spotify:track:4iV5W9uYEdYUVa79Axb7Rh", "spotify:track:1301WleyT98MSxVHPZCA6M" },
                accessToken: accessToken)).SnapshotId);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task ChangePlaylistDetails_NewName_PlaylistNameEqualsNewName()
        {
            string accessToken = await TestsHelper.GetUserAccessToken();
            string description = $"_Test{Guid.NewGuid():N}";
            string newName = $"_Test{Guid.NewGuid():N}";
            var playlist = await CreatePlaylist(accessToken, description);

            // act
            await api.ChangePlaylistDetails(playlist.Id, new PlaylistDetails { Name = newName },
                accessToken: await TestsHelper.GetUserAccessToken());
            
            // assert
            var changedPlaylist = await api.GetPlaylist(playlist.Id, accessToken);
            Assert.AreEqual(newName, changedPlaylist.Name);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task ChangePlaylistDetails_NewNameDescriptionNotChanged_PlaylistDescriptionEqualsOriginalDescription()
        {
            string accessToken = await TestsHelper.GetUserAccessToken();
            string description = $"_Test{Guid.NewGuid():N}";
            string newName = $"_Test{Guid.NewGuid():N}";
            var playlist = await CreatePlaylist(accessToken, description);

            // act
            await api.ChangePlaylistDetails(playlist.Id, new PlaylistDetails { Name = newName },
                accessToken: await TestsHelper.GetUserAccessToken());

            // assert
            var changedPlaylist = await api.GetPlaylist(playlist.Id, accessToken);
            Assert.AreEqual(description, changedPlaylist.Description);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task CreatePlaylist_PublicNotSpecified_ResultPublicEqualsTrue()
        {
            string accessToken = await TestsHelper.GetUserAccessToken();
            var playlist = await CreatePlaylist(accessToken);
            Assert.IsNotNull(playlist.Public);
            Assert.IsTrue(playlist.Public.Value, "New playlist should default to Public = true");
        }

        [TestCategory("User")]
        [TestMethod]
        public async Task GetCurrentUsersPlaylists_AtLeastOnePlaylistReturnedIsTrue()
        {
            string accessToken = await TestsHelper.GetUserAccessToken();
            _ = await CreatePlaylist(accessToken);
            var playlists = await api.GetCurrentUsersPlaylists<PagedPlaylists>(accessToken: accessToken);
            Assert.IsTrue(playlists.Total > 0, "No playlists found.");
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetPlaylistCoverImage_PlaylistId_AtLeastOnePlalistCoverImageReturnedIsTrue()
        {
            string accessToken = await TestsHelper.GetUserAccessToken();
            var playlistCoverImages = await api.GetPlaylistCoverImage<Image[]>("3cEYpjA9oz9GiPac4AsH4n", accessToken);
            Assert.IsTrue(playlistCoverImages.Length > 0, "No playlist images were found.");
        }
    }
}
