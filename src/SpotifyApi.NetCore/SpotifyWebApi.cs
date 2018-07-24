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
        protected internal readonly IAccountsService _accounts;

        public SpotifyWebApi(HttpClient httpClient, IAccountsService accountsService)
        {
            if (httpClient == null) throw new ArgumentNullException("httpClient");
            if (accountsService == null) throw new ArgumentNullException("accountsService");

            _http = httpClient;
            _accounts = accountsService;
        }

        /// <summary>
        /// Helper to invoke a GET request and deserialise the result to JSON
        /// </summary>
        protected internal virtual async Task<T> Get<T>(string url)
        {
            return JsonConvert.DeserializeObject<T>(
                await _http.Get(url,
                    new AuthenticationHeaderValue("Bearer", (await _accounts.GetAppAccessToken()).AccessToken))
                );
        }
    }
}
