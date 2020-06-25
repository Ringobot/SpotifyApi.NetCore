using SpotifyApi.NetCore.Authorization;
using SpotifyApi.NetCore.Helpers;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// An API Wrapper for the Spotify Web API Follow endpoints.
    /// </summary>
    public class FollowApi : SpotifyWebApi, IFollowApi
    {
        #region constructors

        public FollowApi(HttpClient httpClient, IAccessTokenProvider accessTokenProvider) : base(httpClient, accessTokenProvider)
        {
            SearchApi = new SearchApi(httpClient, accessTokenProvider);
        }

        public FollowApi(HttpClient httpClient, string accessToken) : base(httpClient, accessToken)
        {
            SearchApi = new SearchApi(httpClient, accessToken);
        }

        public FollowApi(HttpClient httpClient) : base(httpClient)
        {
            SearchApi = new SearchApi(httpClient);
        }

        #endregion
        protected internal virtual ISearchApi SearchApi { get; set; }

        #region CheckCurrentUserFollowsArtistsOrUsers

        /// <summary>
        /// Check if Current User Follows Artists
        /// </summary>
        /// <param name="ids">	Required. A comma-separated list of the artist or the user Spotify IDs to check. For example: ids=74ASZWbe4lXaubB36ztrGX,08td7MxkoHQkXnWAYD8d6Q. A maximum of 50 IDs can be sent in one request.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/check-current-user-follows/
        /// </remarks>
        public async Task<bool[]> CheckCurrentUserFollowsArtists(
            string[] ids,
            string accessToken = null
            ) => await CheckCurrentUserFollowsArtists<bool[]>(ids, accessToken);

        /// <summary>
        /// Check if Current User Follows Artists
        /// </summary>
        /// <param name="ids">	Required. A comma-separated list of the artist or the user Spotify IDs to check. For example: ids=74ASZWbe4lXaubB36ztrGX,08td7MxkoHQkXnWAYD8d6Q. A maximum of 50 IDs can be sent in one request.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/check-current-user-follows/
        /// </remarks>
        public async Task<T> CheckCurrentUserFollowsArtists<T>(
            string[] ids,
            string accessToken = null
            )
        {
            if (ids?.Length < 1 || ids?.Length > 50) throw new ArgumentNullException("ids");

            var builder = new UriBuilder($"{BaseUrl}/me/following/contains");
            builder.AppendToQuery("type", "artist");
            builder.AppendToQueryAsCsv("ids", ids);
            return await GetModel<T>(builder.Uri, accessToken);
        }

        /// <summary>
        /// Check if Current User Follows Users
        /// </summary>
        /// <param name="ids">	Required. A comma-separated list of the artist or the user Spotify IDs to check. For example: ids=74ASZWbe4lXaubB36ztrGX,08td7MxkoHQkXnWAYD8d6Q. A maximum of 50 IDs can be sent in one request.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/check-current-user-follows/
        /// </remarks>
        public async Task<bool[]> CheckCurrentUserFollowsUsers(
            string[] ids,
            string accessToken = null
            ) => await CheckCurrentUserFollowsUsers<bool[]>(ids, accessToken);

        /// <summary>
        /// Check if Current User Follows Users
        /// </summary>
        /// <param name="ids">	Required. A comma-separated list of the artist or the user Spotify IDs to check. For example: ids=74ASZWbe4lXaubB36ztrGX,08td7MxkoHQkXnWAYD8d6Q. A maximum of 50 IDs can be sent in one request.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/check-current-user-follows/
        /// </remarks>
        public async Task<T> CheckCurrentUserFollowsUsers<T>(
            string[] ids,
            string accessToken = null
            )
        {
            if (ids?.Length < 1 || ids?.Length > 50) throw new ArgumentNullException("ids");

            var builder = new UriBuilder($"{BaseUrl}/me/following/contains");
            builder.AppendToQuery("type", "user");
            builder.AppendToQueryAsCsv("ids", ids);
            return await GetModel<T>(builder.Uri, accessToken);
        }
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
        public Task<bool[]> CheckUsersFollowPlaylist(
            string playlistId,
            string[] userIds,
            string accessToken = null
            ) => CheckUsersFollowPlaylist<bool[]>(playlistId, userIds, accessToken);

        /// <summary>
        /// Check to see if one or more Spotify users are following a specified playlist.
        /// </summary>
        /// <param name="playlistId">Required. The Spotify ID of the playlist.</param>
        /// <param name="userIds">Required. A comma-separated list of Spotify User IDs ; the ids of the users that you want to check to see if they follow the playlist. Maximum: 5 ids.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/follow/check-user-following-playlist/
        /// </remarks>
        public async Task<T> CheckUsersFollowPlaylist<T>(
            string playlistId,
            string[] userIds,
            string accessToken = null
            )
        {
            if (playlistId?.Length < 1) throw new ArgumentNullException("playlistId");

            if (userIds?.Length < 1 || userIds?.Length > 5) throw new ArgumentNullException("ids");

            var builder = new UriBuilder($"{BaseUrl}/playlists/{playlistId}/followers/contains");
            builder.AppendToQueryAsCsv("ids", userIds);
            return await GetModel<T>(builder.Uri, accessToken);
        }
        #endregion
    }
}
