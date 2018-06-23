using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApiDotNetCore.Http
{
    /// <summary>
    /// An HTTP Client for communicating with the Spotify Web API.
    /// </summary>
    /// <remarks>See https://developer.spotify.com/web-api/ </remarks>
    public class RestHttpClient : IHttpClient
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Instantiates a new instance of <see cref="RestHttpClient"/>
        /// </summary>
        public RestHttpClient(HttpClient httpClient)
        {
            if (httpClient == null) throw new ArgumentNullException("httpClient");
            _httpClient = httpClient;
        }

        /// <summary>
        /// Makes an HTTP(S) GET request to <seealso cref="requestUrl"/> and returns the result as (awaitable Task of) <seealso cref="string"/>.
        /// </summary>
        /// <param name="requestUrl">The entire request URL.</param>
        /// <returns>An (awaitable Task of) <seealso cref="string"/></returns>
        /// <remarks>Will Authorise using the values of the SpotifyApiClientId and SpotifyApiClientSecret appSettings. See 
        /// https://developer.spotify.com/web-api/authorization-guide/#client-credentials-flow </remarks>
        public async Task<string> Get(string requestUrl)
        {
            return await Get(requestUrl, null);
        }

        /// <summary>
        /// Makes an HTTP(S) GET request to <seealso cref="requestUrl"/> and returns the result as (awaitable Task of) <seealso cref="string"/>.
        /// </summary>
        /// <param name="requestUrl">The entire request URL.</param>
        /// <param name="authenticationHeader"></param>
        /// <returns>An (awaitable Task of) <seealso cref="string"/></returns>
        /// <remarks>Will Authorise using the values of the SpotifyApiClientId and SpotifyApiClientSecret appSettings. See 
        /// https://developer.spotify.com/web-api/authorization-guide/#client-credentials-flow </remarks>
        public async Task<string> Get(string requestUrl, AuthenticationHeaderValue authenticationHeader)
        {
            //TODO: Implement if-modified-since support, serving from cache if response = 304

            if (string.IsNullOrEmpty(requestUrl)) throw new ArgumentNullException(requestUrl);

            _httpClient.DefaultRequestHeaders.Authorization = authenticationHeader;            
            var response = await _httpClient.GetAsync(requestUrl);
            Trace.TraceInformation("Got {0} {1}", requestUrl, response.StatusCode);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Makes an HTTP(S) POST request to <seealso cref="requestUrl"/> with <seealso cref="formData"/> as the request body. Returns the result as (awaitable Task of) <seealso cref="string"/>.
        /// </summary>
        /// <param name="requestUrl">The entire request URL.</param>
        /// <param name="formData">The URL encoded formData in format "key1=value1&amp;key2=value2"</param>
        /// <returns>An (awaitable Task of) <seealso cref="string"/> </returns>
        /// <remarks>Will Authorise using the values of the SpotifyApiClientId and SpotifyApiClientSecret appSettings. See 
        /// https://developer.spotify.com/web-api/authorization-guide/#client-credentials-flow </remarks>
        public async Task<string> Post(string requestUrl, string formData)
        {
            return await Post(requestUrl, formData, null);
        }

        /// <summary>
        /// Makes an HTTP(S) POST request to <seealso cref="requestUrl"/> with <seealso cref="formData"/> as the request body. Returns the result as (awaitable Task of) <seealso cref="string"/>.
        /// </summary>
        /// <param name="requestUrl">The entire request URL.</param>
        /// <param name="formData">The URL encoded formData in format "key1=value1&amp;key2=value2"</param>
        /// <param name="headerValue">An authentication header for the request.</param>
        /// <returns>An (awaitable Task of) <seealso cref="string"/> </returns>
        /// <remarks>Will Authorise using the values of the SpotifyApiClientId and SpotifyApiClientSecret appSettings. See 
        /// https://developer.spotify.com/web-api/authorization-guide/#client-credentials-flow </remarks>
        public async Task<string> Post(string requestUrl, string formData, AuthenticationHeaderValue headerValue)
        {
            if (string.IsNullOrEmpty(requestUrl)) throw new ArgumentNullException(requestUrl);
            if (string.IsNullOrEmpty(formData)) throw new ArgumentNullException(formData);

            _httpClient.DefaultRequestHeaders.Authorization = headerValue;
            var response =
                await
                    _httpClient.PostAsync(requestUrl,
                        new StringContent(formData, Encoding.UTF8, "application/x-www-form-urlencoded"));
            Trace.TraceInformation("Posted {0} {1}", requestUrl, response.StatusCode);
            response.EnsureSuccessStatusCode();
            
            return await response.Content.ReadAsStringAsync();
        }
    }
}