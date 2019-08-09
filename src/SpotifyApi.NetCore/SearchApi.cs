using SpotifyApi.NetCore.Authorization;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Get Spotify Catalog information about artists, albums, tracks or playlists that match a keyword string.
    /// </summary>
    public class SearchApi : SpotifyWebApi, ISearchApi
    {
        #region Constructors

        /// <summary>
        /// Instantiates a new <see cref="SearchApi"/>.
        /// </summary>
        /// <remarks>
        /// Use this constructor when an accessToken will be provided using the `accessToken` parameter 
        /// on each method
        /// </remarks>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/></param>
        public SearchApi(HttpClient httpClient) : base(httpClient)
        {
        }

        /// <summary>
        /// Instantiates a new <see cref="SearchApi"/>.
        /// </summary>
        /// <remarks>
        /// This constructor accepts a Spotify access token that will be used for all calls to the API 
        /// (except when an accessToken is provided using the optional `accessToken` parameter on each method).
        /// </remarks>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/></param>
        /// <param name="accessToken">A valid access token from the Spotify Accounts service</param>
        public SearchApi(HttpClient httpClient, string accessToken) : base(httpClient, accessToken)
        {
        }

        /// <summary>
        /// Instantiates a new <see cref="SearchApi"/>.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/></param>
        /// <param name="accessTokenProvider">An instance of <see cref="IAccessTokenProvider"/>, e.g. <see cref="Authorization.AccountsService"/>.</param>
        public SearchApi(HttpClient httpClient, IAccessTokenProvider accessTokenProvider) : base(httpClient, accessTokenProvider)
        {
        }

        #endregion

        /// <summary>
        /// Get Spotify Catalog information about artists, albums, tracks or playlists that match a
        /// keyword string.
        /// </summary>
        /// <param name="query">Search query keywords and optional field filters and operators. For 
        /// example: `q=roadhouse%20blues`. See also https://developer.spotify.com/documentation/web-api/reference/search/search/#writing-a-query---guidelines </param>
        /// <param name="type">Specify one of <see cref="SpotifySearchTypes"/>.</param>
        /// <param name="market">Optional. Choose a <see cref="SpotifyCountryCodes"/>. If a country code
        /// is specified, only artists, albums, and tracks with content that is playable in that market 
        /// is returned. Note: Playlist results are not affected by the market parameter.</param>
        /// <param name="limit">Optional. Maximum number of results to return. Default: 20, Minimum: 1,
        /// Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first result to return. Default: 0 (the
        /// first result). Maximum offset (including limit): 10,000. Use with limit to get the next
        /// page of search results.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service,
        /// used for this call only. See constructors for more ways to provide access tokens.</param>
        /// <returns>Task of <see cref="SearchResult"/></returns>
        public async Task<SearchResult> Search(
            string query,
            string type,
            string market = null,
            int? limit = null,
            int offset = 0,
            string accessToken = null) => await Search<SearchResult>(
                query,
                new string[] { type },
                market: market,
                limit: limit,
                offset: offset,
                accessToken: accessToken);

        /// <summary>
        /// Get Spotify Catalog information about artists, albums, tracks or playlists that match a
        /// keyword string.
        /// </summary>
        /// <param name="query">Search query keywords and optional field filters and operators. For 
        /// example: `q=roadhouse%20blues`. See also https://developer.spotify.com/documentation/web-api/reference/search/search/#writing-a-query---guidelines </param>
        /// <param name="types">Specify multiple <see cref="SpotifySearchTypes"/> to search across.</param>
        /// <param name="market">Optional. Choose a <see cref="SpotifyCountryCodes"/>. If a country code
        /// is specified, only artists, albums, and tracks with content that is playable in that market 
        /// is returned. Note: Playlist results are not affected by the market parameter.</param>
        /// <param name="limit">Optional. Maximum number of results to return. Default: 20, Minimum: 1,
        /// Maximum: 50. Note: The limit is applied within each type, not on the total response. For
        /// example, if the limit value is 3 and the type is `artist,album`, the response contains 3
        /// artists and 3 albums.</param>
        /// <param name="offset">Optional. The index of the first result to return. Default: 0 (the
        /// first result). Maximum offset (including limit): 10,000. Use with limit to get the next
        /// page of search results.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service,
        /// used for this call only. See constructors for more ways to provide access tokens.</param>
        /// <returns>Task of <see cref="SearchResult"/></returns>
        public async Task<SearchResult> Search(
            string query,
            string[] types,
            string market = null,
            int? limit = null,
            int offset = 0,
            string accessToken = null) => await Search<SearchResult>(
                query,
                types,
                market: market,
                limit: limit,
                offset: offset,
                accessToken: accessToken);

        /// <summary>
        /// Get Spotify Catalog information about artists, albums, tracks or playlists that match a
        /// keyword string.
        /// </summary>
        /// <param name="query">Search query keywords and optional field filters and operators. For 
        /// example: `q=roadhouse%20blues`. See also https://developer.spotify.com/documentation/web-api/reference/search/search/#writing-a-query---guidelines </param>
        /// <param name="types">Specify multiple <see cref="SpotifySearchTypes"/> to search across.</param>
        /// <param name="market">Optional. Choose a <see cref="SpotifyCountryCodes"/>. If a country code
        /// is specified, only artists, albums, and tracks with content that is playable in that market 
        /// is returned. Note: Playlist results are not affected by the market parameter.</param>
        /// <param name="limit">Optional. Maximum number of results to return. Default: 20, Minimum: 1,
        /// Maximum: 50. Note: The limit is applied within each type, not on the total response. For
        /// example, if the limit value is 3 and the type is `artist,album`, the response contains 3
        /// artists and 3 albums.</param>
        /// <param name="offset">Optional. The index of the first result to return. Default: 0 (the
        /// first result). Maximum offset (including limit): 10,000. Use with limit to get the next
        /// page of search results.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service,
        /// used for this call only. See constructors for more ways to provide access tokens.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        public async Task<T> Search<T>(
            string query,
            string[] types,
            string market = null,
            int? limit = null,
            int offset = 0,
            string accessToken = null)
        {
            if (string.IsNullOrWhiteSpace(query)) throw new ArgumentNullException("query");
            if (types == null || types.Length == 0) throw new ArgumentNullException("types");

            string typeQuery = string.Join(",", types);
            string url = $"{BaseUrl}/search?q={Uri.EscapeDataString(query)}&type={typeQuery}";

            if (!string.IsNullOrWhiteSpace(market))
            {
                url += $"&market={market}";
            }

            if (limit.HasValue && limit > 0)
            {
                url += $"&limit={limit.Value}";
            }

            if (offset > 0)
            {
                url += $"&offset={offset}";
            }

            return await GetModel<T>(url, accessToken);
        }
    }
}