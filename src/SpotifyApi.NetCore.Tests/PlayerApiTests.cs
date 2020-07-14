using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using SpotifyApi.NetCore.Authorization;
using SpotifyApi.NetCore.Models;
using SpotifyApi.NetCore.Tests.Mocks;

namespace SpotifyApi.NetCore.Tests
{
    [TestClass]
    public class PlayerApiTests
    {
        private static IConfiguration _config = TestsHelper.GetLocalConfig();
        private static HttpClient _http = new HttpClient();

        [TestMethod]
        public async Task PlayTracks_AccessToken_PutInvokedWithAccessToken()
        {
            // arrange
            const string token = "abc123";
            const string trackUri = "spotify:track:7ouMYWpwJ422jRcDASZB7P";

            var http = new MockHttpClient();
            http.SetupSendAsync();
            var service = new Mock<PlayerApi>(http.HttpClient, token){CallBase = true};

            // act
            await service.Object.PlayTracks(trackUri, token);

            // assert
            service.Verify(s => s.Put(It.IsAny<string>(), It.IsAny<object>(), token));
        }

        [TestMethod]
        public async Task PlayAlbum_AccessToken_PutInvokedWithAccessToken()
        {
            // arrange
            const string token = "abc123";
            const string uri = "spotify:album:FooBar1234567";

            var http = new MockHttpClient();
            http.SetupSendAsync();
            var service = new Mock<PlayerApi>(http.HttpClient, token) { CallBase = true };

            // act
            await service.Object.PlayAlbum(uri, token);

            // assert
            service.Verify(s => s.Put(It.IsAny<string>(), It.IsAny<object>(), token));
        }

        [TestMethod]
        public async Task PlayArtist_AccessToken_PutInvokedWithAccessToken()
        {
            // arrange
            const string token = "abc123";
            const string id = "0TnOYISbd1XYRBk9myaseg";

            var http = new MockHttpClient();
            http.SetupSendAsync();
            var service = new Mock<PlayerApi>(http.HttpClient, token) { CallBase = true };

            // act
            await service.Object.PlayArtist(id, token);

            // assert
            service.Verify(s => s.Put(It.IsAny<string>(), It.IsAny<object>(), token));
        }

        [TestMethod]
        public async Task PlayPlaylist_AccessToken_PutInvokedWithAccessToken()
        {
            // arrange
            const string token = "abc123";
            const string uri = "spotify:playlist:FooBar1234567";

            var http = new MockHttpClient();
            http.SetupSendAsync();
            var service = new Mock<PlayerApi>(http.HttpClient, token) { CallBase = true };

            // act
            await service.Object.PlayPlaylist(uri, token);

            // assert
            service.Verify(s => s.Put(It.IsAny<string>(), It.IsAny<object>(), token));
        }

        [TestMethod]
        public async Task Play_AccessToken_PutInvokedWithAccessToken()
        {
            // arrange
            const string token = "abc123";

            var http = new MockHttpClient();
            http.SetupSendAsync();
            var service = new Mock<PlayerApi>(http.HttpClient, token) { CallBase = true };

            // act
            await service.Object.Play(token);

            // assert
            service.Verify(s => s.Put(It.IsAny<string>(), It.IsAny<object>(), token));
        }

        [TestMethod]
        public async Task GetDevices_AccessToken_PutInvokedWithAccessToken()
        {
            // arrange
            const string token = "abc123";
            string json = JsonConvert.SerializeObject(new { devices = new Device[] { new Device() } });
            var http = new MockHttpClient();
            http.SetupSendAsync(json);
            var service = new Mock<PlayerApi>(http.HttpClient, token) { CallBase = true };

            // act
            await service.Object.GetDevices<dynamic>(token);

            // assert
            service.Verify(s => s.GetModelFromProperty<dynamic>(It.IsAny<string>(), It.IsAny<string>(), token));
        }

        [TestMethod]
        public async Task GetCurrentPlaybackInfo_AccessToken_GetModelInvokedWithAccessToken()
        {
            // arrange
            const string token = "abc123";
            string json = JsonConvert.SerializeObject(new CurrentTrackPlaybackContext());
            var http = new MockHttpClient();
            http.SetupSendAsync(json);
            var service = new Mock<PlayerApi>(http.HttpClient, token) { CallBase = true };

            // act
            await service.Object.GetCurrentPlaybackInfo(token);

            // assert
            service.Verify(s => s.GetModel<CurrentPlaybackContext>(It.IsAny<string>(), token));
        }

        [TestMethod]
        [TestCategory("Integration")]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task GetCurrentPlaybackInfo_NoToken_ThrowsInvalidOperationException()
        {
            // arrange
            var http = new HttpClient();
            var player = new PlayerApi(http);

            // act
            await player.GetCurrentPlaybackInfo();
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task Seek_1ms_NoException()
        {
            // arrange
            var http = new HttpClient();
            var player = new PlayerApi(http);
            string accessToken = TestsHelper.GetLocalConfig()["SpotifyUserBearerAccessToken"];
            await player.PlayAlbum("5mAPk4qeNqVLtNydaWbWlf", accessToken);

            // act
            await player.Seek(1, accessToken: accessToken);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task Shuffle_OnOff_NoException()
        {
            // arrange
            var http = new HttpClient();
            var player = new PlayerApi(http);
            string accessToken = TestsHelper.GetLocalConfig()["SpotifyUserBearerAccessToken"];
            await player.PlayAlbum("5mAPk4qeNqVLtNydaWbWlf", accessToken);

            // act
            await player.Shuffle(true, accessToken: accessToken);
            await player.Shuffle(false, accessToken: accessToken);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task Volume_100_NoException()
        {
            // arrange
            var http = new HttpClient();
            var player = new PlayerApi(http);
            string accessToken = TestsHelper.GetLocalConfig()["SpotifyUserBearerAccessToken"];
            await player.PlayAlbum("5mAPk4qeNqVLtNydaWbWlf", accessToken);

            // act
            await player.Volume(100, accessToken: accessToken);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task Repeat_ContextOff_NoException()
        {
            // arrange
            var http = new HttpClient();
            var player = new PlayerApi(http);
            string accessToken = TestsHelper.GetLocalConfig()["SpotifyUserBearerAccessToken"];
            await player.PlayAlbum("5mAPk4qeNqVLtNydaWbWlf", accessToken);

            // act
            await player.Repeat(RepeatStates.Context, accessToken: accessToken);
            await player.Repeat(RepeatStates.Off, accessToken: accessToken);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task Pause_UserAccessToken_NoException()
        {
            // arrange
            var http = new HttpClient();
            var player = new PlayerApi(http);
            string accessToken = TestsHelper.GetLocalConfig()["SpotifyUserBearerAccessToken"];
            await player.PlayAlbum("5mAPk4qeNqVLtNydaWbWlf", accessToken);

            // act
            await player.Pause(accessToken: accessToken);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task SkipNext_UserAccessToken_NoException()
        {
            // arrange
            var http = new HttpClient();
            var player = new PlayerApi(http);
            string accessToken = TestsHelper.GetLocalConfig()["SpotifyUserBearerAccessToken"];
            await player.PlayAlbum("5mAPk4qeNqVLtNydaWbWlf", accessToken);

            // act
            await player.SkipNext(accessToken: accessToken);
        }

        [TestMethod]
        [TestCategory("Integration")]
        [TestCategory("User")]
        public async Task SkipPrevious_AfterSkipNext_NoException()
        {
            // arrange
            var http = new HttpClient();
            var player = new PlayerApi(http);
            string accessToken = TestsHelper.GetLocalConfig()["SpotifyUserBearerAccessToken"];
            await player.PlayAlbum("5mAPk4qeNqVLtNydaWbWlf", accessToken);
            await player.SkipNext(accessToken: accessToken);

            // act
            await player.SkipPrevious(accessToken: accessToken);
        }
    }

    class BearerTokenStore
    {
        public string GetToken(string user)
        {
            return "foo";
        }
    }
}