using Newtonsoft.Json;
using System;

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
        [JsonProperty("cursors", NullValueHandling = NullValueHandling.Ignore)]
        public Cursors Cursors { get; set; }

        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public int Total { get; set; }
    }
}
