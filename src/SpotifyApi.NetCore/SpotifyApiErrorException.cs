using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SpotifyApi.NetCore
{
    public class SpotifyApiErrorException : Exception
    {
        //public SpotifyApiErrorException(string message, Exception innerException) : base(message, innerException) { }
        public SpotifyApiErrorException(string message) : base(message) { }

        public SpotifyApiErrorException(HttpStatusCode statusCode, SpotifyApiError spotifyApiError) : base(spotifyApiError?.Message)
        {
            HttpStatusCode = statusCode;
            SpotifyApiError = spotifyApiError;
        }

        public HttpStatusCode HttpStatusCode { get; private set; }
        public SpotifyApiError SpotifyApiError { get; private set; }

        public static async Task<SpotifyApiError> ReadErrorResponse(HttpResponseMessage response)
        {
            // if no content
            if (response.Content == null) return null;

            // if not JSON content type
            if (response.Content.Headers.ContentType?.MediaType != "application/json") return null;

            var content = await response.Content.ReadAsStringAsync();

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

    public class SpotifyApiError
    {
        public string Message { get; set; }

        public string Json { get; set; }
    }
}