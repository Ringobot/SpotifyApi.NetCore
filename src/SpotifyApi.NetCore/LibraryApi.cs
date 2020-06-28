using SpotifyApi.NetCore.Authorization;
using SpotifyApi.NetCore.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// An API Wrapper for the Spotify Web API Follow endpoints.
    /// </summary>
    public class LibraryApi : SpotifyWebApi, ILibraryApi
    {
        #region constructors
        public LibraryApi(HttpClient httpClient, IAccessTokenProvider accessTokenProvider) : base(httpClient, accessTokenProvider)
        {
        }

        public LibraryApi(HttpClient httpClient, string accessToken) : base(httpClient, accessToken)
        {
        }

        public LibraryApi(HttpClient httpClient) : base(httpClient)
        {
        }
        #endregion

        #region CheckUserSavedAlbums
        /// <summary>
        /// Check User's Saved Albums
        /// </summary>
        /// <param name="albumIds">Required. A comma-separated list of the Spotify IDs for the albums. Minimum: 1 ID. Maximum: 50 IDs.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/check-users-saved-albums/
        /// </remarks>
        public async Task<bool[]> CheckUserSavedAlbums(
            string[] albumIds,
            string accessToken = null
            ) => await CheckUserSavedAlbums<bool[]>(albumIds, accessToken);

        /// <summary>
        /// Check User's Saved Albums
        /// </summary>
        /// <param name="albumIds">Required. A comma-separated list of the Spotify IDs for the albums. Minimum: 1 ID. Maximum: 50 IDs.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/check-users-saved-albums/
        /// </remarks>
        public async Task<T> CheckUserSavedAlbums<T>(
            string[] albumIds,
            string accessToken = null
            )
        {
            if (albumIds?.Length < 1 || albumIds?.Length > 50) throw new
                    ArgumentException("A minimum of 1 and a maximum of 50 album ids can be sent.");

            var builder = new UriBuilder($"{BaseUrl}/me/albums/contains");
            builder.AppendToQueryAsCsv("ids", albumIds);
            return await GetModel<T>(builder.Uri, accessToken);
        }
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
        public async Task<bool[]> CheckUserSavedShows(
            string[] showIds,
            string accessToken = null
            ) => await CheckUserSavedShows<bool[]>(showIds, accessToken);

        /// <summary>
        /// Check User's Saved Shows
        /// </summary>
        /// <param name="showIds">Required. A comma-separated list of the Spotify IDs for the shows. Minimum: 1 ID. Maximum: 50 IDs.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/check-users-saved-shows/
        /// </remarks>
        public async Task<T> CheckUserSavedShows<T>(
            string[] showIds,
            string accessToken = null
            )
        {
            if (showIds?.Length < 1 || showIds?.Length > 50) throw new
                    ArgumentException("A minimum of 1 and a maximum of 50 show ids can be sent.");

            var builder = new UriBuilder($"{BaseUrl}/me/shows/contains");
            builder.AppendToQueryAsCsv("ids", showIds);
            return await GetModel<T>(builder.Uri, accessToken);
        }
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
        public async Task<bool[]> CheckUserSavedTracks(
            string[] trackIds,
            string accessToken = null
            ) => await CheckUserSavedTracks<bool[]>(trackIds, accessToken);

        /// <summary>
        /// Check User's Saved Tracks
        /// </summary>
        /// <param name="trackIds">Required. A comma-separated list of the Spotify IDs for the tracks. Minimum: 1 ID. Maximum: 50 IDs.</param>
        /// <returns>bool[] an array of true or false values, in the same order in which the ids were specified.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/check-users-saved-tracks/
        /// </remarks>
        public async Task<T> CheckUserSavedTracks<T>(
            string[] trackIds,
            string accessToken = null
            )
        {
            if (trackIds?.Length < 1 || trackIds?.Length > 50) throw new
                    ArgumentException("A minimum of 1 and a maximum of 50 track ids can be sent.");

            var builder = new UriBuilder($"{BaseUrl}/me/tracks/contains");
            builder.AppendToQueryAsCsv("ids", trackIds);
            return await GetModel<T>(builder.Uri, accessToken);
        }
        #endregion

        #region GetUserSavedAlbums
        /// <summary>
        /// Get Current User's Saved Albums
        /// </summary>
        /// <param name="limit">Optional. The maximum number of objects to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string from_token. Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns>A json string containing an array of album objects (wrapped in a paging object) in JSON format. Each album object is accompanied by a timestamp (added_at) to show when it was added. There is also an etag in the header that can be used in future conditional requests.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/get-users-saved-albums/
        /// </remarks>
        public async Task<PagedAlbums> GetUserSavedAlbums(
            int limit = 20,
            int offset = 0,
            string market = null,
            string accessToken = null
            ) => await GetUserSavedAlbums<PagedAlbums>(limit, offset, market, accessToken);

        /// <summary>
        /// Get Current User's Saved Albums
        /// </summary>
        /// <param name="limit">Optional. The maximum number of objects to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string from_token. Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns>A json string containing an array of album objects (wrapped in a paging object) in JSON format. Each album object is accompanied by a timestamp (added_at) to show when it was added. There is also an etag in the header that can be used in future conditional requests.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/get-users-saved-albums/
        /// </remarks>
        public async Task<T> GetUserSavedAlbums<T>(
            int limit = 20,
            int offset = 0,
            string market = null,
            string accessToken = null
            )
        {
            if (limit < 1 || limit > 50) throw new
                    ArgumentException("The limit can be a minimum of 1 and a maximum of 50 album IDs.");
            if(offset < 0) throw new
                    ArgumentException("The offset must be an integer value greater than 0.");

            var builder = new UriBuilder($"{BaseUrl}/me/albums");
            builder.AppendToQuery("limit", limit);
            builder.AppendToQuery("offset", offset);
            if (!string.IsNullOrWhiteSpace(market))
            {
                builder.AppendToQuery("market", market);
            }
            return await GetModel<T>(builder.Uri, accessToken);
        }
        #endregion

        #region GetUserSavedShows
        /// <summary>
        /// Get User's Saved Shows
        /// </summary>
        /// <param name="limit">Optional. The maximum number of objects to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <returns>A json string containing an array of saved show objects (wrapped in a paging object) in JSON format. If the current user has no shows saved, the response will be an empty array. If a show is unavailable in the given market it is filtered out. The total field in the paging object represents the number of all items, filtered or not, and thus might be larger than the actual total number of observable items.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/get-users-saved-shows/
        /// </remarks>
        public async Task<PagedShows> GetUserSavedShows(
            int limit = 20,
            int offset = 0,
            string accessToken = null
            ) => await GetUserSavedShows<PagedShows>(limit, offset, accessToken);

        /// <summary>
        /// Get User's Saved Shows
        /// </summary>
        /// <param name="limit">Optional. The maximum number of objects to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <returns>A json string containing an array of saved show objects (wrapped in a paging object) in JSON format. If the current user has no shows saved, the response will be an empty array. If a show is unavailable in the given market it is filtered out. The total field in the paging object represents the number of all items, filtered or not, and thus might be larger than the actual total number of observable items.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/get-users-saved-shows/
        /// </remarks>
        public async Task<T> GetUserSavedShows<T>(
            int limit = 20,
            int offset = 0,
            string accessToken = null
            )
        {
            if (limit < 1 || limit > 50) throw new
                    ArgumentException("The limit can be a minimum of 1 and a maximum of 50.");
            if (offset < 0) throw new
                     ArgumentException("The offset must be an integer value greater than 0.");

            var builder = new UriBuilder($"{BaseUrl}/me/albums");
            builder.AppendToQuery("limit", limit);
            builder.AppendToQuery("offset", offset);
            return await GetModel<T>(builder.Uri, accessToken);
        }
        #endregion

        #region GetUserSavedTracks
        /// <summary>
        /// Get User's Saved Tracks
        /// </summary>
        /// <param name="limit">Optional. The maximum number of objects to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string from_token. Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns>A json string containing an array of saved track objects (wrapped in a paging object) in JSON format.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/get-users-saved-tracks/
        /// </remarks>
        public async Task<PagedTracks> GetUserSavedTracks(
            int limit = 20,
            int offset = 0,
            string market = null,
            string accessToken = null
            ) => await GetUserSavedTracks<PagedTracks>(limit, offset, market, accessToken);

        /// <summary>
        /// Get User's Saved Tracks
        /// </summary>
        /// <param name="limit">Optional. The maximum number of objects to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string from_token. Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns>A json string containing an array of saved track objects (wrapped in a paging object) in JSON format.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/get-users-saved-tracks/
        /// </remarks>
        public async Task<T> GetUserSavedTracks<T>(
            int limit = 20,
            int offset = 0,
            string market = null,
            string accessToken = null
            )
        {
            if (limit < 1 || limit > 50) throw new
                    ArgumentException("The limit can be a minimum of 1 and a maximum of 50.");
            if (offset < 0) throw new
                     ArgumentException("The offset must be an integer value greater than 0.");

            var builder = new UriBuilder($"{BaseUrl}/me/albums");
            builder.AppendToQuery("limit", limit);
            builder.AppendToQuery("offset", offset);
            if (!string.IsNullOrWhiteSpace(market))
            {
                builder.AppendToQuery("market", market);
            }
            return await GetModel<T>(builder.Uri, accessToken);
        }
        #endregion

        #region RemoveAlbumsForCurrentUser
        /// <summary>
        /// Remove Albums for Current User
        /// </summary>
        /// <param name="albumIds">A comma-separated list of the album Spotify IDs. A maximum of 50 album IDs can be sent in one request. A minimum of 1 album id is required. </param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/remove-albums-user/
        /// </remarks>
        public async Task RemoveAlbumsForCurrentUser(
            string[] albumIds,
            string accessToken = null
            )
        {
            if (albumIds?.Length < 1 || albumIds?.Length > 50) throw new
                    ArgumentException("The album ids can be a minimum of 1 and a maximum of 50.");

            var builder = new UriBuilder($"{BaseUrl}/me/albums");
            builder.AppendToQueryAsCsv("ids", albumIds);
            await Delete(builder.Uri, accessToken);
        }
        #endregion

        #region RemoveUserSavedShows
        /// <summary>
        /// Remove User's Saved Shows
        /// </summary>
        /// <param name="showIds">Required. A comma-separated list of the show Spotify IDs. A maximum of 50 show IDs can be sent in one request. A minimum of 1 show id is required.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code. If a country code is specified, only shows that are available in that market will be removed. If a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter. Note: If neither market or user country are provided, the content is considered unavailable for the client. Users can view the country that is associated with their account in the account settings.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/remove-shows-user/
        /// </remarks>
        public async Task RemoveUserSavedShows(
            string[] showIds,
            string market = null,
            string accessToken = null
            )
        {
            if (showIds?.Length < 1 || showIds?.Length > 50) throw new
                    ArgumentException("The show ids can be a minimum of 1 and a maximum of 50.");

            var builder = new UriBuilder($"{BaseUrl}/me/shows");
            builder.AppendToQueryAsCsv("ids", showIds);
            if (string.IsNullOrWhiteSpace(market))
            {
                builder.AppendToQuery("market", market);
            }
            await Delete(builder.Uri, accessToken);
        }
        #endregion

        #region RemoveUserSavedTracks
        /// <summary>
        /// Remove User's Saved Tracks
        /// </summary>
        /// <param name="trackIds">Required. A comma-separated list of the track Spotify IDs. A maximum of 50 track IDs can be sent in one request. A minimum of 1 track id is required.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/remove-tracks-user/
        /// </remarks>
        public async Task RemoveUserSavedTracks(
            string[] trackIds,
            string accessToken = null
            )
        {
            if (trackIds?.Length < 1 || trackIds?.Length > 50) throw new
                    ArgumentException("The track ids can be a minimum of 1 and a maximum of 50.");

            var builder = new UriBuilder($"{BaseUrl}/me/tracks");
            builder.AppendToQueryAsCsv("ids", trackIds);
            await Delete(builder.Uri, accessToken);
        }
        #endregion

        #region SaveAlbumsForCurrentUser
        /// <summary>
        /// Save Albums for Current User
        /// </summary>
        /// <param name="albumIds">Required. A comma-separated list of the album Spotify IDs. A maximum of 50 album IDs can be sent in one request. A minimum of 1 album id is required.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/save-albums-user/
        /// </remarks>
        public async Task SaveAlbumsForCurrentUser(
            string[] albumIds,
            string accessToken = null
            )
        {
            if (albumIds?.Length < 1 && albumIds.Length > 50) throw new
                    ArgumentException("A minimum of 1 and a maximum of 50 album ids can be sent.");

            var builder = new UriBuilder($"{BaseUrl}/me/albums");
            builder.AppendToQueryAsCsv("ids", albumIds);
            await Put(builder.Uri, null, accessToken);
        }
        #endregion

        #region SaveShowsForCurrentUser
        /// <summary>
        /// Save Shows for Current User
        /// </summary>
        /// <param name="showIds">Required. A comma-separated list of the show Spotify IDs. A maximum of 50 show IDs can be sent in one request. A minimum of 1 show id is required.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/save-shows-user/
        /// </remarks>
        public async Task SaveShowsForCurrentUser(
            string[] showIds,
            string accessToken = null
            )
        {
            if (showIds?.Length < 1 && showIds.Length > 50) throw new
                    ArgumentException("A minimum of 1 and a maximum of 50 show ids can be sent.");

            var builder = new UriBuilder($"{BaseUrl}/me/shows");
            builder.AppendToQueryAsCsv("ids", showIds);
            await Put(builder.Uri, null, accessToken);
        }
        #endregion

        #region SaveTracksForCurrentUser
        /// <summary>
        /// Save Shows for Current User
        /// </summary>
        /// <param name="trackIds">Required. A comma-separated list of the track Spotify IDs. A maximum of 50 track IDs can be sent in one request. A minimum of 1 track id is required.</param>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/library/save-tracks-user/
        /// </remarks>
        public async Task SaveTracksForCurrentUser(
            string[] trackIds,
            string accessToken = null
            )
        {
            if (trackIds?.Length < 1 && trackIds.Length > 50) throw new
                    ArgumentException("A minimum of 1 and a maximum of 50 track ids can be sent.");

            var builder = new UriBuilder($"{BaseUrl}/me/tracks");
            builder.AppendToQueryAsCsv("ids", trackIds);
            await Put(builder.Uri, null, accessToken);
        }
        #endregion
    }
}
