using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Defines a wrapper for the Spotify Web Artists API.
    /// </summary>
    public interface IArtistsApi
    {
        /// <summary>
        /// Get Spotify catalog information for a single artist identified by their unique Spotify ID.
        /// </summary>
        /// <param name="artistId">The Spotify ID for the artist.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        Task<T> GetArtist<T>(string artistId);

        /// <summary>
        /// Get Spotify catalog information for a single artist identified by their unique Spotify ID.
        /// </summary>
        /// <param name="artistId">The Spotify ID for the artist.</param>
        /// <returns>Task of Artist</returns>
        Task<Artist> GetArtist(string artistId);

        /// <summary>
        /// Get Spotify catalog information about artists similar to a given artist. Similarity is 
        /// based on analysis of the Spotify community’s listening history.
        /// </summary>
        /// <param name="artistId">The Spotify ID for the artist.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        Task<T> GetRelatedArtists<T>(string artistId);

        /// <summary>
        /// Get Spotify catalog information about artists similar to a given artist. Similarity is 
        /// based on analysis of the Spotify community’s listening history.
        /// </summary>
        /// <param name="artistId">The Spotify ID for the artist.</param>
        /// <returns>Task of Artist[]</returns>
        Task<Artist[]> GetRelatedArtists(string artistId);

        /// <summary>
        /// Get Spotify Catalog information about artists that match a keyword string.
        /// </summary>
        /// <param name="artist">Artist search keyword(s). Wildcards accepted. See
        /// https://developer.spotify.com/documentation/web-api/reference/search/search/#writing-a-query---guidelines
        /// for more info.
        /// </param>
        /// <returns>Task of <see cref="SearchResult">SearchResult</see></returns>
        Task<SearchResult> SearchArtists(string artist);

        /// <summary>
        /// Get Spotify Catalog information about artists that match a keyword string.
        /// </summary>
        /// <param name="artist">Artist search keyword(s). Wildcards accepted. See
        /// https://developer.spotify.com/documentation/web-api/reference/search/search/#writing-a-query---guidelines
        /// for more info.
        /// </param>
        /// <param name="limit">Maximum number of results to return. Default: 20 Minimum: 1 Maximum: 50</param>
        /// <returns>Task of <see cref="SearchResult">SearchResult</see></returns>
        Task<SearchResult> SearchArtists(string artist, int limit);

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
        /// <returns>Task of <see cref="SearchResult">SearchResult</see></returns>
        Task<SearchResult> SearchArtists(string artist, (int limit, int offset) limitOffset);

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
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        Task<T> SearchArtists<T>(string artist, (int limit, int offset) limitOffset);
    }
}
