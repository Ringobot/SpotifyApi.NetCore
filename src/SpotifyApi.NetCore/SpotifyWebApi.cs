using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        /// Invoke a GET request and deserialise the JSON to a model of T
        /// </summary>
        /// <param name="url">The URL to GET</param>
        /// <typeparam name="T">The type to deserialise to</typeparam>
        protected internal virtual async Task<T> GetModel<T>(string url, string accessToken = null)
        {
            return JsonConvert.DeserializeObject<T>
            (
                await _http.Get
                (
                    url,
                    new AuthenticationHeaderValue("Bearer", accessToken ?? (await _accounts.GetAppAccessToken()).AccessToken)
                )
            );
        }

        /// <summary>
        /// Invoke a GET request and deserialise the result to JSON from a root property of the Spotify Response
        /// </summary>
        /// <param name="url">The URL to GET</param>
        /// <param name="rootPropertyName">The name of the root property of the JSON response to deserialise, e.g. "artists"</param>
        /// <typeparam name="T">The type to deserialise to</typeparam>
        /// <returns></returns>
        protected internal virtual async Task<T> GetModelFromProperty<T>(string url, string rootPropertyName, string accessToken = null)
        {
            var deserialized = JsonConvert.DeserializeObject
            (
                await _http.Get
                (
                    url,
                    new AuthenticationHeaderValue("Bearer", accessToken ?? (await _accounts.GetAppAccessToken()).AccessToken)
                )
            ) as JObject;
            return deserialized[rootPropertyName].ToObject<T>();
        }
    }
}
