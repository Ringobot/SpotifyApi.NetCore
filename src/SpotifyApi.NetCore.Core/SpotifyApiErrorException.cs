using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// A Spotify API Error Exception.
    /// </summary>
    public class SpotifyApiErrorException : Exception
    {
        /// <summary>
        /// Instantiates a SpotifyApiErrorException with a message.
        /// </summary>
        /// <param name="message"></param>
        public SpotifyApiErrorException(string message) : base(message) { }

        /// <summary>
        /// Instantiates a SpotifyApiErrorException with a status code and a <see cref="SpotifyApiError"/> .
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="spotifyApiError"></param>
        public SpotifyApiErrorException(HttpStatusCode statusCode, SpotifyApiError spotifyApiError) : base(spotifyApiError?.Message)
        {
            HttpStatusCode = statusCode;
            SpotifyApiError = spotifyApiError;
        }

        /// <summary>
        /// The HTTP Status Code returned from the Spotify API
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; private set; }

        /// <summary>
        /// The derived <see cref="SpotifyApiError"/> returned from the Spotify API
        /// </summary>
        public SpotifyApiError SpotifyApiError { get; private set; }

        /// <summary>
        /// Reads an <see cref="HttpResponseMessage"/> to parse a <see cref="SpotifyApiError"/>.
        /// </summary>
        /// <param name="response">The <see cref="HttpResponseMessage"/>.</param>
        /// <returns>An instance of <see cref="SpotifyApiError"/>.</returns>
        public static async Task<SpotifyApiError> ReadErrorResponse(HttpResponseMessage response)
        {
            // if no content
            if (response.Content == null) return null;

            // if not JSON content type
            if (response.Content.Headers.ContentType?.MediaType != "application/json") return null;

            var content = await response.Content.ReadAsStringAsync();
            Logger.Debug(content, nameof(SpotifyApiErrorException));

            // if empty body
            if (string.IsNullOrWhiteSpace(content)) return null;

            var error = new SpotifyApiError { Json = content };

            // interrogate properties to detect error json type
            var deserialized = JsonConvert.DeserializeObject(content) as JObject;

            // if no error property
            if (!deserialized.ContainsKey("error")) return error;

            switch (deserialized["error"].Type)
            {
                case JTokenType.Object:
                    error.Message = deserialized["error"].Value<string>("message");
                    break;
                case JTokenType.String:
                    error.Message = deserialized["error_description"].Value<string>();
                    break;
            }

            return error;
        }
    }

    /// <summary>
    /// A model for a Spotify API Error.
    /// </summary>
    public class SpotifyApiError
    {
        /// <summary>
        /// The message returned by the API
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The raw JSON string returned by the API
        /// </summary>
        public string Json { get; set; }
    }
}