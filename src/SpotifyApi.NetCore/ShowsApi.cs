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
    public class ShowsApi : SpotifyWebApi, IShowsApi
    {
        #region constructors
        public ShowsApi(HttpClient httpClient, IAccessTokenProvider accessTokenProvider) : base(httpClient, accessTokenProvider)
        {
        }

        public ShowsApi(HttpClient httpClient, string accessToken) : base(httpClient, accessToken)
        {
        }

        public ShowsApi(HttpClient httpClient) : base(httpClient)
        {
        }
        #endregion

        #region GetShow
        /// <summary>
        /// Get a Show
        /// </summary>
        /// <param name="showId">Required. The Spotify ID for the show.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code. If a country code is specified, only shows and episodes that are available in that market will be returned. If a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter. Note: If neither market or user country are provided, the content is considered unavailable for the client. Users can view the country that is associated with their account in the account settings.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="Show"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/shows/get-a-show/
        /// </remarks>
        public async Task<Show> GetShow(
            string showId,
            string market = null,
            string accessToken = null
            ) => await GetShow<Show>(showId, market, accessToken);

        /// <summary>
        /// Get a Show
        /// </summary>
        /// <param name="showId">Required. The Spotify ID for the show.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code. If a country code is specified, only shows and episodes that are available in that market will be returned. If a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter. Note: If neither market or user country are provided, the content is considered unavailable for the client. Users can view the country that is associated with their account in the account settings.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="Show"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/shows/get-a-show/
        /// </remarks>
        public async Task<T> GetShow<T>(
            string showId,
            string market = null,
            string accessToken = null
            )
        {
            if (string.IsNullOrWhiteSpace(showId)) throw new
                     ArgumentException("A valid show Spotify ID has to be supplied.");

            var builder = new UriBuilder($"{BaseUrl}/shows/{showId}");
            if (!string.IsNullOrWhiteSpace(market))
            {
                builder.AppendToQuery("market", market);
            }
            return await GetModel<T>(builder.Uri, accessToken);
        }
        #endregion

        #region GetSeveralShows
        /// <summary>
        /// Get Several Shows
        /// </summary>
        /// <param name="showIds">Required. A comma-separated list of the Spotify IDs for the shows. Minimum: 1 ID. Maximum: 50 IDs.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code. If a country code is specified, only shows and episodes that are available in that market will be returned. If a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter. Note: If neither market or user country are provided, the content is considered unavailable for the client. Users can view the country that is associated with their account in the account settings.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedShows"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/shows/get-several-shows/
        /// </remarks>
        public async Task<PagedShows> GetSeveralShows(
            string[] showIds,
            string market = null,
            string accessToken = null
            ) => await GetSeveralShows<PagedShows>(showIds, market, accessToken);

        /// <summary>
        /// Get Several Shows
        /// </summary>
        /// <param name="showIds">Required. A comma-separated list of the Spotify IDs for the shows. Minimum: 1 ID. Maximum: 50 IDs.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code. If a country code is specified, only shows and episodes that are available in that market will be returned. If a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter. Note: If neither market or user country are provided, the content is considered unavailable for the client. Users can view the country that is associated with their account in the account settings.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedShows"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/shows/get-several-shows/
        /// </remarks>
        public async Task<T> GetSeveralShows<T>(
            string[] showIds,
            string market = null,
            string accessToken = null
            )
        {
            if (showIds?.Length < 1 || showIds?.Length > 50) throw new
                    ArgumentException("A minimum of 1 and a maximum of 50 show ids can be sent.");

            UriBuilder builder = new UriBuilder($"{BaseUrl}/shows");
            builder.AppendToQueryAsCsv("ids", showIds);
            if (!string.IsNullOrWhiteSpace(market))
            {
                builder.AppendToQuery("market", market);
            }
            return await GetModel<T>(builder.Uri, accessToken);
        }
        #endregion

        #region GetShowEpisodes
        /// <summary>
        /// Get a Show's Episodes
        /// </summary>
        /// <param name="showId">Required. The Spotify ID for the show.</param>
        /// <param name="limit">Optional. The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code. If a country code is specified, only shows and episodes that are available in that market will be returned. If a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter. Note: If neither market or user country are provided, the content is considered unavailable for the client. Users can view the country that is associated with their account in the account settings.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedEpisodes"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/shows/get-shows-episodes/
        /// </remarks>
        public async Task<PagedEpisodes> GetShowEpisodes(
            string showId,
            int limit = 20,
            int offset = 0,
            string market = null,
            string accessToken = null
            ) => await GetShowEpisodes<PagedEpisodes>(showId, limit, offset, market, accessToken);

        /// <summary>
        /// Get a Show's Episodes
        /// </summary>
        /// <param name="showId">Required. The Spotify ID for the show.</param>
        /// <param name="limit">Optional. The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code. If a country code is specified, only shows and episodes that are available in that market will be returned. If a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter. Note: If neither market or user country are provided, the content is considered unavailable for the client. Users can view the country that is associated with their account in the account settings.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedEpisodes"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/shows/get-shows-episodes/
        /// </remarks>
        public async Task<T> GetShowEpisodes<T>(
            string showId,
            int limit = 20,
            int offset = 0,
            string market = null,
            string accessToken = null
            )
        {
            if (string.IsNullOrWhiteSpace(showId)) throw new
                     ArgumentException("A valid show Spotify ID has to be supplied.");
            if (limit < 1 || limit > 50) throw new
                ArgumentException("A minimum of 1 and a maximum of 50 episodes ids can be returned.");
            if (offset < 0) throw new
                ArgumentException("The offset must be an integer value greater than 0.");

            UriBuilder builder = new UriBuilder($"{BaseUrl}/shows/{showId}/episodes");
            builder.AppendToQuery("limit", limit);
            builder.AppendToQuery("offset", offset);
            if (!string.IsNullOrWhiteSpace(market))
            {
                builder.AppendToQuery("market", market);
            }
            return await GetModel<T>(builder.Uri, accessToken);
        }
        #endregion
    }
}
