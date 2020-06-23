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
        /// Check if current user follows artists or users.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="type">The type, either artist or user, the current user follows.</param>
        /// <param name="ids">A comma-separated list of the artist or the user Spotify IDs to check. A maximum of 50 IDs can be sent in one request.</param>
        /// <returns>List<bool></returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/check-current-user-follows/
        /// </remarks>
        Task<List<bool>> GetFollowingContains(
            string username,
            FollowApi.ContainsTypes type,
            List<string> ids,
            string accessToken = null
            );

        #endregion
    }
}
