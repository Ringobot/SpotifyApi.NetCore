using System;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    public interface IPlayerApi
    {
        #region PlayContext

        [Obsolete("Will be removed in vNext. Use Play() instead.")]
        Task PlayContext(string userHash, string spotifyUri);

        [Obsolete("Will be removed in vNext. Use Play() instead.")]
        Task PlayContext(string userHash, string spotifyUri, string offsetTrackUri = null, string deviceId = null);

        [Obsolete("Will be removed in vNext. Use Play() instead.")]
        Task PlayContext(string userHash, string spotifyUri, int offsetPosition = 0, string deviceId = null);
        
        #endregion

        #region PlayTracks

        [Obsolete("userHash overrides Will be removed in vNext. Use accessToken parameter instead.")]
        Task PlayTracks(string userHash, string[] spotifyTrackUris, string offsetTrackUri = null, string deviceId = null);
        
        [Obsolete("userHash overrides Will be removed in vNext. Use accessToken parameter instead.")]
        Task PlayTracks(string userHash, string[] spotifyTrackUris, int offsetPosition = 0, string deviceId = null);

        /// <summary>
        /// BETA. Play a Track on the user’s active device.
        /// </summary>
        /// <param name="spotifyTrackId">Spotify track Ids to play</param>
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
        Task PlayTracks(string spotifyTrackId, string accessToken = null, string deviceId = null, long positionMs = 0);

        /// <summary>
        /// BETA. Play Tracks on the user’s active device.
        /// </summary>
        /// <param name="spotifyTrackIds">Array of the Spotify track Ids to play</param>
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
        Task PlayTracks(string[] spotifyTrackIds, string accessToken = null, string deviceId = null, long positionMs = 0);

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
        Task PlayAlbumOffset(string albumId, string offsetTrackId, string accessToken = null, string deviceId = null, 
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
        Task PlayAlbumOffset(string albumId, int offsetPosition, string accessToken = null, string deviceId = null, 
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
        Task PlayPlaylist(string playlistId, string accessToken = null, string deviceId = null,
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
        Task PlayPlaylistOffset(string playlistId, string offsetTrackId, string accessToken = null, string deviceId = null, 
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
        Task PlayPlaylistOffset(string playlistId, int offsetPosition, string accessToken = null, string deviceId = null, 
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

        [Obsolete("Helper method will be removed from public interface in next major version")]
        Task Play(string userHash, object data, string deviceId = null);

        #endregion

        #region GetDevices

        [Obsolete("userHash overrides Will be removed in vNext.")]
        Task<Device[]> GetDevices(string userHash);

        /// <summary>
        /// BETA. Get information about a user’s available devices.
        /// </summary>
        /// <returns>Task of Array of <see cref="Device"/></returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/player/get-a-users-available-devices/
        /// </remarks>
        Task<Device[]> GetDevices();
        //Task<Device[]> GetDevices(string accessToken = null); //TODO: In vNext

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
        /// <returns>Task of <see cref="CurrentPlaybackContext"/></returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/player/get-information-about-the-users-current-playback/
        /// </remarks>
        Task<CurrentPlaybackContext> GetCurrentPlaybackInfo(string accessToken = null, string market = null);

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
        Task<T> GetCurrentPlaybackInfo<T>(string accessToken = null, string market = null);

        #endregion

    }
}
