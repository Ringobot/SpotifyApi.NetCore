using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    public interface ISearchApi
    {
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
        /// <returns>Task of <see cref="SearchResult"/></returns>
        Task<SearchResult> Search(string query, string type, string market, (int limit, int offset) limitOffset);

        /// <summary>
        /// Get Spotify Catalog information about artists, albums, tracks or playlists that match a
        /// keyword string.
        /// </summary>
        /// <param name="query">Search query keywords and optional field filters and operators. For 
        /// example: `q=roadhouse%20blues`. See also https://developer.spotify.com/documentation/web-api/reference/search/search/#writing-a-query---guidelines </param>
        /// <param name="type">Specify multiple <see cref="SpotifySearchTypes"/> to search across.</param>
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
        /// <returns>Task of <see cref="SearchResult"/></returns>
        Task<SearchResult> Search(string query, string[] types, string market, (int limit, int offset) limitOffset);

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
        /// <returns>Task of <see cref="SearchResult"/></returns>
        Task<SearchResult> Search(string query, string type, string market = null);

        /// <summary>
        /// Get Spotify Catalog information about artists, albums, tracks or playlists that match a
        /// keyword string.
        /// </summary>
        /// <param name="query">Search query keywords and optional field filters and operators. For 
        /// example: `q=roadhouse%20blues`. See also https://developer.spotify.com/documentation/web-api/reference/search/search/#writing-a-query---guidelines </param>
        /// <param name="type">Specify multiple <see cref="SpotifySearchTypes"/> to search across.</param>
        /// <param name="market">Optional. Choose a <see cref="SpotifyCountryCodes"/>. If a country code
        /// is specified, only artists, albums, and tracks with content that is playable in that market 
        /// is returned. Note: Playlist results are not affected by the market parameter.</param>
        /// <returns>Task of <see cref="SearchResult"/></returns>
        Task<SearchResult> Search(string query, string[] types, string market = null);

        /// <summary>
        /// Get Spotify Catalog information about artists, albums, tracks or playlists that match a
        /// keyword string.
        /// </summary>
        /// <param name="query">Search query keywords and optional field filters and operators. For 
        /// example: `q=roadhouse%20blues`. See also https://developer.spotify.com/documentation/web-api/reference/search/search/#writing-a-query---guidelines </param>
        /// <param name="type">Specify multiple <see cref="SpotifySearchTypes"/> to search across.</param>
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
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        Task<T> Search<T>(string query, string[] types, string market, (int limit, int offset) limitOffset);

    }
}