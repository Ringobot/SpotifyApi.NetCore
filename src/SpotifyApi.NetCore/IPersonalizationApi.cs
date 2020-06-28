using SpotifyApi.NetCore.Models;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    public interface IPersonalizationApi
    {
        #region GetUserTopArtistsOrTracks
        /// <summary>
        /// Get a User's Top Artists
        /// </summary>
        /// <param name="limit">Optional. The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <returns>A json string containing an artists object. The artists object in turn contains a cursor-based paging object of Artists.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/personalization/get-users-top-artists-and-tracks/
        /// </remarks>
        Task<PagedArtists> GetUserTopArtists(
            int limit = 20,
            int offset = 0,
            PersonalizationApi.TimeRange timeRange = PersonalizationApi.TimeRange.MediumTerm,
            string accessToken = null
            );

        Task<T> GetUserTopArtists<T>(
            int limit = 20,
            int offset = 0,
            PersonalizationApi.TimeRange timeRange = PersonalizationApi.TimeRange.MediumTerm,
            string accessToken = null
            );
        #endregion
    }
}
