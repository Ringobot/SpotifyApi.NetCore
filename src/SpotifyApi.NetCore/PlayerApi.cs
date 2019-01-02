using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpotifyApi.NetCore.Http;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SpotifyApi.NetCore.Helpers;

namespace SpotifyApi.NetCore
{
    public class PlayerApi : SpotifyWebApi, IPlayerApi
    {
        public PlayerApi(HttpClient httpClient) : base(httpClient) { }

        public PlayerApi(HttpClient httpClient, string accessToken) : base(httpClient, accessToken) { }

        /*
        // Add this ctor when obsolete UserAccountService overload is removed
        public PlayerApi(HttpClient httpClient, IAccessTokenProvider accessTokenProvider) 
            : base(httpClient, accessTokenProvider) { }
        */

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
        public async Task PlayTracks(string trackId, string accessToken = null, string deviceId = null, 
            long positionMs = 0) => await PlayTracks(new[] { trackId }, accessToken, deviceId, positionMs);

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
        public async Task PlayTracks(string[] trackIds, string accessToken = null, string deviceId = null, 
            long positionMs = 0) 
        {
            if (trackIds == null || trackIds.Length == 0) throw new ArgumentNullException(nameof(trackIds));
            dynamic data = JObject.FromObject(new { uris = trackIds.Select(SpotifyUriHelper.TrackUri).ToArray() });
            await Play(data, accessToken, deviceId, positionMs);
        }

        [Obsolete("userHash overrides Will be removed in vNext.")]
        public async Task PlayTracks(string userHash, string[] trackIds, string offsetTrackUri = null, 
            string deviceId = null)
        {
            dynamic data = JObject.FromObject(new { uris = trackIds });
            if (offsetTrackUri != null) data.offset = JObject.FromObject(new { uri = offsetTrackUri });
            await Play(userHash, data, deviceId);
        }

        [Obsolete("userHash overrides Will be removed in vNext.")]
        public async Task PlayTracks(string userHash, string[] trackIds, int offsetPosition = 0, string deviceId = null)
        {
            dynamic data = JObject.FromObject(new { uris = trackIds });
            if (offsetPosition > 0) data.offset = JObject.FromObject(new { position = offsetPosition });
            await Play(userHash, data, deviceId);
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
        public async Task PlayAlbumOffset(string albumId, string offsetTrackId, string accessToken = null, 
            string deviceId = null, long positionMs = 0)
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
        public async Task PlayAlbumOffset(string albumId, int offsetPosition, string accessToken = null, 
            string deviceId = null, long positionMs = 0)
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
        public async Task PlayArtistOffset(string artistId, int offsetPosition, string accessToken = null, string deviceId = null,
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
        public async Task PlayPlaylist(string playlistId, string accessToken = null, string deviceId = null,
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
        public async Task PlayPlaylistOffset(string playlistId, string offsetTrackId, string accessToken = null, 
            string deviceId = null, long positionMs = 0)
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
        public async Task PlayPlaylistOffset(string playlistId, int offsetPosition, string accessToken = null, 
            string deviceId = null, long positionMs = 0)
        {
            dynamic data = JObject.FromObject(new { context_uri = SpotifyUriHelper.PlaylistUri(playlistId) });
            if (offsetPosition > 0) data.offset = JObject.FromObject(new { position = offsetPosition});
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
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/player/get-a-users-available-devices/
        /// </remarks>
        public async Task<Device[]> GetDevices() => await GetDevices<Device[]>();

        /// <summary>
        /// BETA. Get information about a user’s available devices.
        /// </summary>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of a user. The access token must have the 
        /// `user-read-playback-state` scope authorized in order to control playback. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of <see cref="Device"/></returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/player/get-a-users-available-devices/
        /// </remarks>
        public async Task<T> GetDevices<T>(string accessToken = null) 
            => await GetModelFromProperty<T>($"{BaseUrl}/me/player/devices", "devices", accessToken);

        [Obsolete("userHash overrides Will be removed in vNext.")]
        public async Task<Device[]> GetDevices(string userHash) =>
            await GetModelFromProperty<Device[]>($"{BaseUrl}/me/player/devices", "devices",
                (await _userAccounts.GetUserAccessToken(userHash)).AccessToken);

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

            await PutWithUsersToken(url, userHash, data);
        }

        private async Task Play(dynamic data, string accessToken, string deviceId, long positionMs)
        {
            // url
            string url = $"{BaseUrl}/me/player/play";
            if (deviceId != null) url += $"?device_id={deviceId}";
            if (positionMs > 0) data.position_ms = positionMs;
            await Put(url, data, accessToken);
        }

        #region Obsolete

        [Obsolete("Will be removed in vNext")]
        private readonly IUserAccountsService _userAccounts;

        [Obsolete("Will be removed in vNext")]
        public PlayerApi(HttpClient httpClient, IUserAccountsService userAccountsService)
            : base(httpClient, userAccountsService)
        {
            _userAccounts = userAccountsService ?? throw new ArgumentNullException("userAccountsService");
        }

        /// <summary>
        /// Helper to PUT an object as JSON body
        /// </summary>
        [Obsolete("Will be removed in vNext")]
        protected internal virtual async Task<HttpResponseMessage> PutWithUsersToken(string url, string userHash, object data)
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


        #endregion

    }
}
