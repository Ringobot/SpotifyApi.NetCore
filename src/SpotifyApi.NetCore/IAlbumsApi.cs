using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    public interface IAlbumsApi
    {
        #region GetAlbum

        /// <summary>
        /// Get Spotify catalog information for a single album.
        /// </summary>
        /// <param name="albumId">The Spotify ID for the album.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="Album"/> object.</returns>
        Task<Album> GetAlbum(string albumId);

        /// <summary>
        /// Get Spotify catalog information for a single album.
        /// </summary>
        /// <param name="albumId">The Spotify ID for the album.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string `from_token` 
        /// (See <see cref="SpotifyCountryCodes"/>). Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="Album"/> object.</returns>
        Task<Album> GetAlbum(string albumId, string market);
        
        /// <summary>
        /// Get Spotify catalog information for a single album.
        /// </summary>
        /// <param name="albumId">The Spotify ID for the album.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string `from_token` 
        /// (See <see cref="SpotifyCountryCodes"/>). Provide this parameter if you want to apply Track Relinking.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>A Task that, once successfully completed, returns a Model of T.</returns>        
        Task<T> GetAlbum<T>(string albumId, string market);

        #endregion

        #region GetAlbumTracks

        /// <summary>
        /// Get Spotify catalog information about an album’s tracks. Optional parameters can be used to limit the number of tracks returned.
        /// </summary>
        /// <param name="albumId">The Spotify ID for the album.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string `from_token` 
        /// (See <see cref="SpotifyCountryCodes"/>). Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="Album"/> object.</returns>
        Task<Album> GetAlbumTracks(string albumId, string market = null);

        /// <summary>
        /// Get Spotify catalog information about an album’s tracks. Optional parameters can be used to limit the number of tracks returned.
        /// </summary>
        /// <param name="albumId">The Spotify ID for the album.</param>
        /// <param name="limitOffset">A tuple of `limit`: The maximum number of tracks to return.
        /// Default: 20. Minimum: 1. Maximum: 50. `offset`: The index of the first track to return.
        /// Default: 0 (the first object). Use with limit to get the next set of tracks.
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string `from_token` 
        /// (See <see cref="SpotifyCountryCodes"/>). Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="Album"/> object.</returns>
        Task<Album> GetAlbumTracks(string albumId, (int limit, int offset) limitOffset, string market = null);

        /// <summary>
        /// Get Spotify catalog information about an album’s tracks. Optional parameters can be used to limit the number of tracks returned.
        /// </summary>
        /// <param name="albumId">The Spotify ID for the album.</param>
        /// <param name="limitOffset">A tuple of `limit`: The maximum number of tracks to return.
        /// Default: 20. Minimum: 1. Maximum: 50. `offset`: The index of the first track to return.
        /// Default: 0 (the first object). Use with limit to get the next set of tracks.
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string `from_token` 
        /// (See <see cref="SpotifyCountryCodes"/>). Provide this parameter if you want to apply Track Relinking.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>A Task that, once successfully completed, returns a Model of T.</returns>        
        Task<T> GetAlbumTracks<T>(string albumId, (int limit, int offset) limitOffset, string market = null);

        #endregion

        #region GetAlbums
        
        /// <summary>
        /// Get Spotify catalog information for multiple albums identified by their Spotify IDs.
        /// </summary>
        /// <param name="albumIds">An array of the Spotify IDs for the albums. Maximum: 20 IDs.</param>
        /// <returns>A Task that, once successfully completed, returns an array of full <see cref="Album"/> objects.</returns>
        Task<Album[]> GetAlbums(string[] albumIds);

        /// <summary>
        /// Get Spotify catalog information for multiple albums identified by their Spotify IDs.
        /// </summary>
        /// <param name="albumIds">An array of the Spotify IDs for the albums. Maximum: 20 IDs.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string `from_token` 
        /// (See <see cref="SpotifyCountryCodes"/>). Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns>A Task that, once successfully completed, returns an array of full <see cref="Album"/> objects.</returns>
        Task<Album[]> GetAlbums(string[] albumIds, string market);
        
        /// <summary>
        /// Get Spotify catalog information for multiple albums identified by their Spotify IDs.
        /// </summary>
        /// <param name="albumIds">An array of the Spotify IDs for the albums. Maximum: 20 IDs.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string `from_token` 
        /// (See <see cref="SpotifyCountryCodes"/>). Provide this parameter if you want to apply Track Relinking.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>A Task that, once successfully completed, returns a Model of T.</returns>
        Task<T> GetAlbums<T>(string[] albumIds, string market);

        #endregion

        #region SearchAlbums

        /// <summary>
        /// Get Spotify Catalog information about albums that match a keyword string.
        /// </summary>
        /// <param name="query">Search query keywords and optional field filters and operators. See
        /// https://developer.spotify.com/documentation/web-api/reference/search/search/#writing-a-query---guidelines</param>
        /// <returns>Task of <see cref="AlbumsSearchResult" /></returns>
        Task<AlbumsSearchResult> SearchAlbums(string query);

        /// <summary>
        /// Get Spotify Catalog information about albums that match a keyword string.
        /// </summary>
        /// <param name="query">Search query keywords and optional field filters and operators. See
        /// https://developer.spotify.com/documentation/web-api/reference/search/search/#writing-a-query---guidelines</param>
        /// <param name="market">Optional. Choose a <see cref="SpotifyCountryCodes"/>. If a country code
        /// is specified, only tracks with content that is playable in that market is returned. </param>
        /// <returns>Task of <see cref="AlbumsSearchResult" /></returns>
        Task<AlbumsSearchResult> SearchAlbums(string query, string market);

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
        /// <returns>Task of <see cref="AlbumsSearchResult" /></returns>
        Task<AlbumsSearchResult> SearchTracks(string query, string market, (int limit, int offset) limitOffset);

        #endregion

    }
}