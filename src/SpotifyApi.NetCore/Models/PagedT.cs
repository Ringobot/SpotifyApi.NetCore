using Newtonsoft.Json;

namespace SpotifyApi.NetCore
{
    public class Paged<T>
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("items")]
        public T[] Items { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("offset", NullValueHandling = NullValueHandling.Ignore)]
        public int Offset { get; set; }

        [JsonProperty("previous", NullValueHandling = NullValueHandling.Ignore)]
        public string Previous { get; set; }

        /// <summary>
        /// The cursors used to find the next set of items.
        /// </summary>
        [JsonProperty("cursor", NullValueHandling = NullValueHandling.Ignore)]
        public Cursor Cursor { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }

    public class Cursor
    {
        /// <summary>
        /// The cursor to use as key to find the next page of items.
        /// </summary>
        [JsonProperty("after")]
        public string After { get; set; }
    }
}
