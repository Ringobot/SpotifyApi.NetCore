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
        public Task PlayTracks(
            string trackId,
            string accessToken = null,
            string deviceId = null,
            long positionMs = 0) => PlayTracks(
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
        public Task Play(string accessToken = null, string deviceId = null) 
            => Play(null, accessToken, deviceId, 0);

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
        public Task<Device[]> GetDevices(string accessToken = null) 
            => GetDevices<Device[]>(accessToken: accessToken);

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
        public Task<T> GetDevices<T>(string accessToken = null)
            => GetModelFromProperty<T>(new Uri($"{BaseUrl}/me/player/devices"), "devices", accessToken: accessToken);

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
        /// <param name="additionalTypes">Optional. A comma-separated list of item types that your client 
        /// supports besides the default track type. Valid types are: track and episode. An unsupported 
        /// type in the response is expected to be represented as null value in the item field. Note: 
        /// This parameter was introduced to allow existing clients to maintain their current behaviour 
        /// and might be deprecated in the future. In addition to providing this parameter, make sure 
        /// that your client properly handles cases of new types in the future by checking against the 
        /// currently_playing_type field.</param>
        /// <returns>Task of <see cref="CurrentPlaybackContext"/></returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/player/get-information-about-the-users-current-playback/
        /// If no devices are active API may return 204 (No Content) which will be returned as `null`. 
        /// </remarks>
        public async Task<CurrentPlaybackContext> GetCurrentPlaybackInfo(
            string accessToken = null, 
            string market = null,
            string[] additionalTypes = null)
        {
            var builder = new UriBuilder($"{BaseUrl}/me/player");
            builder.AppendToQueryIfValueNotNullOrWhiteSpace("market", market);
            builder.AppendToQueryAsCsv("additional_types", additionalTypes);
            var jObject = await GetJObject(builder.Uri, accessToken: accessToken);

            // Todo #25 return 204 no content result 
            if (jObject == null) return null;

            // Deserialize as Items of Track or Items of Episode
            if (jObject["currently_playing_type"].ToString() == "episode")
                return jObject.ToObject<CurrentEpisodePlaybackContext>();

            return jObject.ToObject<CurrentTrackPlaybackContext>();
        }

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
        /// <param name="additionalTypes">Optional. A comma-separated list of item types that your client 
        /// supports besides the default track type. Valid types are: track and episode. An unsupported 
        /// type in the response is expected to be represented as null value in the item field. Note: 
        /// This parameter was introduced to allow existing clients to maintain their current behaviour 
        /// and might be deprecated in the future. In addition to providing this parameter, make sure 
        /// that your client properly handles cases of new types in the future by checking against the 
        /// currently_playing_type field.</param>
        /// <returns>Task of T</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/player/get-information-about-the-users-current-playback/
        /// </remarks>
        public async Task<T> GetCurrentPlaybackInfo<T>(
            string accessToken = null, 
            string market = null,
            string[] additionalTypes = null)
        {
            var builder = new UriBuilder($"{BaseUrl}/me/player");
            builder.AppendToQueryIfValueNotNullOrWhiteSpace("market", market);
            builder.AppendToQueryAsCsv("additional_types", additionalTypes);
            return await GetModel<T>(builder.Uri, accessToken: accessToken);
        }

        #endregion

        private async Task Play(dynamic data, string accessToken, string deviceId, long positionMs)
        {
            // url
            var builder = new UriBuilder($"{BaseUrl}/me/player/play");
            builder.AppendToQueryIfValueNotNullOrWhiteSpace("device_id", deviceId);
            if (positionMs > 0) data.position_ms = positionMs;
            await Put(builder.Uri, data, accessToken: accessToken);
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
            var builder = new UriBuilder($"{BaseUrl}/me/player/seek?position_ms={positionMs}");
            builder.AppendToQueryIfValueNotNullOrWhiteSpace("device_id", deviceId);
            await Put(builder.Uri, null, accessToken);
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
            var builder = new UriBuilder($"{BaseUrl}/me/player/shuffle?state={(state ? "true" : "false")}");
            builder.AppendToQueryIfValueNotNullOrWhiteSpace("device_id", deviceId);
            await Put(builder.Uri, null, accessToken);
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
            var builder = new UriBuilder($"{BaseUrl}/me/player/volume?volume_percent={volumePercent}");
            builder.AppendToQueryIfValueNotNullOrWhiteSpace("device_id", deviceId);
            await Put(builder.Uri, null, accessToken);
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
            var builder = new UriBuilder($"{BaseUrl}/me/player/repeat?state={state}");
            builder.AppendToQueryIfValueNotNullOrWhiteSpace("device_id", deviceId);
            await Put(builder.Uri, null, accessToken);
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
            var builder = new UriBuilder($"{BaseUrl}/me/player/pause");
            builder.AppendToQueryIfValueNotNullOrWhiteSpace("device_id", deviceId);
            await Put(builder.Uri, null, accessToken);
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
            var builder = new UriBuilder($"{BaseUrl}/me/player/next");
            builder.AppendToQueryIfValueNotNullOrWhiteSpace("device_id", deviceId);
            await Post(builder.Uri, null, accessToken);
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
            var builder = new UriBuilder($"{BaseUrl}/me/player/previous");
            builder.AppendToQueryIfValueNotNullOrWhiteSpace("device_id", deviceId);
            await Post(builder.Uri, null, accessToken);
        }

        #endregion

        #region GetRecentlyPlayedTracks

        /// <summary>
        /// Get tracks from the current user’s recently played tracks.
        /// </summary>
        /// <param name="limit">Optional. The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="after">Optional. A Unix timestamp in milliseconds. Returns all items after 
        /// (but not including) this cursor position. If after is specified, before must not be specified.</param>
        /// <param name="before">Optional. A Unix timestamp in milliseconds. Returns all items before 
        /// (but not including) this cursor position. If before is specified, after must not be specified.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service,
        /// used for this call only. See constructors for other ways to provide an access token.</param>
        /// <returns>An array of play history objects (wrapped in a cursor-based paging object). The 
        /// play history items each contain the context the track was played from (e.g. playlist, album), 
        /// the date and time the track was played, and a <see cref="Track"/> object.</returns>
        /// <remarks> https://developer.spotify.com/documentation/web-api/reference/player/get-recently-played/ </remarks>
        public Task<PagedPlayHistory> GetRecentlyPlayedTracks(
            int? limit = null,
            string after = null,
            string before = null,
            string accessToken = null) => GetRecentlyPlayedTracks<PagedPlayHistory>(
                limit: limit,
                after: after,
                before: before,
                accessToken: accessToken);

        /// <summary>
        /// Get tracks from the current user’s recently played tracks.
        /// </summary>
        /// <param name="limit">Optional. The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="after">Optional. A Unix timestamp in milliseconds. Returns all items after 
        /// (but not including) this cursor position. If after is specified, before must not be specified.</param>
        /// <param name="before">Optional. A Unix timestamp in milliseconds. Returns all items before 
        /// (but not including) this cursor position. If before is specified, after must not be specified.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service,
        /// used for this call only. See constructors for other ways to provide an access token.</param>
        /// <returns>An array of play history objects serialized as T</returns>
        /// <remarks> https://developer.spotify.com/documentation/web-api/reference/player/get-recently-played/ </remarks>
        public async Task<T> GetRecentlyPlayedTracks<T>(
            int? limit = null,
            string after = null,
            string before = null,
            string accessToken = null)
        {
            if (limit.HasValue && (limit.Value < 1 || limit.Value > 50))
                throw new ArgumentOutOfRangeException("limit", "Limit must be a value from 1 to 50");

            var builder = new UriBuilder($"{BaseUrl}/me/player/recently-played");
            builder.AppendToQueryIfValueGreaterThan0("limit", limit);
            builder.AppendToQueryIfValueNotNullOrWhiteSpace("after", after);
            builder.AppendToQueryIfValueNotNullOrWhiteSpace("before", before);
            return await GetModel<T>(builder.Uri, accessToken);
        }

        #endregion

        #region GetCurrentlyPlayingTrack

        /// <summary>
        /// Get the object currently being played on the user’s Spotify account.
        /// </summary>
        /// <param name="market">Optional. A <see cref="SpotifyCountryCodes" /> or the string from_token.
        /// Provide this parameter if you want to apply Track Relinking.</param>
        /// <param name="additionalTypes">Optional. A comma-separated list of item types that your client 
        /// supports besides the default track type. Valid types are: track and episode. An unsupported 
        /// type in the response is expected to be represented as null value in the item field. Note: 
        /// This parameter was introduced to allow existing clients to maintain their current behaviour 
        /// and might be deprecated in the future. In addition to providing this parameter, make sure 
        /// that your client properly handles cases of new types in the future by checking against the 
        /// currently_playing_type field.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service,
        /// used for this call only. See constructors for other ways to provide an access token.</param>
        /// <returns>Information about the currently playing track or episode and its context. The information 
        /// returned is for the last known state, which means an inactive device could be returned if 
        /// it was the last one to execute playback.</returns>
        /// <remarks> https://developer.spotify.com/documentation/web-api/reference/player/get-the-users-currently-playing-track/ </remarks>
        public async Task<CurrentPlaybackContext> GetCurrentlyPlayingTrack(
            string market = null,
            string[] additionalTypes = null,
            string accessToken = null)
        {
            var builder = new UriBuilder($"{BaseUrl}/me/player/currently-playing");
            builder.AppendToQueryIfValueNotNullOrWhiteSpace("market", market);
            builder.AppendToQueryAsCsv("additional_types", additionalTypes);
            var jObject = await GetJObject(builder.Uri, accessToken: accessToken);

            // Todo #25 return 204 no content result 
            if (jObject == null) return null;

            // Deserialize as Items of Track or Items of Episode
            if (jObject["currently_playing_type"].ToString() == "episode")
                return jObject.ToObject<CurrentEpisodePlaybackContext>();

            return jObject.ToObject<CurrentTrackPlaybackContext>();
        }

        /// <summary>
        /// Get the object currently being played on the user’s Spotify account.
        /// </summary>
        /// <param name="market">Optional. A <see cref="SpotifyCountryCodes" /> or the string from_token.
        /// Provide this parameter if you want to apply Track Relinking.</param>
        /// <param name="additionalTypes">Optional. A comma-separated list of item types that your client 
        /// supports besides the default track type. Valid types are: track and episode. An unsupported 
        /// type in the response is expected to be represented as null value in the item field. Note: 
        /// This parameter was introduced to allow existing clients to maintain their current behaviour 
        /// and might be deprecated in the future. In addition to providing this parameter, make sure 
        /// that your client properly handles cases of new types in the future by checking against the 
        /// currently_playing_type field.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service,
        /// used for this call only. See constructors for other ways to provide an access token.</param>
        /// <returns>Information about the currently playing track or episode and its context serialized
        /// as T.</returns>
        /// <remarks> https://developer.spotify.com/documentation/web-api/reference/player/get-the-users-currently-playing-track/ </remarks>
        public async Task<T> GetCurrentlyPlayingTrack<T>(
                string market = null,
                string[] additionalTypes = null,
                string accessToken = null)
        {
            var builder = new UriBuilder($"{BaseUrl}/me/player/currently-playing");
            builder.AppendToQueryIfValueNotNullOrWhiteSpace("market", market);
            builder.AppendToQueryAsCsv("additional_types", additionalTypes);
            return await GetModel<T>(builder.Uri, accessToken);
        }

        #endregion

        #region TransferPlayback

        /// <summary>
        /// Transfer playback to a new device and determine if it should start playing.
        /// </summary>
        /// <param name="deviceId">ID of the device on which playback should be started/transferred.</param>
        /// <param name="play">Optional. true: ensure playback happens on new device. false or not provided: 
        /// keep the current playback state.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service,
        /// used for this call only. See constructors for other ways to provide an access token.</param>
        /// <returns></returns>
        /// <remarks> https://developer.spotify.com/documentation/web-api/reference/player/transfer-a-users-playback/ </remarks>
        public async Task TransferPlayback(string deviceId, bool? play = null, string accessToken = null)
        {
            await Put(
                new Uri($"{BaseUrl}/me/player"), 
                new { device_ids = new[] { deviceId }, play }, 
                accessToken: accessToken);
        }


        #endregion


        #region AddToQueue
        public async Task AddToQueue(string itemUri, string accessToken = null, string deviceId = null)
        {
            var builder = new UriBuilder($"{BaseUrl}/me/player/queue");
            builder.AppendToQueryIfValueNotNullOrWhiteSpace("device_id", deviceId);
            builder.AppendToQueryIfValueNotNullOrWhiteSpace("uri", itemUri);
            await Post(builder.Uri, null, accessToken);
        }

        #endregion
    }
}
