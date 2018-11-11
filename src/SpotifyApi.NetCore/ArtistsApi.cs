using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpotifyApi.NetCore.Http;

namespace SpotifyApi.NetCore
{
    public class ArtistsApi : SpotifyWebApi, IArtistsApi
    {
        protected internal virtual ISearchApi SearchApi { get; set; }

        public ArtistsApi(HttpClient httpClient, IAccountsService accountsService) : base(httpClient, accountsService)
        {
            SearchApi = new SearchApi(httpClient, accountsService);
        }

        #region GetArtist

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
        public async Task<T> GetArtist<T>(string artistId) => await GetModel<T>($"{BaseUrl}/artists/{artistId}");

        #endregion

        #region GetRelatedArtists

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
        public async Task<T> GetRelatedArtists<T>(string artistId) => await GetModel<T>($"{BaseUrl}/artists/{artistId}/related-artists");

        #endregion

        #region SearchArtists

        /// <summary>
        /// Get Spotify Catalog information about artists that match a keyword string.
        /// </summary>
        /// <param name="artist">Artist search keyword(s). Wildcards accepted. See
        /// https://developer.spotify.com/documentation/web-api/reference/search/search/#writing-a-query---guidelines
        /// for more info.
        /// </param>
        /// <returns>Task of <see cref="SearchResult">SearchResult</see></returns>
        public async Task<SearchResult> SearchArtists(string artist)
            => await SearchApi.Search(artist, SpotifySearchTypes.Artist , null, (0, 0));

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
            => await SearchApi.Search(artist, new string[] { SpotifySearchTypes.Artist }, null, (limit, 0));

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
            => await SearchApi.Search(artist, new string[] { SpotifySearchTypes.Artist }, null, limitOffset);

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
        [Obsolete("Is replaced by SearchApi.Search<T>(). Will be deprecated in next major version")]
        public async Task<T> SearchArtists<T>(string artist, (int limit, int offset) limitOffset)
            => await SearchApi.Search<T>(artist, new string[] { SpotifySearchTypes.Artist }, null, limitOffset);

        #endregion

        #region GetArtists

        /// <summary>
        /// Get Spotify catalog information for several artists based on their Spotify IDs.
        /// </summary>
        /// <param name="artistIds">The Spotify IDs for the artists. Maximum: 50 IDs.</param>
        /// <returns>Task of Artist[]</returns>
        public async Task<Artist[]> GetArtists(string[] artistIds) => await GetArtists<Artist[]>(artistIds);

        /// <summary>
        /// Get Spotify catalog information for several artists based on their Spotify IDs.
        /// </summary>
        /// <param name="artistIds">The Spotify IDs for the artists. Maximum: 50 IDs.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise the `artists` property
        /// of Spotify's response to. Should be an array like `Artist[]`.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        public async Task<T> GetArtists<T>(string[] artistIds)
        {
            if (artistIds == null || artistIds.Length == 0 || string.IsNullOrEmpty(artistIds[0]))
            {
                throw new ArgumentNullException("artistIds");
            }

            return await GetModelFromProperty<T>($"{BaseUrl}/artists?ids={string.Join(",", artistIds)}", "artists");
        }

        #endregion

        #region GetArtistsTopTracks

        /// <summary>
        /// Get Spotify catalog information about an artist’s top tracks by country.
        /// </summary>
        /// <param name="artistId">The Spotify ID for the artist.</param>
        /// <param name="market">Required. An ISO 3166-1 alpha-2 country code (<see cref="SpotifyCountryCodes"/>)
        /// or the string `from_token`.</param>
        /// <returns>Task of Track[]</returns>
        public async Task<Track[]> GetArtistsTopTracks(string artistId, string market) => await GetArtistsTopTracks<Track[]>(artistId, market);

        /// <summary>
        /// Get Spotify catalog information about an artist’s top tracks by country.
        /// </summary>
        /// <param name="artistId">The Spotify ID for the artist.</param>
        /// <param name="market">Required. An ISO 3166-1 alpha-2 country code (<see cref="SpotifyCountryCodes"/>)
        /// or the string `from_token`.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        public async Task<T> GetArtistsTopTracks<T>(string artistId, string market)
        {
            if (string.IsNullOrWhiteSpace(artistId)) throw new ArgumentNullException("artistId");
            if (string.IsNullOrWhiteSpace(market)) throw new ArgumentNullException("market");

            return await GetModelFromProperty<T>($"{BaseUrl}/artists/{artistId}/top-tracks?country={market}", "tracks");
        }

        #endregion

    }
}
