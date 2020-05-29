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
    /// Endpoints for retrieving information about one or more artists from the Spotify catalog.
    /// </summary>
    public class ArtistsApi : SpotifyWebApi, IArtistsApi
    {
        protected internal virtual ISearchApi SearchApi { get; set; }

        #region Constructors

        /// <summary>
        /// Use this constructor when an accessToken will be provided using the `accessToken` parameter 
        /// on each method
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/></param>
        public ArtistsApi(HttpClient httpClient) : base(httpClient)
        {
            SearchApi = new SearchApi(httpClient);
        }

        /// <summary>
        /// This constructor accepts a Spotify access token that will be used for all calls to the API 
        /// (except when an accessToken is provided using the optional `accessToken` parameter on each method).
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/></param>
        /// <param name="accessToken">A valid access token from the Spotify Accounts service</param>
        public ArtistsApi(HttpClient httpClient, string accessToken) : base(httpClient, accessToken)
        {
            SearchApi = new SearchApi(httpClient, accessToken);
        }

        public ArtistsApi(HttpClient httpClient, IAccessTokenProvider accessTokenProvider) : base(httpClient, accessTokenProvider)
        {
            SearchApi = new SearchApi(httpClient, accessTokenProvider);
        }

        #endregion

        #region GetArtist

        /// <summary>
        /// Get Spotify catalog information for a single artist identified by their unique Spotify ID.
        /// </summary>
        /// <param name="artistId">The Spotify ID for the artist.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service,
        /// used for this call only. See constructors for more ways to provide access tokens.</param>
        /// <returns>Task of Artist</returns>
        public async Task<Artist> GetArtist(string artistId, string accessToken = null)
            => await GetArtist<Artist>(artistId, accessToken);

        /// <summary>
        /// Get Spotify catalog information for a single artist identified by their unique Spotify ID.
        /// </summary>
        /// <param name="artistId">The Spotify ID for the artist.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service,
        /// used for this call only. See constructors for more ways to provide access tokens.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        public async Task<T> GetArtist<T>(string artistId, string accessToken = null)
            => await GetModel<T>($"{BaseUrl}/artists/{SpotifyUriHelper.ArtistId(artistId)}", accessToken);

        #endregion

        #region GetRelatedArtists

        /// <summary>
        /// Get Spotify catalog information about artists similar to a given artist. Similarity is 
        /// based on analysis of the Spotify community’s listening history.
        /// </summary>
        /// <param name="artistId">The Spotify ID for the artist.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service,
        /// used for this call only. See constructors for more ways to provide access tokens.</param>
        /// <returns>Task of Artist[]</returns>
        public async Task<Artist[]> GetRelatedArtists(string artistId, string accessToken = null)
            => await GetRelatedArtists<Artist[]>(artistId, accessToken);

        /// <summary>
        /// Get Spotify catalog information about artists similar to a given artist. Similarity is 
        /// based on analysis of the Spotify community’s listening history.
        /// </summary>
        /// <param name="artistId">The Spotify ID for the artist.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service,
        /// used for this call only. See constructors for more ways to provide access tokens.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        public async Task<T> GetRelatedArtists<T>(string artistId, string accessToken = null)
            => await GetModelFromProperty<T>($"{BaseUrl}/artists/{SpotifyUriHelper.ArtistId(artistId)}/related-artists", "artists", accessToken);

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
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service,
        /// used for this call only. See constructors for more ways to provide access tokens.</param>
        /// <returns>Task of <see cref="SearchResult">SearchResult</see></returns>
        public async Task<SearchResult> SearchArtists(string artist, int? limit = null, int offset = 0, string accessToken = null)
            => await SearchApi.Search(artist, SpotifySearchTypes.Artist, limit: limit, offset: offset, accessToken: accessToken);

        #endregion

        #region GetArtists

        /// <summary>
        /// Get Spotify catalog information for several artists based on their Spotify IDs.
        /// </summary>
        /// <param name="artistIds">The Spotify IDs for the artists. Maximum: 50 IDs.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service,
        /// used for this call only. See constructors for more ways to provide access tokens.</param>
        /// <returns>Task of Artist[]</returns>
        public async Task<Artist[]> GetArtists(string[] artistIds, string accessToken = null)
            => await GetArtists<Artist[]>(artistIds, accessToken);

        /// <summary>
        /// Get Spotify catalog information for several artists based on their Spotify IDs.
        /// </summary>
        /// <param name="artistIds">The Spotify IDs for the artists. Maximum: 50 IDs.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service,
        /// used for this call only. See constructors for more ways to provide access tokens.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise the `artists` property
        /// of Spotify's response to. Should be an array like `Artist[]`.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        public async Task<T> GetArtists<T>(string[] artistIds, string accessToken = null)
        {
            if (artistIds == null || artistIds.Length == 0 || string.IsNullOrEmpty(artistIds[0]))
            {
                throw new ArgumentNullException("artistIds");
            }

            IEnumerable<string> ids = artistIds.Select(SpotifyUriHelper.ArtistId);

            return await GetModelFromProperty<T>($"{BaseUrl}/artists?ids={string.Join(",", ids)}", "artists", accessToken);
        }

        #endregion

        #region GetArtistsTopTracks

        /// <summary>
        /// Get Spotify catalog information about an artist’s top tracks by country.
        /// </summary>
        /// <param name="artistId">The Spotify ID for the artist.</param>
        /// <param name="market">Required. An ISO 3166-1 alpha-2 country code (<see cref="SpotifyCountryCodes"/>)
        /// or the string `from_token`.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service,
        /// used for this call only. See constructors for more ways to provide access tokens.</param>
        /// <returns>Task of Track[]</returns>
        public async Task<Track[]> GetArtistsTopTracks(string artistId, string market, string accessToken = null)
            => await GetArtistsTopTracks<Track[]>(artistId, market, accessToken);

        /// <summary>
        /// Get Spotify catalog information about an artist’s top tracks by country.
        /// </summary>
        /// <param name="artistId">The Spotify ID for the artist.</param>
        /// <param name="market">Required. An ISO 3166-1 alpha-2 country code (<see cref="SpotifyCountryCodes"/>)
        /// or the string `from_token`.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service,
        /// used for this call only. See constructors for more ways to provide access tokens.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        public async Task<T> GetArtistsTopTracks<T>(string artistId, string market, string accessToken = null)
        {
            if (string.IsNullOrWhiteSpace(artistId)) throw new ArgumentNullException("artistId");
            if (string.IsNullOrWhiteSpace(market)) throw new ArgumentNullException("market");

            return await GetModelFromProperty<T>($"{BaseUrl}/artists/{SpotifyUriHelper.ArtistId(artistId)}/top-tracks?country={market}", "tracks", accessToken);
        }

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
        public Task<PagedAlbums> GetArtistsAlbums(
            string artistId,
            string[] includeGroups = null,
            string country = null,
            int? limit = null,
            int offset = 0,
            string accessToken = null) => GetArtistsAlbums<PagedAlbums>(
                artistId,
                includeGroups: includeGroups,
                country: country,
                limit: limit,
                offset: offset,
                accessToken: accessToken);

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
        public async Task<T> GetArtistsAlbums<T>(
            string artistId,
            string[] includeGroups = null,
            string country = null,
            int? limit = null,
            int offset = 0,
            string accessToken = null)
        {
            var builder = new UriBuilder($"{BaseUrl}/artists/{artistId}/albums");
            builder.AppendToQueryAsCsv("include_groups", includeGroups);
            builder.AppendToQueryIfValueNotNullOrWhiteSpace("country", country);
            builder.AppendToQueryIfValueGreaterThan0("limit", limit);
            builder.AppendToQueryIfValueGreaterThan0("offset", offset);
            return await GetModel<T>(builder.Uri, accessToken: accessToken);
        }

        #endregion


    }
}
