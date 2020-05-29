using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Defines a wrapper for the Spotify Web Artists API.
    /// </summary>
    public interface IArtistsApi
    {
        #region GetArtist

        /// <summary>
        /// Get Spotify catalog information for a single artist identified by their unique Spotify ID.
        /// </summary>
        /// <param name="artistId">The Spotify ID for the artist.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.</param>
        /// <returns>Task of Artist</returns>
        Task<Artist> GetArtist(string artistId, string accessToken = null);

        /// <summary>
        /// Get Spotify catalog information for a single artist identified by their unique Spotify ID.
        /// </summary>
        /// <param name="artistId">The Spotify ID for the artist.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        Task<T> GetArtist<T>(string artistId, string accessToken = null);

        #endregion

        #region GetRelatedArtists
        /// <summary>
        /// Get Spotify catalog information about artists similar to a given artist. Similarity is 
        /// based on analysis of the Spotify community’s listening history.
        /// </summary>
        /// <param name="artistId">The Spotify ID for the artist.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.</param>
        /// <returns>Task of Artist[]</returns>
        Task<Artist[]> GetRelatedArtists(string artistId, string accessToken = null);

        /// <summary>
        /// Get Spotify catalog information about artists similar to a given artist. Similarity is 
        /// based on analysis of the Spotify community’s listening history.
        /// </summary>
        /// <param name="artistId">The Spotify ID for the artist.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        Task<T> GetRelatedArtists<T>(string artistId, string accessToken = null);

        #endregion

        #region SearchArtists

        /// <summary>
        /// Get Spotify Catalog information about artists that match a keyword string.
        /// </summary>
        /// <param name="artist">Artist search keyword(s). Wildcards accepted. See
        /// https://developer.spotify.com/documentation/web-api/reference/search/search/#writing-a-query---guidelines
        /// for more info.
        /// </param>
        /// <param name="limit">Maximum number of results to return. Default: 20 Minimum: 1 Maximum: 50</param>
        /// <param name="offset">The index of the first result to return. Default: 0 (the first result). 
        /// Maximum offset (including limit): 10,000. Use with limit to get the next page of search results.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.</param>
        /// <returns>Task of <see cref="ArtistsSearchResult">SearchResult</see></returns>
        Task<SearchResult> SearchArtists(string artist, int? limit = null, int offset = 0, string accessToken = null);

        #endregion

        #region GetArtists

        /// <summary>
        /// Get Spotify catalog information for several artists based on their Spotify IDs.
        /// </summary>
        /// <param name="artistIds">The Spotify IDs for the artists. Maximum: 50 IDs.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.</param>
        /// <returns>Task of Artist[]</returns>
        Task<Artist[]> GetArtists(string[] artistIds, string accessToken = null);

        /// <summary>
        /// Get Spotify catalog information for several artists based on their Spotify IDs.
        /// </summary>
        /// <param name="artistIds">The Spotify IDs for the artists. Maximum: 50 IDs.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        Task<T> GetArtists<T>(string[] artistIds, string accessToken = null);

        #endregion

        #region GetArtistsTopTracks

        /// <summary>
        /// Get Spotify catalog information about an artist’s top tracks by country.
        /// </summary>
        /// <param name="artistId">The Spotify ID for the artist.</param>
        /// <param name="market">Required. An ISO 3166-1 alpha-2 country code (<see cref="SpotifyCountryCodes"/>)
        /// or the string `from_token`.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.</param>
        /// <returns>Task of Track[]</returns>
        Task<Track[]> GetArtistsTopTracks(string artistId, string market, string accessToken = null);

        /// <summary>
        /// Get Spotify catalog information about an artist’s top tracks by country.
        /// </summary>
        /// <param name="artistId">The Spotify ID for the artist.</param>
        /// <param name="market">Required. An ISO 3166-1 alpha-2 country code (<see cref="SpotifyCountryCodes"/>)
        /// or the string `from_token`.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        Task<T> GetArtistsTopTracks<T>(string artistId, string market, string accessToken = null);

        #endregion

        #region GetArtistsAlbums

        /// <summary>
        /// Get Spotify catalog information about an artist’s albums.
        /// </summary>
        /// <param name="artistId">The Spotify ID for the artist.</param>
        /// <param name="includeGroups">Optional. An array of keywords (<see cref="SpotifyArtistAlbumGroups"/>) 
        /// that will be used to filter the response. If not supplied, all album types will be returned.</param>
        /// <param name="country">Optional. An ISO 3166-1 alpha-2 country code (<see cref="SpotifyCountryCodes"/>) 
        /// or the string from_token. Supply this parameter to limit the response to one particular 
        /// geographical market. If not given, results will be returned for all countries and you are 
        /// likely to get duplicate results per album, one for each country in which the album is available!</param>
        /// <param name="limit">Optional. Maximum number of results to return. Default: 20, Minimum: 1,
        /// Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first album to return. Default: 0 (the
        /// first result). Use with <paramref name="limit"/> to get the next page of search results.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.</param>
        /// <returns>Task of Album[]</returns>
        /// <remarks> https://developer.spotify.com/documentation/web-api/reference/artists/get-artists-albums/ </remarks>
        Task<PagedAlbums> GetArtistsAlbums(
            string artistId, 
            string[] includeGroups = null, 
            string country = null,
            int? limit = null,
            int offset = 0,
            string accessToken = null);

        /// <summary>
        /// Get Spotify catalog information about an artist’s albums.
        /// </summary>
        /// <param name="artistId">The Spotify ID for the artist.</param>
        /// <param name="includeGroups">Optional. An array of keywords (<see cref="SpotifyArtistAlbumGroups"/>) 
        /// that will be used to filter the response. If not supplied, all album types will be returned.</param>
        /// <param name="country">Optional. An ISO 3166-1 alpha-2 country code (<see cref="SpotifyCountryCodes"/>) 
        /// or the string from_token. Supply this parameter to limit the response to one particular 
        /// geographical market. If not given, results will be returned for all countries and you are 
        /// likely to get duplicate results per album, one for each country in which the album is available!</param>
        /// <param name="limit">Optional. Maximum number of results to return. Default: 20, Minimum: 1,
        /// Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first album to return. Default: 0 (the
        /// first result). Use with <paramref name="limit"/> to get the next page of search results.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        /// <remarks> https://developer.spotify.com/documentation/web-api/reference/artists/get-artists-albums/ </remarks>
        Task<T> GetArtistsAlbums<T>(
            string artistId,
            string[] includeGroups = null,
            string country = null,
            int? limit = null,
            int offset = 0,
            string accessToken = null);

        #endregion
    }
}
