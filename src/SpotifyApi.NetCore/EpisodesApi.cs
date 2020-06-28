using SpotifyApi.NetCore.Authorization;
using SpotifyApi.NetCore.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Endpoints for retrieving information about one or more episodes from the Spotify catalog.
    /// </summary>
    public class EpisodesApi : SpotifyWebApi, IEpisodesApi
    {
        #region constructors
        public EpisodesApi(HttpClient httpClient, IAccessTokenProvider accessTokenProvider) : base(httpClient, accessTokenProvider)
        {
        }

        public EpisodesApi(HttpClient httpClient, string accessToken) : base(httpClient, accessToken)
        {
        }

        public EpisodesApi(HttpClient httpClient) : base(httpClient)
        {
        }
        #endregion

        #region GetEpisode
        /// <summary>
        /// Get an Episode
        /// </summary>
        /// <param name="episodeId">The Spotify ID for the episode.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code. If a country code is specified, only shows and episodes that are available in that market will be returned. If a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter. Note: If neither market or user country are provided, the content is considered unavailable for the client. Users can view the country that is associated with their account in the account settings.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="Episode"/> object.</returns>
        /// <remarks>https://developer.spotify.com/documentation/web-api/reference/episodes/get-an-episode/</remarks>
        public async Task<Episode> GetEpisode(string episodeId, string market = null, string accessToken = null)
            => await GetEpisode<Episode>(episodeId, market, accessToken: accessToken);

        /// <summary>
        /// Get an Episode
        /// </summary>
        /// <param name="episodeId">The Spotify ID for the episode.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code. If a country code is specified, only shows and episodes that are available in that market will be returned. If a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter. Note: If neither market or user country are provided, the content is considered unavailable for the client. Users can view the country that is associated with their account in the account settings.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="Episode"/> object.</returns>
        /// <remarks>https://developer.spotify.com/documentation/web-api/reference/episodes/get-an-episode/</remarks>
        public async Task<T> GetEpisode<T>(string episodeId, string market = null, string accessToken = null)
        {
            UriBuilder builder = new UriBuilder($"{BaseUrl}/episodes/{episodeId}");
            if (!string.IsNullOrWhiteSpace(market))
            {
                builder.AppendToQuery("market", market);
            }
            return await GetModel<T>(builder.Uri, accessToken);
        }
        #endregion

        #region GetSeveralEpisodes
        /// <summary>
        /// Get Several Episodes
        /// </summary>
        /// <param name="episodeIds">Required. A comma-separated list of the episode Spotify IDs. A maximum of 50 episode IDs can be sent in one request. A minimum of 1 user id is required.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code. If a country code is specified, only shows and episodes that are available in that market will be returned. If a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter. Note: If neither market or user country are provided, the content is considered unavailable for the client. Users can view the country that is associated with their account in the account settings.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedEpisodes"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/episodes/get-several-episodes/
        /// </remarks>
        public async Task<PagedEpisodes> GetSeveralEpisodes(
            string[] episodeIds,
            string market = null,
            string accessToken = null
            ) => await GetSeveralEpisodes<PagedEpisodes>(episodeIds, market, accessToken);

        /// <summary>
        /// Get Several Episodes
        /// </summary>
        /// <param name="episodeIds">Required. A comma-separated list of the episode Spotify IDs. A maximum of 50 episode IDs can be sent in one request. A minimum of 1 user id is required.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code. If a country code is specified, only shows and episodes that are available in that market will be returned. If a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter. Note: If neither market or user country are provided, the content is considered unavailable for the client. Users can view the country that is associated with their account in the account settings.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedEpisodes"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/episodes/get-several-episodes/
        /// </remarks>
        public async Task<T> GetSeveralEpisodes<T>(
            string[] episodeIds,
            string market = null,
            string accessToken = null
            )
        {
            if (episodeIds?.Length < 1 || episodeIds?.Length > 50) throw new
                    ArgumentException("A minimum of 1 and a maximum of 50 episode ids can be sent.");

            UriBuilder builder = new UriBuilder($"{BaseUrl}/episodes");
            builder.AppendToQueryAsCsv("ids", episodeIds);
            if (!string.IsNullOrWhiteSpace(market))
            {
                builder.AppendToQuery("market", market);
            }
            return await GetModel<T>(builder.Uri, accessToken);
        }
        #endregion
    }
}
