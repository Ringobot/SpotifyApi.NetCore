using SpotifyApi.NetCore.Authorization;
using SpotifyApi.NetCore.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// An API Wrapper for the Spotify Web API Follow endpoints.
    /// </summary>
    public class ShowsApi : SpotifyWebApi, IShowsApi
    {
        #region constructors
        public ShowsApi(HttpClient httpClient, IAccessTokenProvider accessTokenProvider) : base(httpClient, accessTokenProvider)
        {
        }

        public ShowsApi(HttpClient httpClient, string accessToken) : base(httpClient, accessToken)
        {
        }

        public ShowsApi(HttpClient httpClient) : base(httpClient)
        {
        }
        #endregion

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
        public Task<Show> GetShow(
            string showId,
            string market = null,
            string accessToken = null
            ) => GetShow<Show>(showId, market, accessToken);

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
        public async Task<T> GetShow<T>(
            string showId,
            string market = null,
            string accessToken = null
            )
        {
            if (string.IsNullOrWhiteSpace(showId)) throw new
                     ArgumentException("A valid show Spotify ID has to be supplied.");

            var builder = new UriBuilder($"{BaseUrl}/shows/{showId}");
            builder.AppendToQueryIfValueNotNullOrWhiteSpace("market", market);
            return await GetModel<T>(builder.Uri, accessToken);
        }
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
        public Task<PagedShows> GetSeveralShows(
            string[] showIds,
            string market = null,
            string accessToken = null
            ) => GetSeveralShows<PagedShows>(showIds, market, accessToken);

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
        public async Task<T> GetSeveralShows<T>(
            string[] showIds,
            string market = null,
            string accessToken = null
            )
        {
            if (showIds?.Length < 1 || showIds?.Length > 50) throw new
                    ArgumentException("A minimum of 1 and a maximum of 50 show ids can be sent.");

            UriBuilder builder = new UriBuilder($"{BaseUrl}/shows");
            builder.AppendToQueryAsCsv("ids", showIds);
            builder.AppendToQueryIfValueNotNullOrWhiteSpace("market", market);
            return await GetModel<T>(builder.Uri, accessToken);
        }
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
        public Task<PagedEpisodes> GetShowEpisodes(
            string showId,
            int limit = 20,
            int offset = 0,
            string market = null,
            string accessToken = null
            ) => GetShowEpisodes<PagedEpisodes>(showId, limit, offset, market, accessToken);

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
        public async Task<T> GetShowEpisodes<T>(
            string showId,
            int limit = 20,
            int offset = 0,
            string market = null,
            string accessToken = null
            )
        {
            if (string.IsNullOrWhiteSpace(showId)) throw new
                     ArgumentException("A valid show Spotify ID has to be supplied.");
            if (limit < 1 || limit > 50) throw new
                ArgumentException("A minimum of 1 and a maximum of 50 episodes ids can be returned.");
            if (offset < 0) throw new
                ArgumentException("The offset must be an integer value greater than 0.");

            UriBuilder builder = new UriBuilder($"{BaseUrl}/shows/{showId}/episodes");
            builder.AppendToQuery("limit", limit);
            builder.AppendToQuery("offset", offset);
            builder.AppendToQueryIfValueNotNullOrWhiteSpace("market", market);
            return await GetModel<T>(builder.Uri, accessToken);
        }
        #endregion
    }
}
