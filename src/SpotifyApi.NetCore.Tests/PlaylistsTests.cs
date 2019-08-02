using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using SpotifyApi.NetCore.Http;
using SpotifyApi.NetCore.Tests.Http;
using SpotifyApi.NetCore.Tests.Mocks;

namespace SpotifyApi.NetCore.Tests
{
    [TestClass]
    public class PlaylistsTests
    {
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
            mockAccounts.Verify(a=>a.GetAppAccessToken());
        }
		
        [TestMethod]
        public async Task GetTracks_UsernameAndPlaylistId_GetAccessTokenCalled()
        {
            // Arrange
            const string username = "abc123";
            const string playlistId = "jkl012";

            var mockHttp = new MockHttpClient();
            mockHttp.SetupSendAsync("{\"Id\":\"def456\",\"Name\":\"ghi789\"}");
            var mockAccounts = new MockAccountsService();

            var api = new PlaylistsApi(mockHttp.HttpClient, mockAccounts.Object);

            // Act
            await api.GetTracks(username, playlistId);

            // Assert
            mockAccounts.Verify(a => a.GetAppAccessToken());
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
            const string username = "spotify";
            const string playlistId = "37i9dQZF1DX3WvGXE8FqYX";

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new PlaylistsApi(http, accounts);

            // Act
            var response = await api.GetTracks(username, playlistId);

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
            var pl = await plapi.GetTracks("37i9dQZF1DX3WvGXE8FqYX");
            var pl_tr = pl.Items.ElementAt(0).Track.Name;
        }
    }
}
