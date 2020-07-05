using SpotifyApi.NetCore.Authorization;
using SpotifyApi.NetCore.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using SpotifyApi.NetCore.Helpers;

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

        #region GetUsersTopArtistsOrTracks
        /// <summary>
        /// Get the current user’s top artists based on calculated affinity.
        /// </summary>
        /// <param name="limit">Optional. The maximum number of entities to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <param name="timeRange">Optional. Over what time frame the affinities are computed. Valid values: long_term (calculated from several years of data and including all new data as it becomes available), medium_term (approximately last 6 months), short_term (approximately last 4 weeks). Default: medium_term.</param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedArtists"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/personalization/get-users-top-artists-and-tracks/
        /// </remarks>
        public Task<PagedArtists> GetUsersTopArtists(
            int limit = 20,
            int offset = 0,
            TimeRange timeRange = TimeRange.MediumTerm,
            string accessToken = null
            ) => GetUsersTopArtistsOrTracks<PagedArtists>("artist", limit, offset, timeRange, accessToken);

        /// <summary>
        /// Get the current user’s top artists based on calculated affinity.
        /// </summary>
        /// <param name="limit">Optional. The maximum number of entities to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <param name="timeRange">Optional. Over what time frame the affinities are computed. Valid values: long_term (calculated from several years of data and including all new data as it becomes available), medium_term (approximately last 6 months), short_term (approximately last 4 weeks). Default: medium_term.</param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <returns>A Task that, once successfully completed, returns a instance of `T`.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/personalization/get-users-top-artists-and-tracks/
        /// </remarks>
        public Task<T> GetUsersTopArtists<T>(
            int limit = 20,
            int offset = 0,
            TimeRange timeRange = TimeRange.MediumTerm,
            string accessToken = null
            ) => GetUsersTopArtistsOrTracks<T>("artist", limit, offset, timeRange, accessToken);

        /// <summary>
        /// Get the current user’s top tracks based on calculated affinity.
        /// </summary>
        /// <param name="limit">Optional. The maximum number of entities to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <param name="timeRange">Optional. Over what time frame the affinities are computed. Valid values: long_term (calculated from several years of data and including all new data as it becomes available), medium_term (approximately last 6 months), short_term (approximately last 4 weeks). Default: medium_term.</param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedTracks"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/personalization/get-users-top-artists-and-tracks/
        /// </remarks>
        public Task<PagedTracks> GetUsersTopTracks(
            int limit = 20,
            int offset = 0,
            TimeRange timeRange = TimeRange.MediumTerm,
            string accessToken = null
            ) => GetUsersTopArtistsOrTracks<PagedTracks>("track", limit, offset, timeRange, accessToken);

        /// <summary>
        /// Get the current user’s top tracks based on calculated affinity.
        /// </summary>
        /// <param name="limit">Optional. The maximum number of entities to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <param name="timeRange">Optional. Over what time frame the affinities are computed. Valid values: long_term (calculated from several years of data and including all new data as it becomes available), medium_term (approximately last 6 months), short_term (approximately last 4 weeks). Default: medium_term.</param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <returns>A Task that, once successfully completed, returns a instance of `T`.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/personalization/get-users-top-artists-and-tracks/
        /// </remarks>
        public Task<T> GetUsersTopTracks<T>(
            int limit = 20,
            int offset = 0,
            TimeRange timeRange = TimeRange.MediumTerm,
            string accessToken = null
            ) => GetUsersTopArtistsOrTracks<T>("track", limit, offset, timeRange, accessToken);

        internal async Task<T> GetUsersTopArtistsOrTracks<T>(
            string type,
            int limit = 20,
            int offset = 0,
            TimeRange timeRange = TimeRange.MediumTerm,
            string accessToken = null
            )
        {
            if (type != "artist" && type != "track") throw new
                     ArgumentException("The type value can be one of either artist or track.");
            if (limit < 1 || limit > 50) throw new
                ArgumentException($"A minimum of 1 and a maximum of 50 {type} ids can be sent.");
            if (offset < 0) throw new
                ArgumentException("The offset must be an integer value greater than 0.");

            var builder = new UriBuilder($"{BaseUrl}/me/top/{type}s");
            builder.AppendToQuery("limit", limit);
            builder.AppendToQuery("offset", offset);
            builder.AppendToQuery("time_range",  TimeRangeHelper.TimeRangeString(timeRange));
            return await GetModel<T>(builder.Uri, accessToken);
        }
        #endregion
    }
}
