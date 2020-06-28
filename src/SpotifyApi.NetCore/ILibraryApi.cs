using SpotifyApi.NetCore.Models;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    public interface ILibraryApi
    {
        #region CheckUserSavedAlbums
        /// <summary>
        /// Check User's Saved Albums
        /// </summary>
        /// <param name="ids">Required. A comma-separated list of the Spotify IDs for the albums. Minimum: 1 ID. Maximum: 50 IDs.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/check-users-saved-albums/
        /// </remarks>
        Task<bool[]> CheckUserSavedAlbums(
            string[] ids,
            string accessToken = null
            );

        /// <summary>
        /// Check User's Saved Albums
        /// </summary>
        /// <param name="ids">Required. A comma-separated list of the Spotify IDs for the albums. Minimum: 1 ID. Maximum: 50 IDs.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/check-users-saved-albums/
        /// </remarks>
        Task<T> CheckUserSavedAlbums<T>(
            string[] ids,
            string accessToken = null
            );
        #endregion

        #region CheckUserSavedShows
        /// <summary>
        /// Check User's Saved Shows
        /// </summary>
        /// <param name="showIds">Required. A comma-separated list of the Spotify IDs for the shows. Minimum: 1 ID. Maximum: 50 IDs.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/check-users-saved-shows/
        /// </remarks>
        Task<bool[]> CheckUserSavedShows(
            string[] showIds,
            string accessToken = null
            );

        /// <summary>
        /// Check User's Saved Shows
        /// </summary>
        /// <param name="showIds">Required. A comma-separated list of the Spotify IDs for the shows. Minimum: 1 ID. Maximum: 50 IDs.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/check-users-saved-shows/
        /// </remarks>
        Task<T> CheckUserSavedShows<T>(
            string[] showIds,
            string accessToken = null
            );
        #endregion

        #region CheckUserSavedTracks
        /// <summary>
        /// Check User's Saved Tracks
        /// </summary>
        /// <param name="trackIds">Required. A comma-separated list of the Spotify IDs for the tracks. Minimum: 1 ID. Maximum: 50 IDs.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/check-users-saved-tracks/
        /// </remarks>
        Task<bool[]> CheckUserSavedTracks(
            string[] trackIds,
            string accessToken = null
            );

        /// <summary>
        /// Check User's Saved Tracks
        /// </summary>
        /// <param name="trackIds">Required. A comma-separated list of the Spotify IDs for the tracks. Minimum: 1 ID. Maximum: 50 IDs.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/check-users-saved-tracks/
        /// </remarks>
        Task<T> CheckUserSavedTracks<T>(
            string[] trackIds,
            string accessToken = null
            );
        #endregion

        #region GetUserSavedAlbums
        /// <summary>
        /// Get Current User's Saved Albums
        /// </summary>
        /// <param name="limit">Optional. The maximum number of objects to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <param name="market">	Optional. An ISO 3166-1 alpha-2 country code or the string from_token. Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedAlbums"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/get-users-saved-albums/
        /// </remarks>
        Task<PagedAlbums> GetUserSavedAlbums(
            int limit = 20,
            int offset = 0,
            string market = null,
            string accessToken = null
            );

        /// <summary>
        /// Get Current User's Saved Albums
        /// </summary>
        /// <param name="limit">Optional. The maximum number of objects to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <param name="market">	Optional. An ISO 3166-1 alpha-2 country code or the string from_token. Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedAlbums"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/get-users-saved-albums/
        /// </remarks>
        Task<T> GetUserSavedAlbums<T>(
            int limit = 20,
            int offset = 0,
            string market = null,
            string accessToken = null
            );
        #endregion

        #region GetUserSavedShows
        /// <summary>
        /// Get User's Saved Shows
        /// </summary>
        /// <param name="limit">Optional. The maximum number of objects to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedShows"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/get-users-saved-shows/
        /// </remarks>
        Task<PagedShows> GetUserSavedShows(
            int limit = 20,
            int offset = 0,
            string accessToken = null
            );

        /// <summary>
        /// Get User's Saved Shows
        /// </summary>
        /// <param name="limit">Optional. The maximum number of objects to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedShows"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/get-users-saved-shows/
        /// </remarks>
        Task<T> GetUserSavedShows<T>(
            int limit = 20,
            int offset = 0,
            string accessToken = null
            );
        #endregion

        #region GetUserSavedTracks
        /// <summary>
        /// Get User's Saved Tracks
        /// </summary>
        /// <param name="limit">Optional. The maximum number of objects to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string from_token. Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedTracks"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/get-users-saved-tracks/
        /// </remarks>
        Task<PagedTracks> GetUserSavedTracks(
            int limit = 20,
            int offset = 0,
            string market = null,
            string accessToken = null
            );

        /// <summary>
        /// Get User's Saved Tracks
        /// </summary>
        /// <param name="limit">Optional. The maximum number of objects to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string from_token. Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedTracks"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/get-users-saved-tracks/
        /// </remarks>
        Task<T> GetUserSavedTracks<T>(
            int limit = 20,
            int offset = 0,
            string market = null,
            string accessToken = null
            );
        #endregion

        #region RemoveAlbumsForCurrentUser
        /// <summary>
        /// Remove Albums for Current User
        /// </summary>
        /// <param name="albumIds">A comma-separated list of the album Spotify IDs. A maximum of 50 album IDs can be sent in one request. A minimum of 1 album id is required. </param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/remove-albums-user/
        /// </remarks>
        Task RemoveAlbumsForCurrentUser(
            string[] albumIds = null,
            string accessToken = null
            );
        #endregion

        #region RemoveUserSavedShows
        /// <summary>
        /// Remove User's Saved Shows
        /// </summary>
        /// <param name="showIds">Required. A comma-separated list of the show Spotify IDs. A maximum of 50 IDs can be sent in one request. A minimum of 1 user id is required.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code. If a country code is specified, only shows that are available in that market will be removed. If a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter. Note: If neither market or user country are provided, the content is considered unavailable for the client. Users can view the country that is associated with their account in the account settings.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/remove-shows-user/
        /// </remarks>
        Task RemoveUserSavedShows(
            string[] showIds,
            string market = null,
            string accessToken = null
            );
        #endregion

        #region RemoveUserSavedTracks
        /// <summary>
        /// Remove User's Saved Tracks
        /// </summary>
        /// <param name="trackIds">Required. A comma-separated list of the track Spotify IDs. A maximum of 50 track IDs can be sent in one request. A minimum of 1 track id is required.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/remove-tracks-user/
        /// </remarks>
        Task RemoveUserSavedTracks(
            string[] trackIds,
            string accessToken = null
            );
        #endregion

        #region SaveAlbumsForCurrentUser
        /// <summary>
        /// Save Albums for Current User
        /// </summary>
        /// <param name="albumIds">Required. A comma-separated list of the album Spotify IDs. A maximum of 50 album IDs can be sent in one request. A minimum of 1 album id is required.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/save-albums-user/
        /// </remarks>
        Task SaveAlbumsForCurrentUser(
            string[] albumIds,
            string accessToken = null
            );
        #endregion

        #region SaveShowsForCurrentUser
        /// <summary>
        /// Save Shows for Current User
        /// </summary>
        /// <param name="showIds">Required. A comma-separated list of the show Spotify IDs. A maximum of 50 show IDs can be sent in one request. A minimum of 1 show id is required.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/save-shows-user/
        /// </remarks>
        Task SaveShowsForCurrentUser(
            string[] showIds,
            string accessToken = null
            );
        #endregion

        #region SaveTracksForCurrentUser
        /// <summary>
        /// Save Shows for Current User
        /// </summary>
        /// <param name="trackIds">Required. A comma-separated list of the track Spotify IDs. A maximum of 50 track IDs can be sent in one request. A minimum of 1 track id is required.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/save-tracks-user/
        /// </remarks>
        Task SaveTracksForCurrentUser(
            string[] trackIds,
            string accessToken = null
            );
        #endregion
    }
}
