using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpotifyApi.NetCore.Authorization;
using SpotifyApi.NetCore.Http;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Base class helper for Spotify Web API service classes.
    /// </summary>
    public abstract class SpotifyWebApi
    {
        protected internal const string BaseUrl = "https://api.spotify.com/v1";
        protected internal readonly HttpClient _http;
        protected readonly IAccessTokenProvider _tokenProvider;
        protected readonly string _accessToken;

        #region Constructors

        /// <summary>
        /// This constructor accepts an <see cref="IAccessTokenProvider"/> that will be used to provide a 
        /// Spotify access token for all calls to the API (except when an accessToken is provided using the
        /// optional `accessToken` parameter on each method).
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/></param>
        /// <param name="accessTokenProvider">An instance of an <see cref="IAccessTokenProvider"/></param>
        public SpotifyWebApi(HttpClient httpClient, IAccessTokenProvider accessTokenProvider) : this(httpClient)
        {
            _tokenProvider = accessTokenProvider ?? throw new ArgumentNullException(nameof(accessTokenProvider));
        }

        /// <summary>
        /// This constructor accepts a Spotify access token that will be used for all calls to the API 
        /// (except when an accessToken is provided using the optional `accessToken` parameter on each method).
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/></param>
        /// <param name="accessToken">A valid access token from the Spotify Accounts service</param>
        public SpotifyWebApi(HttpClient httpClient, string accessToken) : this(httpClient)
        {
            if (string.IsNullOrEmpty(accessToken)) throw new ArgumentNullException(nameof(accessToken));
            _accessToken = accessToken;
        }

        /// <summary>
        /// Use this constructor when an accessToken will be provided using the `accessToken` parameter 
        /// on each method
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/></param>
        public SpotifyWebApi(HttpClient httpClient)
        {
            _http = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        #endregion

        protected internal async Task<string> GetAccessToken(string accessToken = null)
        {
            // accessToken or _accessToken or from _tokenProvider or from _accounts
            string token = accessToken
                ?? _accessToken
                ?? (_tokenProvider != null
                    ? (await _tokenProvider.GetAccessToken())
                    : null);

            if (string.IsNullOrEmpty(token)) throw new InvalidOperationException("Access Token is null. Could not get Access Token.");
            return token;
        }

        /// <summary>
        /// Invoke a GET request and deserialise the JSON to a model of T
        /// </summary>
        /// <param name="url">The URL to GET</param>
        /// <typeparam name="T">The type to deserialise to</typeparam>
        [Obsolete("Use UriBuilder to construct Uri using AppendToQuery extensions")]
        protected internal virtual Task<T> GetModel<T>(string url, string accessToken = null)
            => GetModel<T>(new Uri(url), accessToken: accessToken);

        /// <summary>
        /// Invoke a GET request and deserialise the JSON to a model of T
        /// </summary>
        /// <param name="url">The URL to GET</param>
        /// <typeparam name="T">The type to deserialise to</typeparam>
        protected internal virtual async Task<T> GetModel<T>(Uri uri, string accessToken = null)
        {
            return JsonConvert.DeserializeObject<T>
            (
                await _http.Get
                (
                    uri,
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
        [Obsolete("Use UriBuilder to construct Uri using AppendToQuery extensions")]
        protected internal virtual Task<T> GetModelFromProperty<T>(
            string url,
            string rootPropertyName,
            string accessToken = null)
                => GetModelFromProperty<T>(new Uri(url), rootPropertyName, accessToken: accessToken);


        /// <summary>
        /// Invoke a GET request and deserialise the result to JSON from a root property of the Spotify Response
        /// </summary>
        /// <param name="url">The URL to GET</param>
        /// <param name="rootPropertyName">The name of the root property of the JSON response to deserialise, e.g. "artists"</param>
        /// <typeparam name="T">The type to deserialise to</typeparam>
        /// <returns></returns>
        protected internal virtual async Task<T> GetModelFromProperty<T>(
            Uri uri,
            string rootPropertyName,
            string accessToken = null)
        {
            string json = await _http.Get
            (
                uri,
                new AuthenticationHeaderValue("Bearer", accessToken ?? (await GetAccessToken()))
            );
            var deserialized = JsonConvert.DeserializeObject(json) as JObject;
            if (deserialized == null) throw new InvalidOperationException($"Failed to deserialize response as JSON. Response = {json.Substring(0, Math.Min(json.Length, 256))}");
            return deserialized[rootPropertyName].ToObject<T>();
        }

        /// <summary>
        /// Helper to PUT an object as JSON body
        /// </summary>
        [Obsolete("Use UriBuilder to construct Uri using AppendToQuery extensions")]
        protected internal virtual async Task<HttpResponseMessage> Put(string url, object data, string accessToken = null)
            => await PostOrPut("PUT", new Uri(url), data, accessToken);

        /// <summary>
        /// Helper to POST an object as JSON body
        /// </summary>
        [Obsolete("Use UriBuilder to construct Uri using AppendToQuery extensions")]
        protected internal virtual async Task<HttpResponseMessage> Post(string url, object data, string accessToken = null)
            => await PostOrPut("POST", new Uri(url), data, accessToken);

        /// <summary>
        /// Helper to PUT an object as JSON body
        /// </summary>
        protected internal virtual async Task<HttpResponseMessage> Put(Uri uri, object data, string accessToken = null)
            => await PostOrPut("PUT", uri, data, accessToken);

        /// <summary>
        /// Helper to POST an object as JSON body
        /// </summary>
        protected internal virtual async Task<HttpResponseMessage> Post(Uri uri, object data, string accessToken = null)
            => await PostOrPut("POST", uri, data, accessToken);

        /// <summary>
        /// Helper to DELETE an object
        /// </summary>
        protected internal virtual async Task<HttpResponseMessage> Delete(Uri uri, string accessToken = null)
        {
            Logger.Debug($"DELETE {uri}. Token = {accessToken?.ToString()?.Substring(0, 4)}...", nameof(SpotifyWebApi));

            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", 
                accessToken ?? (await GetAccessToken()));

            var response = await _http.DeleteAsync(uri);

            Logger.Information($"DELETE {uri} {response.StatusCode}", nameof(RestHttpClient));

            await RestHttpClient.CheckForErrors(response);

            return response;
        }

        private async Task<HttpResponseMessage> PostOrPut(string verb, Uri uri, object data, string accessToken = null)
        {
            Logger.Debug($"{verb} {uri}. Token = {accessToken?.ToString()?.Substring(0, 4)}...", nameof(SpotifyWebApi));

            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken ?? (await GetAccessToken()));

            var content = data == null ? new StringContent("null") : new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = null;

            switch (verb)
            {
                case "PUT":
                    response = await _http.PutAsync(uri, content);
                    break;
                case "POST":
                    response = await _http.PostAsync(uri, content);
                    break;
                default:
                    throw new NotSupportedException($"{verb} is not a supported verb");
            }

            Logger.Information($"{verb} {uri} {response.StatusCode}", nameof(RestHttpClient));

            await RestHttpClient.CheckForErrors(response);

            return response;
        }
    }
}