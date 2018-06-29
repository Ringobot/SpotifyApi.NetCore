using System;
using System.Net.Http;
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
            _http = httpClient;
            _auth = authorizationApi;
        }
    }
}
