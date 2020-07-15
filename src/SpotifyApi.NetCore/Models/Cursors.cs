using Newtonsoft.Json;

namespace SpotifyApi.NetCore
{
    public class Cursors
    {
        /// <summary>
        /// The cursor to use as key to find the next page of items.
        /// </summary>
        [JsonProperty("after")]
        public string After { get; set; }

        /// <summary>
        /// The cursor to use as key to find the previous page of items.
        /// </summary>
        [JsonProperty("before")]
        public string Before { get; set; }
    }
}
