using SpotifyApi.NetCore.Authorization;
using SpotifyApi.NetCore.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Endpoints for retrieving information about the top artists or tracks of the current user from the Spotify catalog.
    /// </summary>
    public class PersonalizationApi : SpotifyWebApi, IPersonalizationApi
    {
        #region constructors
        public PersonalizationApi(HttpClient httpClient, IAccessTokenProvider accessTokenProvider) : base(httpClient, accessTokenProvider)
        {
        }

        public PersonalizationApi(HttpClient httpClient, string accessToken) : base(httpClient, accessToken)
        {
        }

        public PersonalizationApi(HttpClient httpClient) : base(httpClient)
        {
        }
        #endregion

        public enum TimeRange
        {
            LongTerm,
            MediumTerm,
            ShortTerm
        }

        #region GetUsersTopArtistsOrTracks
        /// <summary>
        /// Get the current user’s top artists based on calculated affinity.
        /// </summary>
        /// <param name="limit">Optional. The maximum number of entities to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedArtists"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/personalization/get-users-top-artists-and-tracks/
        /// </remarks>
        public Task<PagedArtists> GetUsersTopArtists(
            int limit = 20,
            int offset = 0,
            TimeRange timeRange = TimeRange.MediumTerm,
            string accessToken = null
            ) => GetUsersTopArtists<PagedArtists>(limit, offset, timeRange, accessToken);

        /// <summary>
        /// Get the current user’s top artists based on calculated affinity.
        /// </summary>
        /// <param name="limit">Optional. The maximum number of entities to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedArtists"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/personalization/get-users-top-artists-and-tracks/
        /// </remarks>
        public async Task<T> GetUsersTopArtists<T>(
            int limit = 20,
            int offset = 0,
            TimeRange timeRange = TimeRange.MediumTerm,
            string accessToken = null
            )
        {
            if (limit < 1 || limit > 50) throw new
                ArgumentException("A minimum of 1 and a maximum of 50 artist ids can be sent.");
            if (offset < 0) throw new
                ArgumentException("The offset must be an integer value greater than 0.");

            var builder = new UriBuilder($"{BaseUrl}/me/top/artists");
            builder.AppendToQuery("limit", limit);
            builder.AppendToQuery("offset", offset);
            builder.AppendToQuery("time_range", timeRange == TimeRange.ShortTerm ? "short_term" : 
                (timeRange == TimeRange.LongTerm ? "long_term" : "medium_term"));
            return await GetModel<T>(builder.Uri, accessToken);
        }

        /// <summary>
        /// Get the current user’s top tracks based on calculated affinity.
        /// </summary>
        /// <param name="limit">Optional. The maximum number of entities to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedTracks"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/personalization/get-users-top-artists-and-tracks/
        /// </remarks>
        public Task<PagedTracks> GetUsersTopTracks(
            int limit = 20,
            int offset = 0,
            TimeRange timeRange = TimeRange.MediumTerm,
            string accessToken = null
            ) => GetUsersTopTracks<PagedTracks>(limit, offset, timeRange, accessToken);

        /// <summary>
        /// Get the current user’s top tracks based on calculated affinity.
        /// </summary>
        /// <param name="limit">Optional. The maximum number of entities to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedTracks"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/personalization/get-users-top-artists-and-tracks/
        /// </remarks>
        public async Task<T> GetUsersTopTracks<T>(
            int limit = 20,
            int offset = 0,
            TimeRange timeRange = TimeRange.MediumTerm,
            string accessToken = null
            )
        {
            if (limit < 1 || limit > 50) throw new
                ArgumentException("A minimum of 1 and a maximum of 50 track ids can be sent.");
            if (offset < 0) throw new
                ArgumentException("The offset must be an integer value greater than 0.");

            var builder = new UriBuilder($"{BaseUrl}/me/top/tracks");
            builder.AppendToQuery("limit", limit);
            builder.AppendToQuery("offset", offset);
            builder.AppendToQuery("time_range", timeRange == TimeRange.ShortTerm ? "short_term" :
                (timeRange == TimeRange.LongTerm ? "long_term" : "medium_term"));
            return await GetModel<T>(builder.Uri, accessToken);
        }
        #endregion
    }
}
