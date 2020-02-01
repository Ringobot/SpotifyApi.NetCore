using SpotifyApi.NetCore.Authorization;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Endpoints for getting playlists and new album releases featured on Spotify’s Browse tab.
    /// </summary>
    public class BrowseApi : SpotifyWebApi, IBrowseApi
    {
        #region Constructors

        /// <summary>
        /// Instantiates a new <see cref="BrowseApi"/>.
        /// </summary>
        /// <remarks>
        /// Use this constructor when an accessToken will be provided using the `accessToken` parameter 
        /// on each method
        /// </remarks>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/></param>
        public BrowseApi(HttpClient httpClient) : base(httpClient)
        {
        }

        /// <summary>
        /// Instantiates a new <see cref="BrowseApi"/>.
        /// </summary>
        /// <remarks>
        /// This constructor accepts a Spotify access token that will be used for all calls to the API 
        /// (except when an accessToken is provided using the optional `accessToken` parameter on each method).
        /// </remarks>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/></param>
        /// <param name="accessToken">A valid access token from the Spotify Accounts service</param>
        public BrowseApi(HttpClient httpClient, string accessToken) : base(httpClient, accessToken)
        {
        }

        /// <summary>
        /// Instantiates a new <see cref="BrowseApi"/>.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/></param>
        /// <param name="accessTokenProvider">An instance of <see cref="IAccessTokenProvider"/>, e.g. <see cref="Authorization.AccountsService"/>.</param>
        public BrowseApi(HttpClient httpClient, IAccessTokenProvider accessTokenProvider) : base(httpClient, accessTokenProvider)
        {
        }

        public async Task<string[]> GetAvailableGenreSeeds()
        {
            string url = $"{BaseUrl}/recommendations/available-genre-seeds";
            return await GetModelFromProperty<string[]>(url, "genres");
        }

        /// <summary>
        /// Get a list of new album releases featured in Spotify (shown, for example, on a Spotify 
        /// player's "Browse" tab).
        /// </summary>
        /// <param name="country">Optional. Choose a <see cref="SpotifyCountryCodes"/>. If a country code
        /// is specified, only artists, albums, and tracks with content that is playable in that market 
        /// is returned. Note: Playlist results are not affected by the market parameter.</param>
        /// <param name="limit">Optional. Maximum number of results to return. Default: 20, Minimum: 1,
        /// Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first result to return. Default: 0 (the
        /// first result). Use with limit to get the next page of search results.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service,
        /// used for this call only. See constructors for more ways to provide access tokens.</param>
        /// <returns>Array of <see cref="Album"/></returns>
        /// <remarks> https://developer.spotify.com/documentation/web-api/reference/browse/get-list-new-releases/ </remarks>
        public Task<Album[]> GetNewReleases(
            string country = null,
            int? limit = null,
            int offset = 0,
            string accessToken = null) => GetNewReleases<Album[]>(country, limit, offset, accessToken);

        /// <summary>
        /// Get a list of new album releases featured in Spotify (shown, for example, on a Spotify 
        /// player's "Browse" tab).
        /// </summary>
        /// <typeparam name="T">Type to deserialise result to.</typeparam>
        /// <param name="country">Optional. Choose a <see cref="SpotifyCountryCodes"/>. If a country code
        /// is specified, only artists, albums, and tracks with content that is playable in that market 
        /// is returned. Note: Playlist results are not affected by the market parameter.</param>
        /// <param name="limit">Optional. Maximum number of results to return. Default: 20, Minimum: 1,
        /// Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first result to return. Default: 0 (the
        /// first result). Use with limit to get the next page of search results.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service,
        /// used for this call only. See constructors for more ways to provide access tokens.</param>
        /// <returns>Result deserialised to `T`.</returns>
        /// <remarks> https://developer.spotify.com/documentation/web-api/reference/browse/get-list-new-releases/ </remarks>
        public async Task<T> GetNewReleases<T>(
            string country = null,
            int? limit = null,
            int offset = 0,
            string accessToken = null)
        {
            string url = $"{BaseUrl}/browse/new-releases?";

            if (!string.IsNullOrWhiteSpace(country))
            {
                url += $"&country={country}";
            }

            if (limit.HasValue && limit.Value > 0)
            {
                url += $"&limit={limit.Value}";
            }

            if (offset > 0)
            {
                url += $"&offset={offset}";
            }

            return await GetModel<T>(url, accessToken);
        }

        #endregion

        /// <summary>
        /// Create a playlist-style listening experience based on seed artists, tracks and genres.
        /// </summary>
        /// <param name="seedArtists">An array of Spotify IDs for seed Artists. Up to 5 seed values 
        /// may be provided in any combination of Artists, Tracks and Genres.</param>
        /// <param name="seedGenres">An array of available seed Genres. Up to 5 seed values 
        /// may be provided in any combination of Artists, Tracks and Genres. <seealso cref="GetAvailableGenreSeeds"/>.</param>
        /// <param name="seedTracks">An array of Spotify IDs for seed Tracks. Up to 5 seed values 
        /// may be provided in any combination of Artists, Tracks and Genres.</param>
        /// <param name="limit">Optional. The target size of the list of recommended tracks. Default:
        /// 20. Minimum: 1. Maximum: 100.</param>
        /// <returns><see cref="RecommendationsResult"/></returns>
        /// <remarks> https://developer.spotify.com/documentation/web-api/reference/browse/get-recommendations/ </remarks>
        public async Task<RecommendationsResult> GetRecommendations(
            string[] seedArtists = null,
            string[] seedGenres = null,
            string[] seedTracks = null,
            int? limit = null)
            => await GetRecommendations<RecommendationsResult>(
                seedArtists: seedArtists,
                seedGenres: seedGenres,
                seedTracks: seedTracks,
                limit: limit);

        /// <summary>
        /// Create a playlist-style listening experience based on seed artists, tracks and genres.
        /// </summary>
        /// <typeparam name="T">Type to deserialise result to.</typeparam>
        /// <param name="seedArtists">An array of Spotify IDs for seed Artists. Up to 5 seed values 
        /// may be provided in any combination of Artists, Tracks and Genres. <seealso cref="GetAvailableGenreSeeds"/>.</param>
        /// <param name="seedGenres">An array of available seed Genres. Up to 5 seed values 
        /// may be provided in any combination of Artists, Tracks and Genres.</param>
        /// <param name="seedTracks">An array of Spotify IDs for seed Tracks. Up to 5 seed values 
        /// may be provided in any combination of Artists, Tracks and Genres.</param>
        /// <param name="limit">Optional. The target size of the list of recommended tracks. Default:
        /// 20. Minimum: 1. Maximum: 100.</param>
        /// <param name="limit"></param>
        /// <returns>Result deserialised to `T`.</returns>
        public async Task<T> GetRecommendations<T>(
            string[] seedArtists = null,
            string[] seedGenres = null,
            string[] seedTracks = null,
            int? limit = null)
        {
            if (seedArtists == null && seedGenres == null && seedTracks == null)
                throw new ArgumentException("At least one of `seedArtists`, `seedGenres` or `seedTracks` must be provided.");

            string url = $"{BaseUrl}/recommendations?";

            if (seedArtists != null && seedArtists.Length > 0) url += $"seed_artists={string.Join(",", seedArtists)}&";
            if (seedGenres != null && seedGenres.Length > 0) url += $"seed_genres={string.Join(",", seedGenres)}&";
            if (seedTracks != null && seedTracks.Length > 0) url += $"seed_tracks={string.Join(",", seedTracks)}&";
            if (limit.HasValue && limit.Value > 0) url += $"limit={limit}";

            return await GetModel<T>(url);
        }
    }
}
