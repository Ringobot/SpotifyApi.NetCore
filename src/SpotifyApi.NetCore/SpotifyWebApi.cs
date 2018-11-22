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
            _http = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _accounts = accountsService ?? throw new ArgumentNullException(nameof(accountsService));
        }

        public SpotifyWebApi(HttpClient httpClient, string bearerToken)
        {
            if (string.IsNullOrEmpty( bearerToken )) throw new ArgumentNullException(nameof(bearerToken));

            _http = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _accounts = new Authorization.SimpleAccountsService(bearerToken);
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
