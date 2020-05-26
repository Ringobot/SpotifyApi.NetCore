using Newtonsoft.Json.Linq;
using SpotifyApi.NetCore.Authorization;
using SpotifyApi.NetCore.Helpers;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Player API endpoints.
    /// </summary>
    /// <remarks> https://developer.spotify.com/documentation/web-api/reference/player/ </remarks>
    public class PlayerApi : SpotifyWebApi, IPlayerApi
    {
        #region Constructors

        /// <summary>
        /// Use this constructor when an accessToken will be provided using the `accessToken` parameter 
        /// on each method
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/></param>
        public PlayerApi(HttpClient httpClient) : base(httpClient) { }

        /// <summary>
        /// This constructor accepts a Spotify access token that will be used for all calls to the API 
        /// (except when an accessToken is provided using the optional `accessToken` parameter on each method).
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/></param>
        /// <param name="accessToken">A valid access token from the Spotify Accounts service</param>
        public PlayerApi(HttpClient httpClient, string accessToken) : base(httpClient, accessToken) { }

        public PlayerApi(HttpClient httpClient, IAccessTokenProvider accessTokenProvider)
            : base(httpClient, accessTokenProvider) { }

        #endregion

        #region PlayTracks

        /// <summary>
        /// BETA. Play a Track on the user’s active device.
        /// </summary>
        /// <param name="trackId">Spotify track Ids to play</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of a user. The access token must have the 
        /// `user-modify-playback-state` scope authorized in order to control playback. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <param name="deviceId">Optional. The id of the device this command is targeting. If not supplied, the user’s 
        /// currently active device is the target.</param>
        /// <param name="positionMs">Optional. Indicates from what position to start playback. Must be a positive number. 
        /// Passing in a position that is greater than the length of the track will cause the player to start playing the 
        /// next song.</param>
        /// <remarks> https://developer.spotify.com/documentation/web-api/reference/player/start-a-users-playback/ </remarks>
        public async Task PlayTracks(
            string trackId,
            string accessToken = null,
            string deviceId = null,
            long positionMs = 0) => await PlayTracks(
                new[] { trackId },
                accessToken: accessToken,
                deviceId: deviceId,
                positionMs: positionMs);

        /// <summary>
        /// BETA. Play Tracks on the user’s active device.
        /// </summary>
        /// <param name="trackIds">Array of the Spotify track Ids to play</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of a user. The access token must have the 
        /// `user-modify-playback-state` scope authorized in order to control playback. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <param name="deviceId">Optional. The id of the device this command is targeting. If not supplied, the user’s 
        /// currently active device is the target.</param>
        /// <param name="positionMs">Optional. Indicates from what position to start playback. Must be a positive number. 
        /// Passing in a position that is greater than the length of the track will cause the player to start playing the 
        /// next song.</param>
        /// <remarks> https://developer.spotify.com/documentation/web-api/reference/player/start-a-users-playback/ </remarks>
        public async Task PlayTracks(
            string[] trackIds,
            string accessToken = null,
            string deviceId = null,
            long positionMs = 0)
        {
            if (trackIds == null || trackIds.Length == 0) throw new ArgumentNullException(nameof(trackIds));
            dynamic data = JObject.FromObject(new { uris = trackIds.Select(SpotifyUriHelper.TrackUri).ToArray() });
            await Play(data, accessToken, deviceId, positionMs);
        }

        #endregion

        #region PlayAlbum

        /// <summary>
        /// BETA. Play an Album on the user’s active device.
        /// </summary>
        /// <param name="albumId">Spotify Album Id to play</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service. 
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
        public async Task PlayAlbum(string albumId, string accessToken = null, string deviceId = null, long positionMs = 0)
        {
            if (string.IsNullOrEmpty(albumId)) throw new ArgumentNullException(nameof(albumId));
            dynamic data = JObject.FromObject(new { context_uri = SpotifyUriHelper.AlbumUri(albumId) });
            await Play(data, accessToken, deviceId, positionMs);
        }

        /// <summary>
        /// BETA. Play an Album from a Track offset on the user’s active device.
        /// </summary>
        /// <param name="albumId">Spotify Album Id to play</param>
        /// <param name="offsetTrackId">Id of the Track to start at</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service. 
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
        public async Task PlayAlbumOffset(
            string albumId,
            string offsetTrackId,
            string accessToken = null,
            string deviceId = null,
            long positionMs = 0)
        {
            dynamic data = JObject.FromObject(new { context_uri = SpotifyUriHelper.AlbumUri(albumId) });
            if (offsetTrackId != null) data.offset = JObject.FromObject(new { uri = SpotifyUriHelper.TrackUri(offsetTrackId) });
            await Play(data, accessToken, deviceId, positionMs);
        }

        /// <summary>
        /// BETA. Play an Album from a Track offset on the user’s active device.
        /// </summary>
        /// <param name="albumId">Spotify Album Id to play</param>
        /// <param name="offsetPosition">From where in the Album playback should start, i.e. Track number</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service. 
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
        public async Task PlayAlbumOffset(
            string albumId,
            int offsetPosition,
            string accessToken = null,
            string deviceId = null,
            long positionMs = 0)
        {
            dynamic data = JObject.FromObject(new { context_uri = SpotifyUriHelper.AlbumUri(albumId) });
            if (offsetPosition > 0) data.offset = JObject.FromObject(new { position = offsetPosition });
            await Play(data, accessToken, deviceId, positionMs);
        }

        #endregion

        #region PlayArtist

        /// <summary>
        /// BETA. Play an Artist on the user’s active device.
        /// </summary>
        /// <param name="artistId">Spotify Album Id to play</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service. 
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
        public async Task PlayArtist(string artistId, string accessToken = null, string deviceId = null, long positionMs = 0)
        {
            if (string.IsNullOrEmpty(artistId)) throw new ArgumentNullException(nameof(artistId));
            dynamic data = JObject.FromObject(new { context_uri = SpotifyUriHelper.ArtistUri(artistId) });
            await Play(data, accessToken, deviceId, positionMs);
        }

        /// <summary>
        /// BETA. Play an Artist from a Track offset on the user’s active device.
        /// </summary>
        /// <param name="artistId">Spotify Artust Id to play</param>
        /// <param name="offsetPosition">From where in the Artist top track list playback should start</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service. 
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
        public async Task PlayArtistOffset(
            string artistId,
            int offsetPosition,
            string accessToken = null,
            string deviceId = null,
            long positionMs = 0)
        {
            dynamic data = JObject.FromObject(new { context_uri = SpotifyUriHelper.ArtistUri(artistId) });
            if (offsetPosition > 0) data.offset = JObject.FromObject(new { position = offsetPosition });
            await Play(data, accessToken, deviceId, positionMs);
        }

        #endregion

        #region PlayPlaylist

        /// <summary>
        /// BETA. Play a Playlist on the user’s active device.
        /// </summary>
        /// <param name="playlistId">Spotify Playlist Id to play</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service. 
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
        public async Task PlayPlaylist(
            string playlistId,
            string accessToken = null,
            string deviceId = null,
            long positionMs = 0)
        {
            if (string.IsNullOrEmpty(playlistId)) throw new ArgumentNullException(nameof(playlistId));
            dynamic data = JObject.FromObject(new { context_uri = SpotifyUriHelper.PlaylistUri(playlistId) });
            await Play(data, accessToken, deviceId, positionMs);
        }


        /// <summary>
        /// BETA. Play a Playlist on the user’s active device.
        /// </summary>
        /// <param name="playlistId">Spotify Playlist Id to play</param>
        /// <param name="offsetTrackId">Id of the Track to start at</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service. 
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
        public async Task PlayPlaylistOffset(
            string playlistId,
            string offsetTrackId,
            string accessToken = null,
            string deviceId = null,
            long positionMs = 0)
        {
            dynamic data = JObject.FromObject(new { context_uri = SpotifyUriHelper.PlaylistUri(playlistId) });
            if (offsetTrackId != null) data.offset = JObject.FromObject(new { uri = SpotifyUriHelper.TrackUri(offsetTrackId) });
            await Play(data, accessToken, deviceId, positionMs);
        }

        /// <summary>
        /// BETA. Play a Playlist on the user’s active device.
        /// </summary>
        /// <param name="playlistId">Spotify Playlist Id to play</param>
        /// <param name="offsetPosition">From where in the Playlist playback should start, i.e. Track number</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service. 
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
        public async Task PlayPlaylistOffset(
            string playlistId,
            int offsetPosition,
            string accessToken = null,
            string deviceId = null,
            long positionMs = 0)
        {
            dynamic data = JObject.FromObject(new { context_uri = SpotifyUriHelper.PlaylistUri(playlistId) });
            if (offsetPosition > 0) data.offset = JObject.FromObject(new { position = offsetPosition });
            await Play(data, accessToken, deviceId, positionMs);
        }

        #endregion

        #region Play

        /// <summary>
        /// BETA. Resume playback on the user's active device.
        /// </summary>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of a user. The access token must have the 
        /// `user-modify-playback-state` scope authorized in order to control playback. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <param name="deviceId">Optional. The id of the device this command is targeting. If not supplied, the user’s 
        /// currently active device is the target.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/player/start-a-users-playback/
        /// </remarks>
        public async Task Play(string accessToken = null, string deviceId = null)
        {
            await Play(null, accessToken, deviceId, 0);
        }

        #endregion

        #region GetDevices

        /// <summary>
        /// BETA. Get information about a user’s available devices.
        /// </summary>
        /// <returns>Task of Array of <see cref="Device"/></returns>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of a user. The access token must have the 
        /// `user-read-playback-state` scope authorized in order to control playback. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/player/get-a-users-available-devices/
        /// </remarks>
        public async Task<Device[]> GetDevices(string accessToken = null) => await GetDevices<Device[]>(accessToken: accessToken);

        /// <summary>
        /// BETA. Get information about a user’s available devices.
        /// </summary>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of a user. The access token must have the 
        /// `user-read-playback-state` scope authorized in order to control playback. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of <see cref="T"/></returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/player/get-a-users-available-devices/
        /// </remarks>
        public async Task<T> GetDevices<T>(string accessToken = null)
            => await GetModelFromProperty<T>($"{BaseUrl}/me/player/devices", "devices", accessToken);

        #endregion

        #region GetCurrentPlaybackInfo

        /// <summary>
        /// BETA. Get information about the user’s current playback state, including track, track progress, and active device.
        /// </summary>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of a user. The access token must have the 
        /// `user-read-playback-state` scope authorized in order to control playback. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <param name="market">Optional. A <see cref="SpotifyCountryCodes" /> or the string from_token.
        /// Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns>Task of <see cref="CurrentPlaybackContext"/></returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/player/get-information-about-the-users-current-playback/
        /// If no devices are active API may return 204 (No Content) which will be returned as `null`. 
        /// </remarks>
        public Task<CurrentPlaybackContext> GetCurrentPlaybackInfo(string accessToken = null, string market = null)
            => GetCurrentPlaybackInfo<CurrentPlaybackContext>(accessToken, market);

        /// <summary>
        /// BETA. Get information about the user’s current playback state, including track, track progress, and active device.
        /// </summary>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service. 
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
        public async Task<T> GetCurrentPlaybackInfo<T>(string accessToken = null, string market = null)
        {
            string url = $"{BaseUrl}/me/player";
            if (!string.IsNullOrEmpty(market)) url += $"?market={market}";
            return await GetModel<T>(url, accessToken);
        }

        #endregion

        private async Task Play(dynamic data, string accessToken, string deviceId, long positionMs)
        {
            // url
            string url = $"{BaseUrl}/me/player/play";
            if (deviceId != null) url += $"?device_id={deviceId}";
            if (positionMs > 0) data.position_ms = positionMs;
            await Put(url, data, accessToken);
        }

        #region Seek

        /// <summary>
        /// BETA. Seeks to the given position in the user’s currently playing track.
        /// </summary>
        /// <param name="positionMs">Required. The position in milliseconds to seek to. Must be a positive
        /// number. Passing in a position that is greater than the length of the track will cause the
        /// player to start playing the next song.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of a user. The access token must have the 
        /// `user-modify-playback-state` scope authorized in order to control playback. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <param name="deviceId">Optional. The id of the device this command is targeting. If not supplied, the user’s 
        /// currently active device is the target.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/player/seek-to-position-in-currently-playing-track/
        /// </remarks>
        public async Task Seek(long positionMs, string accessToken = null, string deviceId = null)
        {
            string url = $"{BaseUrl}/me/player/seek?position_ms={positionMs}";
            if (deviceId != null) url += $"&device_id={deviceId}";
            await Put(url, null, accessToken);
        }

        #endregion

        #region Shuffle

        /// <summary>
        /// BETA. Toggle shuffle on or off for user’s playback.
        /// </summary>
        /// <param name="state">Required. true : Shuffle user’s playback. false : Do not shuffle user’s playback.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of a user. The access token must have the 
        /// `user-modify-playback-state` scope authorized in order to control playback. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <param name="deviceId">Optional. The id of the device this command is targeting. If not supplied, the user’s 
        /// currently active device is the target.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/player/toggle-shuffle-for-users-playback/
        /// </remarks>
        public async Task Shuffle(bool state, string accessToken = null, string deviceId = null)
        {
            string url = $"{BaseUrl}/me/player/shuffle?state={(state ? "true" : "false")}";
            if (deviceId != null) url += $"&device_id={deviceId}";
            await Put(url, null, accessToken);
        }

        #endregion

        #region Volume

        /// <summary>
        /// BETA. Set the volume for the user’s current playback device.
        /// </summary>
        /// <param name="volumePercent">Required. Integer. The volume to set. Must be a value from 0 to 100 inclusive.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of a user. The access token must have the 
        /// `user-modify-playback-state` scope authorized in order to control playback. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <param name="deviceId">Optional. The id of the device this command is targeting. If not supplied, the user’s 
        /// currently active device is the target.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/player/set-volume-for-users-playback/
        /// </remarks>
        public async Task Volume(int volumePercent, string accessToken = null, string deviceId = null)
        {
            if (volumePercent < 0 || volumePercent > 100)
                throw new ArgumentOutOfRangeException(nameof(volumePercent), "Must be a value from 0 to 100 inclusive.");
            string url = $"{BaseUrl}/me/player/volume?volume_percent={volumePercent}";
            if (deviceId != null) url += $"&device_id={deviceId}";
            await Put(url, null, accessToken);
        }

        #endregion

        #region Repeat

        /// <summary>
        /// BETA. Set the repeat mode for the user’s playback. Options are repeat-track, repeat-context, and off.
        /// </summary>
        /// <param name="state">Required. <see cref="RepeatStates.Track"/>, <see cref="RepeatStates.Context"/> 
        /// or <see cref="RepeatStates.Off"/>. Track will repeat the current track. Context will 
        /// repeat the current context. Off will turn repeat off.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of a user. The access token must have the 
        /// `user-modify-playback-state` scope authorized in order to control playback. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <param name="deviceId">Optional. The id of the device this command is targeting. If not supplied, the user’s 
        /// currently active device is the target.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/player/set-repeat-mode-on-users-playback/
        /// </remarks>
        public async Task Repeat(string state, string accessToken = null, string deviceId = null)
        {
            string url = $"{BaseUrl}/me/player/repeat?state={state}";
            if (deviceId != null) url += $"&device_id={deviceId}";
            await Put(url, null, accessToken);
        }

        #endregion

        #region Pause

        /// <summary>
        /// BETA. Pause playback on the user’s account.
        /// </summary>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of a user. The access token must have the 
        /// `user-modify-playback-state` scope authorized in order to control playback. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <param name="deviceId">Optional. The id of the device this command is targeting. If not supplied, the user’s 
        /// currently active device is the target.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/player/pause-a-users-playback/
        /// </remarks>
        public async Task Pause(string accessToken = null, string deviceId = null)
        {
            string url = $"{BaseUrl}/me/player/pause";
            if (deviceId != null) url += $"&device_id={deviceId}";
            await Put(url, null, accessToken);
        }

        #endregion

        #region SkipNext

        /// <summary>
        /// BETA. Skips to next track in the user’s queue.
        /// </summary>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of a user. The access token must have the 
        /// `user-modify-playback-state` scope authorized in order to control playback. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <param name="deviceId">Optional. The id of the device this command is targeting. If not supplied, the user’s 
        /// currently active device is the target.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/player/skip-users-playback-to-next-track/
        /// </remarks>
        public async Task SkipNext(string accessToken = null, string deviceId = null)
        {
            string url = $"{BaseUrl}/me/player/next";
            if (deviceId != null) url += $"&device_id={deviceId}";
            await Post(url, null, accessToken);
        }

        #endregion

        #region SkipPrevious

        /// <summary>
        /// BETA. Skips to previous track in the user’s queue.
        /// </summary>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of a user. The access token must have the 
        /// `user-modify-playback-state` scope authorized in order to control playback. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <param name="deviceId">Optional. The id of the device this command is targeting. If not supplied, the user’s 
        /// currently active device is the target.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/player/skip-users-playback-to-previous-track/
        /// Note that this will ALWAYS skip to the previous track, regardless of the current track’s progress. 
        /// Returning to the start of the current track should be performed using the 
        /// <see cref="Seek(long, string, string)"/> endpoint.
        /// </remarks>
        public async Task SkipPrevious(string accessToken = null, string deviceId = null)
        {
            string url = $"{BaseUrl}/me/player/previous";
            if (deviceId != null) url += $"&device_id={deviceId}";
            await Post(url, null, accessToken);
        }

        #endregion

    }
}
