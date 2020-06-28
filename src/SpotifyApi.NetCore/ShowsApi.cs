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
    }
}
