using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpotifyApi.NetCore.Http;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Base class helper for Spotify Web API service classes.
    /// </summary>
    public abstract class SpotifyWebApi 
    {
        protected internal const string BaseUrl = "https://api.spotify.com/v1";
        protected internal readonly HttpClient _http;
        protected internal readonly IAuthorizationApi _auth;

        public SpotifyWebApi(HttpClient httpClient, IAuthorizationApi authorizationApi)
        {
            if (httpClient == null) throw new ArgumentNullException("httpClient");
            if (authorizationApi == null) throw new ArgumentNullException("authorizationApi");

            _http = httpClient;
            _auth = authorizationApi;
        }

        protected async Task<T> Get<T>(string url){
            return JsonConvert.DeserializeObject<T>(
                await _http.Get(url,
                    new AuthenticationHeaderValue("Bearer", await _auth.GetAccessToken()))
                );
        }
    }
}
