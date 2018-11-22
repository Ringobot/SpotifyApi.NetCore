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

        [Obsolete("userHash overrides Will be removed in vNext.")]
        Task PlayTracks(string userHash, string[] spotifyTrackUris, string offsetTrackUri = null, string deviceId = null);
        
        [Obsolete("userHash overrides Will be removed in vNext.")]
        Task PlayTracks(string userHash, string[] spotifyTrackUris, int offsetPosition = 0, string deviceId = null);

        /// <summary>
        /// BETA. Play a Track on the user’s active device.
        /// https://developer.spotify.com/documentation/web-api/reference/player/start-a-users-playback/
        /// </summary>
        /// <param name="spotifyTrackId">Spotify track Ids to play</param>
        /// <param name="deviceId">Optional. The id of the device this command is targeting. If not supplied, the user’s 
        /// currently active device is the target.</param>
        /// <param name="positionMs">Optional. Indicates from what position to start playback. Must be a positive number. 
        /// Passing in a position that is greater than the length of the track will cause the player to start playing the 
        /// next song.</param>
        /// <remarks>The access token must have the user-modify-playback-state scope authorized in order to control playback.</remarks>
        Task PlayTracks(string spotifyTrackId, string deviceId = null, int positionMs = 0);

        /// <summary>
        /// BETA. Play Tracks on the user’s active device.
        /// https://developer.spotify.com/documentation/web-api/reference/player/start-a-users-playback/
        /// </summary>
        /// <param name="spotifyTrackIds">Array of the Spotify track Ids to play</param>
        /// <param name="deviceId">Optional. The id of the device this command is targeting. If not supplied, the user’s 
        /// currently active device is the target.</param>
        /// <param name="positionMs">Optional. Indicates from what position to start playback. Must be a positive number. 
        /// Passing in a position that is greater than the length of the track will cause the player to start playing the 
        /// next song.</param>
        /// <remarks>The access token must have the user-modify-playback-state scope authorized in order to control playback.</remarks>
        Task PlayTracks(string[] spotifyTrackIds, string deviceId = null, int positionMs = 0);

        #endregion

        #region PlayAlbum

        /// <summary>
        /// BETA. Play an Album on the user’s active device.
        /// https://developer.spotify.com/documentation/web-api/reference/player/start-a-users-playback/
        /// </summary>
        /// <param name="albumId">Spotify Album Id to play</param>
        /// <param name="offsetTrackId">Optional. Id of the Track to start at</param>
        /// <param name="deviceId">Optional. The id of the device this command is targeting. If not supplied, the user’s 
        /// currently active device is the target.</param>
        /// <param name="positionMs">Optional. Indicates from what position to start playback. Must be a positive number. 
        /// Passing in a position that is greater than the length of the track will cause the player to start playing the 
        /// next song.</param>
        /// <remarks>The access token must have the user-modify-playback-state scope authorized in order to control playback.</remarks>
        Task PlayAlbum(string albumId, string offsetTrackId = null, string deviceId = null, int positionMs = 0);

        /// <summary>
        /// BETA. Play an Album on the user’s active device.
        /// https://developer.spotify.com/documentation/web-api/reference/player/start-a-users-playback/
        /// </summary>
        /// <param name="albumId">Spotify Album Id to play</param>
        /// <param name="offsetPosition">Optional. From where in the Album playback should start, i.e. Track number</param>
        /// <param name="deviceId">Optional. The id of the device this command is targeting. If not supplied, the user’s 
        /// currently active device is the target.</param>
        /// <param name="positionMs">Optional. Indicates from what position to start playback. Must be a positive number. 
        /// Passing in a position that is greater than the length of the track will cause the player to start playing the 
        /// next song.</param>
        /// <remarks>The access token must have the user-modify-playback-state scope authorized in order to control playback.</remarks>
        Task PlayAlbum(string albumId, int offsetPosition = 0, string deviceId = null, int positionMs = 0);

        #endregion

        #region PlayPlaylist

        /// <summary>
        /// BETA. Play a Playlist on the user’s active device.
        /// https://developer.spotify.com/documentation/web-api/reference/player/start-a-users-playback/
        /// </summary>
        /// <param name="playlistId">Spotify Playlist Id to play</param>
        /// <param name="offsetTrackId">Optional. Id of the Track to start at</param>
        /// <param name="deviceId">Optional. The id of the device this command is targeting. If not supplied, the user’s 
        /// currently active device is the target.</param>
        /// <param name="positionMs">Optional. Indicates from what position to start playback. Must be a positive number. 
        /// Passing in a position that is greater than the length of the track will cause the player to start playing the 
        /// next song.</param>
        /// <remarks>The access token must have the user-modify-playback-state scope authorized in order to control playback.</remarks>
        Task PlayPlaylist(string playlistId, string offsetTrackId = null, string deviceId = null, int positionMs = 0);

        /// <summary>
        /// BETA. Play a Playlist on the user’s active device.
        /// https://developer.spotify.com/documentation/web-api/reference/player/start-a-users-playback/
        /// </summary>
        /// <param name="playlistId">Spotify Playlist Id to play</param>
        /// <param name="offsetPosition">Optional. From where in the Playlist playback should start, i.e. Track number</param>
        /// <param name="deviceId">Optional. The id of the device this command is targeting. If not supplied, the user’s 
        /// currently active device is the target.</param>
        /// <param name="positionMs">Optional. Indicates from what position to start playback. Must be a positive number. 
        /// Passing in a position that is greater than the length of the track will cause the player to start playing the 
        /// next song.</param>
        /// <remarks>The access token must have the user-modify-playback-state scope authorized in order to control playback.</remarks>
        Task PlayPlaylist(string playlistId, int offsetPosition = 0, string deviceId = null, int positionMs = 0);

        #endregion

        #region Play

        /// <summary>
        /// BETA. Resume playback on the user's active device.
        /// https://developer.spotify.com/documentation/web-api/reference/player/start-a-users-playback/
        /// </summary>
        /// <param name="deviceId">Optional. The id of the device this command is targeting. If not supplied, the user’s 
        /// currently active device is the target.</param>
        /// <remarks>The access token must have the user-modify-playback-state scope authorized in order to control playback.</remarks>
        Task Play(string deviceId = null);

        [Obsolete("Helper method will be removed from public interface in next major version")]
        Task Play(string userHash, object data, string deviceId = null);

        #endregion

        #region GetDevices

        [Obsolete("userHash overrides Will be removed in vNext.")]
        Task<Device[]> GetDevices(string userHash);

        /// <summary>
        /// BETA. Get information about a user’s available devices.
        /// https://developer.spotify.com/documentation/web-api/reference/player/get-a-users-available-devices/
        /// </summary>
        /// <returns>Array of <see cref="Device"/></returns>
        /// <remarks>
        /// The access token must have the `user-read-playback-state` scope authorized in order
        /// to read information.
        /// </remarks>
        Task<Device[]> GetDevices();

        #endregion

        #region GetCurrentPlaybackInfo

        /// <summary>
        /// BETA. Get information about the user’s current playback state, including track, track progress, and active device.
        /// https://developer.spotify.com/documentation/web-api/reference/player/get-information-about-the-users-current-playback/
        /// </summary>
        /// <param name="market">Optional. A <see cref="SpotifyCountryCodes" /> or the string from_token.
        /// Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns>Task of <see cref="CurrentPlaybackContext"/></returns>
        /// <remarks>
        /// The access token must have the `user-read-playback-state` scope authorized in order
        /// to read information.
        /// </remarks>
        Task<CurrentPlaybackContext> GetCurrentPlaybackInfo(string market = null);

        /// <summary>
        /// BETA. Get information about the user’s current playback state, including track, track progress, and active device.
        /// https://developer.spotify.com/documentation/web-api/reference/player/get-information-about-the-users-current-playback/
        /// </summary>
        /// <param name="market">Optional. A <see cref="SpotifyCountryCodes" /> or the string from_token.
        /// Provide this parameter if you want to apply Track Relinking.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T</returns>
        /// <remarks>
        /// The access token must have the `user-read-playback-state` scope authorized in order to read information
        /// </remarks>
        Task<T> GetCurrentPlaybackInfo<T>(string market = null);

        #endregion

    }
}
