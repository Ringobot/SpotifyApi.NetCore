using System.Collections.Generic;
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
    }
}
