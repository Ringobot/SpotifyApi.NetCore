using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Player API endpoints.
    /// </summary>
    /// <remarks> https://developer.spotify.com/documentation/web-api/reference/player/ </remarks>
    public interface IPlayerApi
    {
        #region PlayTracks

        /// <summary>
        /// BETA. Play a Track on the user’s active device.
        /// </summary>
        /// <param name="trackId">Spotify track Id to play</param>
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
        Task PlayTracks(string trackId, string accessToken = null, string deviceId = null, long positionMs = 0);

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
        Task PlayTracks(string[] trackIds, string accessToken = null, string deviceId = null, long positionMs = 0);

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
        Task PlayAlbum(string albumId, string accessToken = null, string deviceId = null, long positionMs = 0);

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
        Task PlayAlbumOffset(
            string albumId,
            string offsetTrackId,
            string accessToken = null,
            string deviceId = null,
            long positionMs = 0);

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
        Task PlayAlbumOffset(
            string albumId,
            int offsetPosition,
            string accessToken = null,
            string deviceId = null,
            long positionMs = 0);

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
        Task PlayArtist(string artistId, string accessToken = null, string deviceId = null, long positionMs = 0);

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
        Task PlayArtistOffset(
            string artistId,
            int offsetPosition,
            string accessToken = null,
            string deviceId = null,
            long positionMs = 0);

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
        Task PlayPlaylist(
            string playlistId,
            string accessToken = null,
            string deviceId = null,
            long positionMs = 0);

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
        Task PlayPlaylistOffset(
            string playlistId,
            string offsetTrackId,
            string accessToken = null,
            string deviceId = null,
            long positionMs = 0);

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
        Task PlayPlaylistOffset(
            string playlistId,
            int offsetPosition,
            string accessToken = null,
            string deviceId = null,
            long positionMs = 0);

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
        Task Play(string accessToken = null, string deviceId = null);

        #endregion

        #region GetDevices

        /// <summary>
        /// BETA. Get information about a user’s available devices.
        /// </summary>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of a user. The access token must have the 
        /// `user-read-playback-state` scope authorized in order to control playback. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <returns>Task of Array of <see cref="Device"/></returns>-
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/player/get-a-users-available-devices/
        /// </remarks>
        Task<Device[]> GetDevices(string accessToken = null);

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
        Task<T> GetDevices<T>(string accessToken = null);

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
        /// </remarks>
        Task<CurrentPlaybackContext> GetCurrentPlaybackInfo(
            string accessToken = null,
            string market = null,
            string[] additionalTypes = null);

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
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/player/get-information-about-the-users-current-playback/
        /// </remarks>
        Task<T> GetCurrentPlaybackInfo<T>(
            string accessToken = null,
            string market = null,
            string[] additionalTypes = null);

        #endregion

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
        Task Seek(long positionMs, string accessToken = null, string deviceId = null);

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
        Task Shuffle(bool state, string accessToken = null, string deviceId = null);

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
        Task Volume(int volumePercent, string accessToken = null, string deviceId = null);

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
        Task Repeat(string state, string accessToken = null, string deviceId = null);

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
        Task Pause(string accessToken = null, string deviceId = null);

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
        Task SkipNext(string accessToken = null, string deviceId = null);

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
        Task SkipPrevious(string accessToken = null, string deviceId = null);

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
        Task<PagedPlayHistory> GetRecentlyPlayedTracks(
            int? limit = null,
            string after = null,
            string before = null,
            string accessToken = null);

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
        Task<T> GetRecentlyPlayedTracks<T>(
            int? limit = null,
            string after = null,
            string before = null,
            string accessToken = null);

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
        Task<CurrentPlaybackContext> GetCurrentlyPlayingTrack(
            string market = null,
            string[] additionalTypes = null,
            string accessToken = null);

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
        Task<T> GetCurrentlyPlayingTrack<T>(
            string market = null,
            string[] additionalTypes = null,
            string accessToken = null);

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
        Task TransferPlayback(string deviceId, bool? play = null, string accessToken = null);

        #endregion

        #region AddToQueue

        /// <summary>
        /// BETA. Add an item to the end of the user’s current playback queue.
        /// </summary>
        /// <param name="itemUri">Required. The uri of the item to add to the queue. Must be a track or an episode uri.</param>
        /// <param name="deviceId">Optional. The id of the device this command is targeting. If not supplied, the user’s 
        /// currently active device is the target.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of a user. The access token must have the 
        /// `user-modify-playback-state` scope authorized in order to control playback. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <returns>A completed request will return no response, and then issue the command to the player. 
        /// Due to the asynchronous nature of the issuance of the command, you should use the <see cref="GetCurrentPlaybackInfo(string, string, string[])"/> 
        /// endpoint to check that your issued command was handled correctly by the player.
        /// 
        /// When performing an action that is restricted, 404 NOT FOUND or 403 FORBIDDEN will be returned 
        /// together with a player error message. For example, if there are no active devices found, 
        /// the request will return 404 NOT FOUND response code and the reason NO_ACTIVE_DEVICE, or, 
        /// if the user making the request is non-premium, a 403 FORBIDDEN response code will be returned 
        /// together with the PREMIUM_REQUIRED reason.
        /// </returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/player/add-to-queue/
        /// </remarks>
        Task AddToQueue(string itemUri, string deviceId = null, string accessToken = null);

        #endregion
    }
}
