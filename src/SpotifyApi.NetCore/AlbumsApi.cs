using SpotifyApi.NetCore.Authorization;
using SpotifyApi.NetCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Endpoints for retrieving information about one or more albums from the Spotify catalog.
    /// </summary>
    public class AlbumsApi : SpotifyWebApi, IAlbumsApi
    {
        protected internal virtual ISearchApi SearchApi { get; set; }

        #region Constructors

        /// <summary>
        /// Instantiates a new <see cref="AlbumsApi"/>.
        /// </summary>
        /// <remarks>
        /// Use this constructor when an accessToken will be provided using the `accessToken` parameter 
        /// on each method
        /// </remarks>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/></param>
        public AlbumsApi(HttpClient httpClient) : base(httpClient)
        {
            SearchApi = new SearchApi(httpClient);
        }

        /// <summary>
        /// Instantiates a new <see cref="AlbumsApi"/>.
        /// </summary>
        /// <remarks>
        /// This constructor accepts a Spotify access token that will be used for all calls to the API 
        /// (except when an accessToken is provided using the optional `accessToken` parameter on each method).
        /// </remarks>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/></param>
        /// <param name="accessToken">A valid access token from the Spotify Accounts service</param>
        public AlbumsApi(HttpClient httpClient, string accessToken) : base(httpClient, accessToken)
        {
            SearchApi = new SearchApi(httpClient, accessToken);
        }

        /// <summary>
        /// Instantiates a new <see cref="AlbumsApi"/>.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/></param>
        /// <param name="accessTokenProvider">An instance of <see cref="IAccessTokenProvider"/>, e.g. <see cref="Authorization.AccountsService"/>.</param>
        public AlbumsApi(HttpClient httpClient, IAccessTokenProvider accessTokenProvider) : base(httpClient, accessTokenProvider)
        {
            SearchApi = new SearchApi(httpClient, accessTokenProvider);
        }

        #endregion

        #region GetAlbum

        /// <summary>
        /// Get Spotify catalog information for a single album.
        /// </summary>
        /// <param name="albumId">The Spotify ID for the album.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string `from_token` 
        /// (See <see cref="SpotifyCountryCodes"/>). Provide this parameter if you want to apply Track Relinking.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="Album"/> object.</returns>
        /// <remarks>https://developer.spotify.com/documentation/web-api/reference/albums/get-album/</remarks>
        public async Task<Album> GetAlbum(string albumId, string market = null, string accessToken = null)
            => await GetAlbum<Album>(albumId, market: market, accessToken: accessToken);

        /// <summary>
        /// Get Spotify catalog information for a single album.
        /// </summary>
        /// <param name="albumId">The Spotify ID for the album.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string `from_token` 
        /// (See <see cref="SpotifyCountryCodes"/>). Provide this parameter if you want to apply Track Relinking.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>A Task that, once successfully completed, returns a Model of T.</returns>       
        /// <remarks>https://developer.spotify.com/documentation/web-api/reference/albums/get-album/</remarks>
        public async Task<T> GetAlbum<T>(string albumId, string market = null, string accessToken = null)
        {
            string url = $"{BaseUrl}/albums/{SpotifyUriHelper.AlbumId(albumId)}";
            if (!string.IsNullOrEmpty(market)) url += $"?market={market}";
            return await GetModel<T>(url, accessToken);
        }

        #endregion

        #region GetAlbumTracks

        /// <summary>
        /// Get Spotify catalog information about an album’s tracks. Optional parameters can be used to limit the number of tracks returned.
        /// </summary>
        /// <param name="albumId">The Spotify ID for the album.</param>
        /// <param name="limit">Optional. The maximum number of tracks to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first track to return. Default: 0 (the first 
        /// object). Use with limit to get the next set of tracks.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string `from_token` 
        /// (See <see cref="SpotifyCountryCodes"/>). Provide this parameter if you want to apply Track Relinking.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="Album"/> object.</returns>
        /// <remarks>https://developer.spotify.com/documentation/web-api/reference/albums/get-albums-tracks/</remarks>
        public async Task<Album> GetAlbumTracks(
            string albumId,
            int? limit = null,
            int offset = 0,
            string market = null,
            string accessToken = null)
            => await GetAlbumTracks<Album>(albumId, limit: limit, offset: offset, market: market, accessToken: accessToken);

        /// <summary>
        /// Get Spotify catalog information about an album’s tracks. Optional parameters can be used to limit the number of tracks returned.
        /// </summary>
        /// <param name="albumId">The Spotify ID for the album.</param>
        /// <param name="limit">Optional. The maximum number of tracks to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first track to return. Default: 0 (the first 
        /// object). Use with limit to get the next set of tracks.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string `from_token` 
        /// (See <see cref="SpotifyCountryCodes"/>). Provide this parameter if you want to apply Track Relinking.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>A Task that, once successfully completed, returns a Model of T.</returns>
        /// <remarks>https://developer.spotify.com/documentation/web-api/reference/albums/get-albums-tracks/</remarks>
        public async Task<T> GetAlbumTracks<T>(
            string albumId,
            int? limit = null,
            int offset = 0,
            string market = null,
            string accessToken = null)
        {
            string url = $"{BaseUrl}/albums/{SpotifyUriHelper.AlbumId(albumId)}/tracks";
            if (limit.HasValue || !string.IsNullOrEmpty(market)) url += "?";
            if (limit.HasValue) url += $"limit={limit.Value}&offset={offset}&";
            if (!string.IsNullOrEmpty(market)) url += $"market={market}";
            return await GetModel<T>(url, accessToken);
        }

        #endregion

        #region GetAlbums

        /// <summary>
        /// Get Spotify catalog information for multiple albums identified by their Spotify IDs.
        /// </summary>
        /// <param name="albumIds">An array of the Spotify IDs for the albums. Maximum: 20 IDs.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string `from_token` 
        /// (See <see cref="SpotifyCountryCodes"/>). Provide this parameter if you want to apply Track Relinking.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.</param>
        /// <returns>A Task that, once successfully completed, returns an array of full <see cref="Album"/> objects.</returns>
        public async Task<Album[]> GetAlbums(string[] albumIds, string market = null, string accessToken = null)
            => await GetAlbums<Album[]>(albumIds, market: market, accessToken: accessToken);

        /// <summary>
        /// Get Spotify catalog information for multiple albums identified by their Spotify IDs.
        /// </summary>
        /// <param name="albumIds">An array of the Spotify IDs for the albums. Maximum: 20 IDs.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string `from_token` 
        /// (See <see cref="SpotifyCountryCodes"/>). Provide this parameter if you want to apply Track Relinking.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>A Task that, once successfully completed, returns a Model of T.</returns>
        public async Task<T> GetAlbums<T>(string[] albumIds, string market = null, string accessToken = null)
        {
            if (albumIds == null || albumIds.Length == 0 || string.IsNullOrEmpty(albumIds[0]))
            {
                throw new ArgumentNullException(nameof(albumIds));
            }

            IEnumerable<string> ids = albumIds.Select(SpotifyUriHelper.ArtistId);

            return await GetModelFromProperty<T>($"{BaseUrl}/albums?ids={string.Join(",", ids)}", "albums", accessToken);
        }

        #endregion

        #region SearchAlbums

        /// <summary>
        /// Get Spotify Catalog information about albums that match a keyword string.
        /// </summary>
        /// <param name="query">Search query keywords and optional field filters and operators. See
        /// https://developer.spotify.com/documentation/web-api/reference/search/search/#writing-a-query---guidelines</param>
        /// <param name="market">Optional. Choose a <see cref="SpotifyCountryCodes"/>. If a country code
        /// is specified, only tracks with content that is playable in that market is returned. </param>
        /// <param name="limit">Optional. Maximum number of results to return. Default: 20, Minimum: 1,
        /// Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first result to return. Default: 0 (the
        /// first result). Maximum offset (including limit): 10,000. Use with limit to get the next
        /// page of search results.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.</param>
        /// <returns>Task of <see cref="AlbumsSearchResult" /></returns>
        public async Task<SearchResult> SearchAlbums(
            string query,
            int? limit = null,
            int offset = 0,
            string market = null,
            string accessToken = null)
            => await SearchApi.Search<SearchResult>(
                query,
                new string[] { SpotifySearchTypes.Album },
                market: market,
                limit: limit,
                offset: offset,
                accessToken);

        #endregion
    }

}
