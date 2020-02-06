using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Endpoints for getting playlists and new album releases featured on Spotify’s Browse tab.
    /// </summary>
    public interface IBrowseApi
    {
        /// <summary>
        /// Retrieve a list of available genres seed parameter values for recommendations.
        /// </summary>
        /// <returns>An array of available genre seeds.</returns>
        Task<string[]> GetAvailableGenreSeeds();

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
        /// <returns><see cref="AlbumsSearchResult"/></returns>
        /// <remarks> https://developer.spotify.com/documentation/web-api/reference/browse/get-list-new-releases/ </remarks>
        Task<AlbumsSearchResult> GetNewReleases(
            string country = null,
            int? limit = null,
            int offset = 0,
            string accessToken = null);

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
        Task<T> GetNewReleases<T>(
            string country = null,
            int? limit = null,
            int offset = 0,
            string accessToken = null);

        /// <summary>
        /// Create a playlist-style listening experience based on seed artists, tracks and genres.
        /// </summary>
        /// <param name="seedArtists">An array of Spotify IDs for seed Artists. Up to 5 seed values 
        /// may be provided in any combination of Artists, Tracks and Genres.</param>
        /// <param name="seedGenres">An array of available seed Genres. Up to 5 seed values 
        /// may be provided in any combination of Artists, Tracks and Genres. <seealso cref="GetAvailableGenreSeeds"/></param>
        /// <param name="seedTracks">An array of Spotify IDs for seed Tracks. Up to 5 seed values 
        /// may be provided in any combination of Artists, Tracks and Genres.</param>
        /// <param name="limit">Optional. The target size of the list of recommended tracks. Default:
        /// 20. Minimum: 1. Maximum: 100.</param>
        /// <returns><see cref="RecommendationsResult"/></returns>
        /// <remarks> https://developer.spotify.com/documentation/web-api/reference/browse/get-recommendations/ </remarks>
        Task<RecommendationsResult> GetRecommendations(
            string[] seedArtists = null,
            string[] seedGenres = null,
            string[] seedTracks = null,
            int? limit = null);

        /// <summary>
        /// Create a playlist-style listening experience based on seed artists, tracks and genres.
        /// </summary>
        /// <typeparam name="T">Type to deserialise result to.</typeparam>
        /// <param name="seedArtists">An array of Spotify IDs for seed Artists. Up to 5 seed values 
        /// may be provided in any combination of Artists, Tracks and Genres.</param>
        /// <param name="seedGenres">An array of available seed Genres. Up to 5 seed values 
        /// may be provided in any combination of Artists, Tracks and Genres. <seealso cref="GetAvailableGenreSeeds"/>.</param>
        /// <param name="seedTracks">An array of Spotify IDs for seed Tracks. Up to 5 seed values 
        /// may be provided in any combination of Artists, Tracks and Genres.</param>
        /// <param name="limit">Optional. The target size of the list of recommended tracks. Default:
        /// 20. Minimum: 1. Maximum: 100.</param>
        /// <param name="limit"></param>
        /// <returns>Result deserialised to `T`.</returns>
        Task<T> GetRecommendations<T>(
            string[] seedArtists = null,
            string[] seedGenres = null,
            string[] seedTracks = null,
            int? limit = null);
    }
}
