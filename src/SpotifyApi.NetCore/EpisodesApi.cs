using SpotifyApi.NetCore.Authorization;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
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
        /// <returns>A Task that, once successfully completed, returns a full <see cref="Album"/> object.</returns>
        /// <remarks>https://developer.spotify.com/documentation/web-api/reference/episodes/get-an-episode/</remarks>
        public async Task<object> GetEpisode(string episodeId, string accessToken = null)
            => await GetEpisode<object>(episodeId, accessToken: accessToken);

        public async Task<T> GetEpisode<T>(string episodeId, string accessToken = null)
        {
            UriBuilder builder = new UriBuilder($"{BaseUrl}/episodes/{episodeId}");
            return await GetModel<T>(builder.Uri, accessToken);
        }
        #endregion
    }
}
