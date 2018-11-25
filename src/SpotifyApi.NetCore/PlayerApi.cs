using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpotifyApi.NetCore.Http;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    public class PlayerApi : SpotifyWebApi, IPlayerApi
    {
        private readonly IUserAccountsService _userAccounts;

        public PlayerApi(HttpClient httpClient, IUserAccountsService userAccountsService) : base(httpClient, userAccountsService)
        {
            _userAccounts = userAccountsService ?? throw new ArgumentNullException("userAccountsService");
        }

        public PlayerApi(HttpClient httpClient, string bearerToken) : base(httpClient, bearerToken)
        {
            //TODO: This is setting two Simple Accounts Services on this class, which is silly. This will go away when 
            // userHash overloads are removed.
            _userAccounts = new Authorization.SimpleAccountsService(bearerToken);
        }

        #region PlayTracks

        /// <summary>
        /// BETA. Play a Track on the user’s active device.
        /// </summary>
        /// <param name="spotifyTrackId">Spotify track Ids to play</param>
        /// <param name="bearerToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of a user. The access token must have the 
        /// `user-modify-playback-state` scope authorized in order to control playback. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <param name="deviceId">Optional. The id of the device this command is targeting. If not supplied, the user’s 
        /// currently active device is the target.</param>
        /// <param name="positionMs">Optional. Indicates from what position to start playback. Must be a positive number. 
        /// Passing in a position that is greater than the length of the track will cause the player to start playing the 
        /// next song.</param>
        /// <remarks> https://developer.spotify.com/documentation/web-api/reference/player/start-a-users-playback/ </remarks>
        public async Task PlayTracks(string spotifyTrackId, string bearerToken = null, string deviceId = null, 
            long positionMs = 0) => await PlayTracks(new[] { spotifyTrackId }, bearerToken, deviceId, positionMs);

        /// <summary>
        /// BETA. Play Tracks on the user’s active device.
        /// </summary>
        /// <param name="spotifyTrackIds">Array of the Spotify track Ids to play</param>
        /// <param name="bearerToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of a user. The access token must have the 
        /// `user-modify-playback-state` scope authorized in order to control playback. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <param name="deviceId">Optional. The id of the device this command is targeting. If not supplied, the user’s 
        /// currently active device is the target.</param>
        /// <param name="positionMs">Optional. Indicates from what position to start playback. Must be a positive number. 
        /// Passing in a position that is greater than the length of the track will cause the player to start playing the 
        /// next song.</param>
        /// <remarks> https://developer.spotify.com/documentation/web-api/reference/player/start-a-users-playback/ </remarks>
        public async Task PlayTracks(string[] spotifyTrackIds, string bearerToken = null, string deviceId = null, 
            long positionMs = 0) 
        {
            if (spotifyTrackIds == null || spotifyTrackIds.Length == 0) throw new ArgumentNullException(nameof(spotifyTrackIds));

            dynamic data = JObject.FromObject(new { uris = spotifyTrackIds.Select(SpotifyTrackUri).ToArray() });
            await Play(data, deviceId, positionMs);

        }

        [Obsolete("userHash overrides Will be removed in vNext.")]
        public async Task PlayTracks(string userHash, string[] spotifyTrackUris, string offsetTrackUri = null, string deviceId = null)
        {
            dynamic data = JObject.FromObject(new { uris = spotifyTrackUris });
            if (offsetTrackUri != null) data.offset = JObject.FromObject(new { uri = offsetTrackUri });

            await Play(userHash, data, deviceId);
        }

        [Obsolete("userHash overrides Will be removed in vNext.")]
        public async Task PlayTracks(string userHash, string[] spotifyTrackUris, int offsetPosition = 0, string deviceId = null)
        {
            dynamic data = JObject.FromObject(new { uris = spotifyTrackUris });
            if (offsetPosition > 0) data.offset = JObject.FromObject(new { position = offsetPosition });

            await Play(userHash, data, deviceId);
        }

        #endregion

        #region PlayAlbum

        /// <summary>
        /// BETA. Play an Album on the user’s active device.
        /// </summary>
        /// <param name="albumId">Spotify Album Id to play</param>
        /// <param name="bearerToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of a user. The access token must have the 
        /// `user-modify-playback-state` scope authorized in order to control playback. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <param name="deviceId">Optional. The id of the device this command is targeting. If not supplied, the user’s 
        /// currently active device is the target.</param>
        /// <param name="positionMs">Optional. Indicates from what position to start playback. Must be a positive number. 
        /// Passing in a position that is greater than the length of the track will cause the player to start playing the 
        /// next song.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/player/start-a-users-playback/ 
        /// </remarks>
        public async Task PlayAlbum(string albumId, string bearerToken = null, string deviceId = null, long positionMs = 0)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// BETA. Play an Album from a Track offset on the user’s active device.
        /// </summary>
        /// <param name="albumId">Spotify Album Id to play</param>
        /// <param name="offsetTrackId">Id of the Track to start at</param>
        /// <param name="bearerToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of a user. The access token must have the 
        /// `user-modify-playback-state` scope authorized in order to control playback. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <param name="deviceId">Optional. The id of the device this command is targeting. If not supplied, the user’s 
        /// currently active device is the target.</param>
        /// <param name="positionMs">Optional. Indicates from what position to start playback. Must be a positive number. 
        /// Passing in a position that is greater than the length of the track will cause the player to start playing the 
        /// next song.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/player/start-a-users-playback/ 
        /// </remarks>
        public async Task PlayAlbumOffset(string albumId, string offsetTrackId, string bearerToken = null, 
            string deviceId = null, long positionMs = 0)
        {
            dynamic data = JObject.FromObject(new { context_uri = SpotifyAlbumUri(albumId) });
            if (offsetTrackId != null) data.offset = JObject.FromObject(new { uri = SpotifyTrackUri(offsetTrackId) });
            await Play(data, deviceId);
        }

        /// <summary>
        /// BETA. Play an Album from a Track offset on the user’s active device.
        /// </summary>
        /// <param name="albumId">Spotify Album Id to play</param>
        /// <param name="offsetPosition">From where in the Album playback should start, i.e. Track number</param>
        /// <param name="bearerToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of a user. The access token must have the 
        /// `user-modify-playback-state` scope authorized in order to control playback. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <param name="deviceId">Optional. The id of the device this command is targeting. If not supplied, the user’s 
        /// currently active device is the target.</param>
        /// <param name="positionMs">Optional. Indicates from what position to start playback. Must be a positive number. 
        /// Passing in a position that is greater than the length of the track will cause the player to start playing the 
        /// next song.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/player/start-a-users-playback/ 
        /// </remarks>
        public async Task PlayAlbumOffset(string albumId, int offsetPosition, string bearerToken = null, string deviceId = null, 
            long positionMs = 0)
        {
            dynamic data = JObject.FromObject(new { context_uri = SpotifyAlbumUri(albumId) });
            if (offsetPosition > 0) data.offset = JObject.FromObject(new { position = offsetPosition });
            await Play(data, deviceId);
        }

        #endregion

        #region PlayPlaylist

        /// <summary>
        /// BETA. Play a Playlist on the user’s active device.
        /// </summary>
        /// <param name="playlistId">Spotify Playlist Id to play</param>
        /// <param name="bearerToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of a user. The access token must have the 
        /// `user-modify-playback-state` scope authorized in order to control playback. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <param name="deviceId">Optional. The id of the device this command is targeting. If not supplied, the user’s 
        /// currently active device is the target.</param>
        /// <param name="positionMs">Optional. Indicates from what position to start playback. Must be a positive number. 
        /// Passing in a position that is greater than the length of the track will cause the player to start playing the 
        /// next song.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/player/start-a-users-playback/ 
        /// </remarks>
        public async Task PlayPlaylist(string playlistId, string bearerToken = null, string deviceId = null,
            long positionMs = 0)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// BETA. Play a Playlist on the user’s active device.
        /// </summary>
        /// <param name="playlistId">Spotify Playlist Id to play</param>
        /// <param name="offsetTrackId">Id of the Track to start at</param>
        /// <param name="bearerToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of a user. The access token must have the 
        /// `user-modify-playback-state` scope authorized in order to control playback. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <param name="deviceId">Optional. The id of the device this command is targeting. If not supplied, the user’s 
        /// currently active device is the target.</param>
        /// <param name="positionMs">Optional. Indicates from what position to start playback. Must be a positive number. 
        /// Passing in a position that is greater than the length of the track will cause the player to start playing the 
        /// next song.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/player/start-a-users-playback/ 
        /// </remarks>
        public async Task PlayPlaylistOffset(string playlistId, string offsetTrackId, string bearerToken = null, 
            string deviceId = null, long positionMs = 0)
        {
            dynamic data = JObject.FromObject(new { context_uri = SpotifyPlaylistUri(playlistId) });
            if (offsetTrackId != null) data.offset = JObject.FromObject(new { uri = SpotifyTrackUri(offsetTrackId) });
            await Play(data, deviceId);
        }

        /// <summary>
        /// BETA. Play a Playlist on the user’s active device.
        /// </summary>
        /// <param name="playlistId">Spotify Playlist Id to play</param>
        /// <param name="offsetPosition">From where in the Playlist playback should start, i.e. Track number</param>
        /// <param name="bearerToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of a user. The access token must have the 
        /// `user-modify-playback-state` scope authorized in order to control playback. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <param name="deviceId">Optional. The id of the device this command is targeting. If not supplied, the user’s 
        /// currently active device is the target.</param>
        /// <param name="positionMs">Optional. Indicates from what position to start playback. Must be a positive number. 
        /// Passing in a position that is greater than the length of the track will cause the player to start playing the 
        /// next song.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/player/start-a-users-playback/ 
        /// </remarks>
        public async Task PlayPlaylistOffset(string playlistId, int offsetPosition, string bearerToken = null, 
            string deviceId = null, long positionMs = 0)
        {
            dynamic data = JObject.FromObject(new { context_uri = SpotifyPlaylistUri(playlistId) });
            if (offsetPosition > 0) data.offset = JObject.FromObject(new { position = offsetPosition});
            await Play(data, deviceId);
         }

        #endregion

        #region Play

        /// <summary>
        /// BETA. Resume playback on the user's active device.
        /// </summary>
        /// <param name="bearerToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of a user. The access token must have the 
        /// `user-modify-playback-state` scope authorized in order to control playback. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <param name="deviceId">Optional. The id of the device this command is targeting. If not supplied, the user’s 
        /// currently active device is the target.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/player/start-a-users-playback/
        /// </remarks>
        public async Task Play(string bearerToken = null, string deviceId = null)
        {
            await Play(null, deviceId, 0);
        }

        #endregion

        #region GetDevices

        /// <summary>
        /// BETA. Get information about a user’s available devices.
        /// </summary>
        /// <returns>Task of Array of <see cref="Device"/></returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/player/get-a-users-available-devices/
        /// </remarks>
        public async Task<Device[]> GetDevices() =>
            await GetModelFromProperty<Device[]>($"{BaseUrl}/me/player/devices", "devices",
            (await _userAccounts.GetUserAccessToken(null)).AccessToken);

        /// <summary>
        /// BETA. Get information about a user’s available devices.
        /// </summary>
        /// <param name="bearerToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of a user. The access token must have the 
        /// `user-read-playback-state` scope authorized in order to control playback. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of <see cref="Device"/></returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/player/get-a-users-available-devices/
        /// </remarks>
        public Task<T> GetDevices<T>(string bearerToken = null) => throw new NotImplementedException();

        [Obsolete("userHash overrides Will be removed in vNext.")]
        public async Task<Device[]> GetDevices(string userHash) =>
            await GetModelFromProperty<Device[]>($"{BaseUrl}/me/player/devices", "devices",
                (await _userAccounts.GetUserAccessToken(userHash)).AccessToken);

        #endregion

        #region GetCurrentPlaybackInfo

        /// <summary>
        /// BETA. Get information about the user’s current playback state, including track, track progress, and active device.
        /// </summary>
        /// <param name="bearerToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of a user. The access token must have the 
        /// `user-read-playback-state` scope authorized in order to control playback. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <param name="market">Optional. A <see cref="SpotifyCountryCodes" /> or the string from_token.
        /// Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns>Task of <see cref="CurrentPlaybackContext"/></returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/player/get-information-about-the-users-current-playback/
        /// </remarks>
        public Task<CurrentPlaybackContext> GetCurrentPlaybackInfo(string bearerToken = null, string market = null) 
            => throw new NotImplementedException();

        /// <summary>
        /// BETA. Get information about the user’s current playback state, including track, track progress, and active device.
        /// </summary>
        /// <param name="bearerToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of a user. The access token must have the 
        /// `user-read-playback-state` scope authorized in order to control playback. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <param name="market">Optional. A <see cref="SpotifyCountryCodes" /> or the string from_token.
        /// Provide this parameter if you want to apply Track Relinking.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/player/get-information-about-the-users-current-playback/
        /// </remarks>
        public Task<T> GetCurrentPlaybackInfo<T>(string bearerToken = null, string market = null) 
            => throw new NotImplementedException();

        #endregion

        #region PlayContext

        [Obsolete("userHash overrides Will be removed in vNext.")]
        public async Task PlayContext(string userHash, string spotifyUri, string offsetTrackUri = null, string deviceId = null)
        {
            dynamic data = JObject.FromObject(new { context_uri = spotifyUri });
            if (offsetTrackUri != null) data.offset = JObject.FromObject(new { uri = offsetTrackUri });

            await Play(userHash, data, deviceId);
        }

        [Obsolete("userHash overrides Will be removed in vNext.")]
        public async Task PlayContext(string userHash, string spotifyUri, int offsetPosition = 0, string deviceId = null)
        {
            dynamic data = JObject.FromObject(new { context_uri = spotifyUri });
            if (offsetPosition > 0) data.offset = JObject.FromObject(new { position = offsetPosition });

            await Play(userHash, data, deviceId);
        }

        [Obsolete("userHash overrides Will be removed in vNext.")]
        public async Task PlayContext(string userHash, string spotifyUri)
        {
            await PlayContext(userHash, spotifyUri, null, null);
        }

        #endregion

        public async Task Play(string userHash, object data, string deviceId = null)
        {
            // url
            string url = $"{BaseUrl}/me/player/play";
            if (deviceId != null) url += $"?device_id={deviceId}";

            await Put(url, userHash, data);
        }

        private async Task Play(dynamic data, string deviceId, long positionMs)
        {
            // url
            string url = $"{BaseUrl}/me/player/play";
            if (deviceId != null) url += $"?device_id={deviceId}";
            if (positionMs > 0) data.position_ms = positionMs;
            await Put(url, data);
        }

        /// <summary>
        /// Helper to PUT an object as JSON body
        /// </summary>
        protected internal virtual async Task<HttpResponseMessage> Put(string url, string userHash, object data)
        {
            // TODO: Could cause unusual effects if multiple threads mix client auth and user auth?
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", (await _userAccounts.GetUserAccessToken(userHash)).AccessToken);
            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _http.PutAsync(url, content);

            await RestHttpClient.CheckForErrors(response);

            return response;
        }

        /// <summary>
        /// Helper to PUT an object as JSON body
        /// </summary>
        protected internal virtual async Task<HttpResponseMessage> Put(string url, object data)
        {
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", (await _userAccounts.GetUserAccessToken(null)).AccessToken);
            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _http.PutAsync(url, content);

            await RestHttpClient.CheckForErrors(response);

            return response;
        }

        private static string SpotifyTrackUri(string trackId) => $"spotify:track:{trackId}";
        private static string SpotifyAlbumUri(string albumId) => $"spotify:album:{albumId}";
        private static string SpotifyPlaylistUri(string playlistId) => $"spotify:playlist:{playlistId}";

    }
}
