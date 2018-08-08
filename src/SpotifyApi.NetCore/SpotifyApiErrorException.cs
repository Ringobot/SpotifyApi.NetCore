using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
            return (response.Content == null) ? null
                : JsonConvert.DeserializeObject<SpotifyApiErrorResponseBody>(await response.Content.ReadAsStringAsync())?.Error;
        }
    }

    public class SpotifyApiErrorResponseBody
    {
        [JsonProperty("error")]
        public SpotifyApiError Error { get; set; }
    }

    public class SpotifyApiError
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public static class SpotifyApiErrorExtensions
    {
        public static bool IsValid (this SpotifyApiError error)
        {
            return !string.IsNullOrEmpty(error.Message) && error.Status != 0;
        }
    }
}