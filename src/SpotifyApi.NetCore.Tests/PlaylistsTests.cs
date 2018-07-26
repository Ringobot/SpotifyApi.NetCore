using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using SpotifyApi.NetCore.Http;
using SpotifyApi.NetCore.Tests.Http;

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
            var accounts = new AccountsService(mockHttp.HttpClient, TestsHelper.GetLocalConfig());
            var playlists = new PlaylistsApi(mockHttp.HttpClient, accounts);

            // Act
            var result1 = await playlists.GetPlaylists(username);
            Trace.WriteLine((object)result1);
        }
	
        [TestMethod]
        public async Task GetPlaylists_Username_GetAccessTokenCalled()
        {
            // Arrange
            const string username = "abc123";

            var mockHttp = new MockHttpClient();
            mockHttp.SetupSendAsync("{\"Id\":\"def456\",\"Name\":\"ghi789\"}");
            var accounts = new AccountsService(mockHttp.HttpClient, TestsHelper.GetLocalConfig());

            var api = new PlaylistsApi(mockHttp.HttpClient, accounts);

            // Act
            await api.GetPlaylists(username);

            // Assert
            Assert.Fail();
            //mockAuth.Verify(a=>a.GetAccessToken());
        }
		
        [TestMethod]
        public async Task GetTracks_UsernameAndPlaylistId_GetAccessTokenCalled()
        {
            // Arrange
            const string username = "abc123";
            const string playlistId = "jkl012";

            var mockHttp = new MockHttpClient();
            mockHttp.SetupSendAsync("{\"Id\":\"def456\",\"Name\":\"ghi789\"}");
            var accounts = new AccountsService(mockHttp.HttpClient, TestsHelper.GetLocalConfig());

            var api = new PlaylistsApi(mockHttp.HttpClient, accounts);

            // Act
            await api.GetTracks(username, playlistId);

            // Assert
            Assert.Fail();
            //mockAuth.Verify(a => a.GetAccessToken());
        }
	
    }

    public class TestPlaylistsModel
    {
        internal string Id { get; set; }
        internal string Name { get; set; }
    }
}
