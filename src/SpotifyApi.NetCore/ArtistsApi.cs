using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    public class ArtistsApi : SpotifyWebApi, IArtistsApi
    {
        public ArtistsApi(HttpClient httpClient, IAccountsService accountsService) : base(httpClient, accountsService)
        {
        }

        /// <summary>
        /// Get Spotify catalog information for a single artist identified by their unique Spotify ID.
        /// </summary>
        /// <param name="artistId">The Spotify ID for the artist.</param>
        /// <returns>Task of Artist</returns>
        public async Task<Artist> GetArtist(string artistId) => await GetArtist<Artist>(artistId);

        /// <summary>
        /// Get Spotify catalog information for a single artist identified by their unique Spotify ID.
        /// </summary>
        /// <param name="artistId">The Spotify ID for the artist.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        public async Task<T> GetArtist<T>(string artistId) => await Get<T>($"{BaseUrl}/artists/{artistId}");

        /// <summary>
        /// Get Spotify catalog information about artists similar to a given artist. Similarity is 
        /// based on analysis of the Spotify community’s listening history.
        /// </summary>
        /// <param name="artistId">The Spotify ID for the artist.</param>
        /// <returns>Task of Artist[]</returns>
        public async Task<Artist[]> GetRelatedArtists(string artistId) => await GetRelatedArtists<Artist[]>(artistId);

        /// <summary>
        /// Get Spotify catalog information about artists similar to a given artist. Similarity is 
        /// based on analysis of the Spotify community’s listening history.
        /// </summary>
        /// <param name="artistId">The Spotify ID for the artist.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        public async Task<T> GetRelatedArtists<T>(string artistId) => await Get<T>($"{BaseUrl}/artists/{artistId}/related-artists");

        /// <summary>
        /// Get Spotify Catalog information about artists that match a keyword string.
        /// </summary>
        /// <param name="artist">Artist search keyword(s). Wildcards accepted. See
        /// https://developer.spotify.com/documentation/web-api/reference/search/search/#writing-a-query---guidelines
        /// for more info.
        /// </param>
        /// <returns>Task of <see cref="SearchResult">SearchResult</see></returns>
        public async Task<SearchResult> SearchArtists(string artist) => await SearchArtists(artist, (0, 0));
        
        /// <summary>
        /// Get Spotify Catalog information about artists that match a keyword string.
        /// </summary>
        /// <param name="artist">Artist search keyword(s). Wildcards accepted. See
        /// https://developer.spotify.com/documentation/web-api/reference/search/search/#writing-a-query---guidelines
        /// for more info.
        /// </param>
        /// <param name="limit">Maximum number of results to return. Default: 20 Minimum: 1 Maximum: 50</param>
        /// <returns>Task of <see cref="SearchResult">SearchResult</see></returns>
        public async Task<SearchResult> SearchArtists(string artist, int limit)
        {
            return await SearchArtists(artist, (limit, 0));
        }
        
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
        public async Task<SearchResult> SearchArtists(string artist, (int limit, int offset) limitOffset) 
            => await SearchArtists<SearchResult>(artist, limitOffset);

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
        public async Task<T> SearchArtists<T>(string artist, (int limit, int offset) limitOffset)
        {
            string url = $"{BaseUrl}/search?q={Uri.EscapeDataString(artist)}&type=artist";

            if (limitOffset.limit > 0 && limitOffset.limit <= 50)
            {
                url += $"&limit={limitOffset.limit}";
            }

            if (limitOffset.offset > 0 && limitOffset.offset <= 10000)
            {
                url += $"&offset={limitOffset.offset}";
            }

            return await Get<T>(url);
        }
    }
}
