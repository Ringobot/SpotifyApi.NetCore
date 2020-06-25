using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Defines a wrapper for the Spotify Web Playlists API.
    /// </summary>
    public interface IFollowApi
    {
        #region GetFollowingContains

        /// <summary>
        /// Check if Current User Follows Artists
        /// </summary>
        /// <param name="ids">Required. A comma-separated list of the artists Spotify IDs to check. A maximum of 50 IDs can be sent in one request.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/check-current-user-follows/
        /// </remarks>
        Task<bool[]> CheckCurrentUserFollowsArtists(
            string[] ids,
            string accessToken = null
            );

        /// <summary>
        /// Check if Current User Follows Artists
        /// </summary>
        /// <param name="ids">Required. A comma-separated list of the artists Spotify IDs to check. A maximum of 50 IDs can be sent in one request.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/check-current-user-follows/
        /// </remarks>
        Task<T> CheckCurrentUserFollowsArtists<T>(
            string[] ids,
            string accessToken = null
            );

        /// <summary>
        /// Check if Current User Follows Users
        /// </summary>
        /// <param name="ids">Required. A comma-separated list of the users Spotify IDs to check. A maximum of 50 IDs can be sent in one request.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/check-current-user-follows/
        /// </remarks>
        Task<bool[]> CheckCurrentUserFollowsUsers(
            string[] ids,
            string accessToken = null
            );

        /// <summary>
        /// Check if Current User Follows Users
        /// </summary>
        /// <param name="ids">Required. A comma-separated list of the users Spotify IDs to check. A maximum of 50 IDs can be sent in one request.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/check-current-user-follows/
        /// </remarks>
        Task<T> CheckCurrentUserFollowsUsers<T>(
            string[] ids,
            string accessToken = null
            );
        #endregion

        #region CheckCurrentUserFollowsPlaylist
        /// <summary>
        /// Check to see if one or more Spotify users are following a specified playlist.
        /// </summary>
        /// <param name="playlistId">Required. The Spotify ID of the playlist.</param>
        /// <param name="userIds">Required. A comma-separated list of Spotify User IDs ; the ids of the users that you want to check to see if they follow the playlist. Maximum: 5 ids.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/check-user-following-playlist/
        /// </remarks>
        Task<bool[]> CheckUsersFollowPlaylist(
            string playlistId,
            string[] userIds,
            string accessToken = null
            );

        /// <summary>
        /// Check to see if one or more Spotify users are following a specified playlist.
        /// </summary>
        /// <param name="playlistId">Required. The Spotify ID of the playlist.</param>
        /// <param name="userIds">Required. A comma-separated list of Spotify User IDs ; the ids of the users that you want to check to see if they follow the playlist. Maximum: 5 ids.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/check-user-following-playlist/
        /// </remarks>
        Task<T> CheckUsersFollowPlaylist<T>(
            string playlistId,
            string[] userIds,
            string accessToken = null
            );
        #endregion
    }
}
