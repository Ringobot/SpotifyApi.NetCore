using System.Net;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// A response from the Spotify API
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SpotifyResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public string ReasonPhrase { get; set; }
    }

    /// <summary>
    /// A generic response from the Spotify API
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SpotifyResponse<T> : SpotifyResponse
    {
        public T Data { get; set; }
    }
}
