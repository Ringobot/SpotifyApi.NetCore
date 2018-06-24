using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore.Http
{
    /// <summary>
    /// Defines an asynchronous HTTP Client that is typically used to communicate with a Web API.
    /// </summary>
    /// <remarks><seealso cref="HttpClient"/></remarks>
    public interface IHttpClient
    {
        /// <summary>
        /// Makes an HTTP(S) GET request to <seealso cref="requestUrl"/> and returns the result as (awaitable Task of) <seealso cref="string"/>.
        /// </summary>
        /// <param name="requestUrl">The entire request URL.</param>
        /// <returns>An (awaitable Task of) <seealso cref="string"/> </returns>
        Task<string> Get(string requestUrl);

        /// <summary>
        /// Makes an HTTP(S) GET request to <seealso cref="requestUrl"/> and returns the result as (awaitable Task of) <seealso cref="string"/>.
        /// </summary>
        /// <param name="requestUrl">The entire request URL.</param>
        /// <param name="authenticationHeader"></param>
        /// <returns>An (awaitable Task of) <seealso cref="string"/> </returns>
        Task<string> Get(string requestUrl, AuthenticationHeaderValue authenticationHeader);

        /// <summary>
        /// Makes an HTTP(S) POST request to <seealso cref="requestUrl"/> with <seealso cref="formData"/> as the request body. Returns the result as (awaitable Task of) <seealso cref="string"/>.
        /// </summary>
        /// <param name="requestUrl">The entire request URL.</param>
        /// <param name="formData">The URL encoded formData in format "key1=value1&amp;key2=value2"</param>
        /// <returns>An (awaitable Task of) <seealso cref="string"/> </returns>
        Task<string> Post(string requestUrl, string formData);

        /// <summary>
        /// Makes an HTTP(S) POST request to <seealso cref="requestUrl"/> with <seealso cref="formData"/> as the request body. Returns the result as (awaitable Task of) <seealso cref="string"/>.
        /// </summary>
        /// <param name="requestUrl">The entire request URL.</param>
        /// <param name="formData">The URL encoded formData in format "key1=value1&amp;key2=value2"</param>
        /// <param name="headerValue">An authentication header for the request.</param>
        /// <returns>An (awaitable Task of) <seealso cref="string"/> </returns>
        Task<string> Post(string requestUrl, string formData, AuthenticationHeaderValue headerValue);
    }
}
