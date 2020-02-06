using System;
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
    internal static class RestHttpClient
    {
        /// <summary>
        /// Makes an HTTP(S) GET request to <seealso cref="requestUrl"/> and returns the result as (awaitable Task of) <seealso cref="string"/>.
        /// </summary>
        /// <param name="requestUri">The entire request URL as <see cref="Uri"/>.</param>
        /// <returns>An (awaitable Task of) <seealso cref="string"/></returns>
        /// <remarks>Will Authorise using the values of the SpotifyApiClientId and SpotifyApiClientSecret appSettings. See 
        /// https://developer.spotify.com/web-api/authorization-guide/#client-credentials-flow </remarks>
        public static async Task<string> Get(this HttpClient http, Uri requestUri)
        {
            return await Get(http, requestUri, null);
        }

        /// <summary>
        /// Makes an HTTP(S) GET request to <seealso cref="requestUrl"/> and returns the result as (awaitable Task of) <seealso cref="string"/>.
        /// </summary>
        /// <param name="requestUri">The entire request URL as <see cref="Uri"/>.</param>
        /// <param name="authenticationHeader"></param>
        /// <returns>An (awaitable Task of) <seealso cref="string"/></returns>
        /// <remarks>Will Authorise using the values of the SpotifyApiClientId and SpotifyApiClientSecret appSettings. See 
        /// https://developer.spotify.com/web-api/authorization-guide/#client-credentials-flow </remarks>
        public static async Task<string> Get(
            this HttpClient http,
            Uri requestUri,
            AuthenticationHeaderValue authenticationHeader)
        {
            //TODO: Implement if-modified-since support, serving from cache if response = 304

            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));

            Logger.Debug(
                $"GET {requestUri}. Token = {authenticationHeader?.ToString()?.Substring(0, 11)}...",
                nameof(RestHttpClient));

            http.DefaultRequestHeaders.Authorization = authenticationHeader;
            var response = await http.GetAsync(requestUri);

            Logger.Information($"Got {requestUri} {response.StatusCode}", nameof(RestHttpClient));

            await CheckForErrors(response);

            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Makes an HTTP(S) POST request to <seealso cref="requestUrl"/> with <seealso cref="formData"/> as the request body. Returns the result as (awaitable Task of) <seealso cref="string"/>.
        /// </summary>
        /// <param name="requestUri">The entire request URL as <see cref="Uri"/>.</param>
        /// <param name="formData">The URL encoded formData in format "key1=value1&amp;key2=value2"</param>
        /// <returns>An (awaitable Task of) <seealso cref="string"/> </returns>
        /// <remarks>Will Authorise using the values of the SpotifyApiClientId and SpotifyApiClientSecret appSettings. See 
        /// https://developer.spotify.com/web-api/authorization-guide/#client-credentials-flow </remarks>
        public static async Task<string> Post(this HttpClient http, Uri requestUri, string formData)
        {
            return await Post(http, requestUri, formData, null);
        }

        /// <summary>
        /// Makes an HTTP(S) POST request to <seealso cref="requestUrl"/> with <seealso cref="formData"/> as the request body. Returns the result as (awaitable Task of) <seealso cref="string"/>.
        /// </summary>
        /// <param name="requestUri">The entire request URI as <see cref="Uri"/>.</param>
        /// <param name="formData">The URL encoded formData in format "key1=value1&amp;key2=value2"</param>
        /// <param name="headerValue">An authentication header for the request.</param>
        /// <returns>An (awaitable Task of) <seealso cref="string"/> </returns>
        /// <remarks>Will Authorise using the values of the SpotifyApiClientId and SpotifyApiClientSecret appSettings. See 
        /// https://developer.spotify.com/web-api/authorization-guide/#client-credentials-flow </remarks>
        public static async Task<string> Post(
            this HttpClient http,
            Uri requestUri,
            string formData,
            AuthenticationHeaderValue headerValue)
        {
            if (requestUri == null) throw new ArgumentNullException(nameof(requestUri));
            if (string.IsNullOrEmpty(formData)) throw new ArgumentNullException(formData);

            Logger.Debug(
                $"POST {requestUri}. Token = {headerValue?.ToString()?.Substring(0, 11)}...",
                nameof(RestHttpClient));

            http.DefaultRequestHeaders.Authorization = headerValue;
            var response =
                await
                    http.PostAsync(requestUri,
                        new StringContent(formData, Encoding.UTF8, "application/x-www-form-urlencoded"));

            Logger.Information($"Posted {requestUri} {response.StatusCode}", nameof(RestHttpClient));

            await CheckForErrors(response);

            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Checks the reponse from the Spotify Server for an error.
        /// </summary>
        /// <param name="response"></param>
        /// <returns>If a Spotify API Error message is parsed, a <see cref="SpotifyApiErrorException"/> is thrown.
        /// If any other error is returned a <see cref="HttpResponseMessageException"/> if thrown. If no error
        /// the method returns void.</returns>
        public static async Task CheckForErrors(HttpResponseMessage response)
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