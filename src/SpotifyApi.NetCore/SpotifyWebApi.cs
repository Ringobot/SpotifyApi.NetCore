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
        [Obsolete("Use _tokenProvider")]
        protected internal readonly IAccountsService _accounts;
        protected readonly IAccessTokenProvider _tokenProvider;
        protected readonly string _accessToken;

        [Obsolete("Use `SpotifyWebApi(HttpClient, IAccessTokenProvider)` instead. Will be removed in vNext")]
        public SpotifyWebApi(HttpClient httpClient, IAccountsService accountsService)
        {
            _http = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _accounts = accountsService ?? throw new ArgumentNullException(nameof(accountsService));
        }

        public SpotifyWebApi(HttpClient httpClient, IAccessTokenProvider accessTokenProvider)
        {
            _http = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _tokenProvider = accessTokenProvider ?? throw new ArgumentNullException(nameof(accessTokenProvider));
        }

        public SpotifyWebApi(HttpClient httpClient, string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken)) throw new ArgumentNullException(nameof(accessToken));

            _http = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _accessToken = accessToken;
        }

        protected internal async Task<string> GetAccessToken(string accessToken = null)
        {
            // accessToken or _accessToken or from _tokenProvider or from _accounts
            return accessToken 
                ?? _accessToken 
                ?? (_tokenProvider != null 
                    ? (await _tokenProvider.GetAccessToken())
                    : (await _accounts.GetAppAccessToken()).AccessToken);
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
                    new AuthenticationHeaderValue("Bearer", accessToken ?? (await GetAccessToken()))
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
        protected internal virtual async Task<T> GetModelFromProperty<T>(string url, string rootPropertyName, 
            string accessToken = null)
        {
            var deserialized = JsonConvert.DeserializeObject
            (
                await _http.Get
                (
                    url,
                    new AuthenticationHeaderValue("Bearer", accessToken ?? (await GetAccessToken()))
                )
            ) as JObject;
            return deserialized[rootPropertyName].ToObject<T>();
        }

        /// <summary>
        /// Helper to PUT an object as JSON body
        /// </summary>
        protected internal virtual async Task<HttpResponseMessage> Put(string url, object data, string accessToken = null)
        {
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken ?? (await GetAccessToken()));
            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _http.PutAsync(url, content);

            await RestHttpClient.CheckForErrors(response);

            return response;
        }

    }
}
