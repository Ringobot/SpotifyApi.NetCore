using SpotifyApi.NetCore.Models;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Defines a wrapper for the Spotify Web Shows API.
    /// </summary>
    public interface IShowsApi
    {
        #region GetShow
        /// <summary>
        /// Get Spotify catalog information for a single show identified by its unique Spotify ID.
        /// </summary>
        /// <param name="showId">Required. The Spotify ID for the show.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string from_token or the string from_token <see cref="SpotifyCountryCodes" />. Provide this parameter if you want to apply Track Relinking.</param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="Show"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/shows/get-a-show/
        /// </remarks>
        Task<Show> GetShow(
            string showId,
            string market = null,
            string accessToken = null
            );

        /// <summary>
        /// Get Spotify catalog information for a single show identified by its unique Spotify ID.
        /// </summary>
        /// <param name="showId">Required. The Spotify ID for the show.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string from_token or the string from_token <see cref="SpotifyCountryCodes" />. Provide this parameter if you want to apply Track Relinking.</param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="Show"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/shows/get-a-show/
        /// </remarks>
        Task<T> GetShow<T>(
            string showId,
            string market = null,
            string accessToken = null
            );
        #endregion

        #region GetSeveralShows
        /// <summary>
        /// Get Spotify catalog information for multiple shows based on their Spotify IDs.
        /// </summary>
        /// <param name="showIds">Required. A comma-separated list of the Spotify IDs for the shows. Minimum: 1 ID. Maximum: 50 IDs.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string from_token or the string from_token <see cref="SpotifyCountryCodes" />. Provide this parameter if you want to apply Track Relinking.</param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedShows"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/shows/get-several-shows/
        /// </remarks>
        Task<PagedShows> GetSeveralShows(
            string[] showIds,
            string market = null,
            string accessToken = null
            );

        /// <summary>
        /// Get Spotify catalog information for multiple shows based on their Spotify IDs.
        /// </summary>
        /// <param name="showIds">Required. A comma-separated list of the Spotify IDs for the shows. Minimum: 1 ID. Maximum: 50 IDs.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string from_token or the string from_token <see cref="SpotifyCountryCodes" />. Provide this parameter if you want to apply Track Relinking.</param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedShows"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/shows/get-several-shows/
        /// </remarks>
        Task<T> GetSeveralShows<T>(
            string[] showIds,
            string market = null,
            string accessToken = null
            );
        #endregion

        #region GetShowEpisodes
        /// <summary>
        /// Get Spotify catalog information about a show’s episodes. Optional parameters can be used to limit the number of episodes returned.
        /// </summary>
        /// <param name="showId">Required. The Spotify ID for the show.</param>
        /// <param name="limit">Optional. The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string from_token or the string from_token <see cref="SpotifyCountryCodes" />. Provide this parameter if you want to apply Track Relinking.</param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedEpisodes"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/shows/get-shows-episodes/
        /// </remarks>
        Task<PagedEpisodes> GetShowEpisodes(
            string showId,
            int limit = 20,
            int offset = 0,
            string market = null,
            string accessToken = null
            );

        /// <summary>
        /// Get Spotify catalog information about a show’s episodes. Optional parameters can be used to limit the number of episodes returned.
        /// </summary>
        /// <param name="showId">Required. The Spotify ID for the show.</param>
        /// <param name="limit">Optional. The maximum number of items to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first object to return. Default: 0 (i.e., the first object). Use with limit to get the next set of objects.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string from_token or the string from_token <see cref="SpotifyCountryCodes" />. Provide this parameter if you want to apply Track Relinking.</param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedEpisodes"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/shows/get-shows-episodes/
        /// </remarks>
        Task<T> GetShowEpisodes<T>(
            string showId,
            int limit = 20,
            int offset = 0,
            string market = null,
            string accessToken = null
            );
        #endregion
    }
}
