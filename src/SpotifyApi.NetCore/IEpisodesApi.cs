using SpotifyApi.NetCore.Models;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Defines an interface for a wrapper for the Spotify Web Episodes API.
    /// </summary>
    public interface IEpisodesApi
    {
        #region GetEpisode
        /// <summary>
        /// Get Spotify catalog information for a single episode identified by its unique Spotify ID.
        /// </summary>
        /// <param name="episodeId">The Spotify ID for the episode.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string from_token <see cref="SpotifyCountryCodes" />. If a country code is specified, only shows and episodes that are available in that market will be returned. If a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter. Note: If neither market or user country are provided, the content is considered unavailable for the client. Users can view the country that is associated with their account in the account settings.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="Episode"/> object.</returns>
        /// <remarks>https://developer.spotify.com/documentation/web-api/reference/episodes/get-an-episode/</remarks>
        Task<Episode> GetEpisode(string episodeId, string market = null, string accessToken = null);

        /// <summary>
        /// <summary>
        /// Get Spotify catalog information for a single episode identified by its unique Spotify ID.
        /// </summary>
        /// <param name="episodeId">The Spotify ID for the episode.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string from_token <see cref="SpotifyCountryCodes" />. If a country code is specified, only shows and episodes that are available in that market will be returned. If a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter. Note: If neither market or user country are provided, the content is considered unavailable for the client. Users can view the country that is associated with their account in the account settings.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="Episode"/> object.</returns>
        /// <remarks>https://developer.spotify.com/documentation/web-api/reference/episodes/get-an-episode/</remarks>
        Task<T> GetEpisode<T>(string episodeId, string market = null, string accessToken = null);
        #endregion

        #region GetSeveralEpisodes
        /// <summary>
        /// Get Spotify catalog information for multiple episodes based on their Spotify IDs.
        /// </summary>
        /// <param name="episodeIds">Required. A comma-separated list of the episode Spotify IDs. A maximum of 50 episode IDs can be sent in one request. A minimum of 1 user id is required.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string from_token <see cref="SpotifyCountryCodes" />. If a country code is specified, only shows and episodes that are available in that market will be returned. If a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter. Note: If neither market or user country are provided, the content is considered unavailable for the client. Users can view the country that is associated with their account in the account settings.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedEpisodes"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/episodes/get-several-episodes/
        /// </remarks>
        Task<PagedEpisodes> GetSeveralEpisodes(
            string[] episodeIds,
            string market = null,
            string accessToken = null
            );

        /// <summary>
        /// Get Spotify catalog information for multiple episodes based on their Spotify IDs.
        /// </summary>
        /// <param name="episodeIds">Required. A comma-separated list of the episode Spotify IDs. A maximum of 50 episode IDs can be sent in one request. A minimum of 1 user id is required.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 country code or the string from_token <see cref="SpotifyCountryCodes" />. If a country code is specified, only shows and episodes that are available in that market will be returned. If a valid user access token is specified in the request header, the country associated with the user account will take priority over this parameter. Note: If neither market or user country are provided, the content is considered unavailable for the client. Users can view the country that is associated with their account in the account settings.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PagedEpisodes"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/episodes/get-several-episodes/
        /// </remarks>
        Task<T> GetSeveralEpisodes<T>(
            string[] episodeIds,
            string market = null,
            string accessToken = null
            );
        #endregion
    }
}
