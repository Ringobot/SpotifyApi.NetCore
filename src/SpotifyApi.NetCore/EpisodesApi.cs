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
    }
}
