using SpotifyApi.NetCore.Models;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Defines a wrapper for the Spotify Web Personalization API.
    /// </summary>
    public interface IPersonalizationApi
    {
        #region GetUserTopArtistsOrTracks
        /// <summary>
        /// Get a User's Top Artists
        /// </summary>
        /// <param name="limit">Optional. The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedArtists"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/personalization/get-users-top-artists-and-tracks/
        /// </remarks>
        Task<PagedArtists> GetUserTopArtists(
            int limit = 20,
            int offset = 0,
            PersonalizationApi.TimeRange timeRange = PersonalizationApi.TimeRange.MediumTerm,
            string accessToken = null
            );

        /// <summary>
        /// Get a User's Top Artists
        /// </summary>
        /// <param name="limit">Optional. The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedArtists"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/personalization/get-users-top-artists-and-tracks/
        /// </remarks>
        Task<T> GetUserTopArtists<T>(
            int limit = 20,
            int offset = 0,
            PersonalizationApi.TimeRange timeRange = PersonalizationApi.TimeRange.MediumTerm,
            string accessToken = null
            );

        /// <summary>
        /// Get a User's Top Tracks
        /// </summary>
        /// <param name="limit">Optional. The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedTracks"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/personalization/get-users-top-artists-and-tracks/
        /// </remarks>
        Task<PagedTracks> GetUserTopTracks(
            int limit = 20,
            int offset = 0,
            PersonalizationApi.TimeRange timeRange = PersonalizationApi.TimeRange.MediumTerm,
            string accessToken = null
            );

        /// <summary>
        /// Get a User's Top Tracks
        /// </summary>
        /// <param name="limit">Optional. The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedTracks"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/personalization/get-users-top-artists-and-tracks/
        /// </remarks>
        Task<T> GetUserTopTracks<T>(
            int limit = 20,
            int offset = 0,
            PersonalizationApi.TimeRange timeRange = PersonalizationApi.TimeRange.MediumTerm,
            string accessToken = null
            );
        #endregion
    }
}
