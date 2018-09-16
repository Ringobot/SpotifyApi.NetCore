using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore.Http
{
    /// <summary>
    /// Static helper extensions on HttpClient for communicating with the Spotify Web API.
    /// </summary>
    /// <remarks>See https://developer.spotify.com/web-api/ </remarks>
    public static class RestHttpClient 
    {
        /// <summary>
        /// Makes an HTTP(S) GET request to <seealso cref="requestUrl"/> and returns the result as (awaitable Task of) <seealso cref="string"/>.
        /// </summary>
        /// <param name="requestUrl">The entire request URL.</param>
        /// <returns>An (awaitable Task of) <seealso cref="string"/></returns>
        /// <remarks>Will Authorise using the values of the SpotifyApiClientId and SpotifyApiClientSecret appSettings. See 
        /// https://developer.spotify.com/web-api/authorization-guide/#client-credentials-flow </remarks>
        public static async Task<string> Get(this HttpClient http, string requestUrl)
        {
            return await Get(http, requestUrl, null);
        }

        /// <summary>
        /// Makes an HTTP(S) GET request to <seealso cref="requestUrl"/> and returns the result as (awaitable Task of) <seealso cref="string"/>.
        /// </summary>
        /// <param name="requestUrl">The entire request URL.</param>
        /// <param name="authenticationHeader"></param>
        /// <returns>An (awaitable Task of) <seealso cref="string"/></returns>
        /// <remarks>Will Authorise using the values of the SpotifyApiClientId and SpotifyApiClientSecret appSettings. See 
        /// https://developer.spotify.com/web-api/authorization-guide/#client-credentials-flow </remarks>
        public static async Task<string> Get(this HttpClient http, string requestUrl, AuthenticationHeaderValue authenticationHeader)
        {
            //TODO: Implement if-modified-since support, serving from cache if response = 304

            if (string.IsNullOrEmpty(requestUrl)) throw new ArgumentNullException(requestUrl);

            http.DefaultRequestHeaders.Authorization = authenticationHeader;            
            var response = await http.GetAsync(requestUrl);
            Trace.TraceInformation("Got {0} {1}", requestUrl, response.StatusCode);
            
            await CheckForErrors(response);

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
        public static async Task<string> Post(this HttpClient http, string requestUrl, string formData)
        {
            return await Post(http, requestUrl, formData, null);
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
        public static async Task<string> Post(this HttpClient http, string requestUrl, string formData, AuthenticationHeaderValue headerValue)
        {
            if (string.IsNullOrEmpty(requestUrl)) throw new ArgumentNullException(requestUrl);
            if (string.IsNullOrEmpty(formData)) throw new ArgumentNullException(formData);

            http.DefaultRequestHeaders.Authorization = headerValue;
            var response =
                await
                    http.PostAsync(requestUrl,
                        new StringContent(formData, Encoding.UTF8, "application/x-www-form-urlencoded"));
            Trace.TraceInformation("Posted {0} {1}", requestUrl, response.StatusCode);
            
            await CheckForErrors(response);

            return await response.Content.ReadAsStringAsync();
        }

        internal static async Task CheckForErrors(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var error = await SpotifyApiErrorException.ReadErrorResponse(response);
                if (error != null) throw new SpotifyApiErrorException(response.StatusCode, error);
                response.EnsureSuccessStatusCode(); // not a Spotify API Error so throw HttpResponseMessageException
            }
        }
    }
}