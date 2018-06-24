using System.Diagnostics;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpotifyApiDotNetCore.Http;

namespace SpotifyApiDotNetCore.Tests
{
    [TestClass]
    public class PlaylistsTests
    {
        [TestMethod]
        public async Task BasicUsage()
        {
            // Arrange
            const string username = "abc123";
            
            var mockHttp = new Mock<IHttpClient>();
            mockHttp.Setup(h => h.Get(It.IsAny<string>(), It.IsAny<AuthenticationHeaderValue>()))
                .ReturnsAsync("{\"Id\":\"def456\",\"Name\":\"ghi789\"}");

            var mockAuth = new Mock<IAuthorizationApi>();

            var playlists = new PlaylistsApi(mockHttp.Object, mockAuth.Object);

            // Act
            var result1 = await playlists.GetPlaylists(username);
            Trace.WriteLine((object)result1);
        }

        [TestMethod]
        public async Task GetPlaylists_Username_HttpGetCalled()
        {
            // Arrange
            const string username = "abc123";

            var mockHttp = new Mock<IHttpClient>();
            mockHttp.Setup(h => h.Get(It.IsAny<string>(), It.IsAny<AuthenticationHeaderValue>()))
                .ReturnsAsync("{\"Id\":\"def456\",\"Name\":\"ghi789\"}");

            var mockAuth = new Mock<IAuthorizationApi>();

            var api = new PlaylistsApi(mockHttp.Object, mockAuth.Object);

            // Act
            await api.GetPlaylists(username);

            // Assert
            mockHttp.Verify(h => h.Get(PlaylistsApi.BaseUrl + "/users/abc123/playlists", It.IsAny<AuthenticationHeaderValue>()));
        }
	
        [TestMethod]
        public async Task GetPlaylists_Username_GetAccessTokenCalled()
        {
            // Arrange
            const string username = "abc123";

            var mockHttp = new Mock<IHttpClient>();
            mockHttp.Setup(h => h.Get(It.IsAny<string>(), It.IsAny<AuthenticationHeaderValue>()))
                .ReturnsAsync("{\"Id\":\"def456\",\"Name\":\"ghi789\"}");

            var mockAuth = new Mock<IAuthorizationApi>();

            var api = new PlaylistsApi(mockHttp.Object, mockAuth.Object);

            // Act
            await api.GetPlaylists(username);

            // Assert
            mockAuth.Verify(a=>a.GetAccessToken());
        }
	
        [TestMethod]
        public async Task GetTracks_UsernameAndPlaylistId_HttpGetCalled()
        {
            // Arrange
            const string username = "abc123";
            const string playlistId = "jkl012";

            var mockHttp = new Mock<IHttpClient>();
            mockHttp.Setup(h => h.Get(It.IsAny<string>(), It.IsAny<AuthenticationHeaderValue>()))
                .ReturnsAsync("{\"Id\":\"def456\",\"Name\":\"ghi789\"}");

            var mockAuth = new Mock<IAuthorizationApi>();

            var api = new PlaylistsApi(mockHttp.Object, mockAuth.Object);

            // Act
            await api.GetTracks(username, playlistId);

            // Assert
            mockHttp.Verify(h => h.Get(PlaylistsApi.BaseUrl + "/users/abc123/playlists/jkl012/tracks", It.IsAny<AuthenticationHeaderValue>()));
        }
	
        [TestMethod]
        public async Task GetTracks_UsernameAndPlaylistId_GetAccessTokenCalled()
        {
            // Arrange
            const string username = "abc123";
            const string playlistId = "jkl012";

            var mockHttp = new Mock<IHttpClient>();
            mockHttp.Setup(h => h.Get(It.IsAny<string>(), It.IsAny<AuthenticationHeaderValue>()))
                .ReturnsAsync("{\"Id\":\"def456\",\"Name\":\"ghi789\"}");

            var mockAuth = new Mock<IAuthorizationApi>();

            var api = new PlaylistsApi(mockHttp.Object, mockAuth.Object);

            // Act
            await api.GetTracks(username, playlistId);

            // Assert
            mockAuth.Verify(a => a.GetAccessToken());
        }
	
    }

    public class TestPlaylistsModel
    {
        internal string Id { get; set; }
        internal string Name { get; set; }
    }
}
