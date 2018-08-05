using Newtonsoft.Json;

namespace SpotifyApi.NetCore
{
    public partial class RecommendationsResult
    {
        [JsonProperty("tracks")]
        public Track[] Tracks { get; set; }

        [JsonProperty("seeds")]
        public Seed[] Seeds { get; set; }
    }

    public partial class Seed
    {
        [JsonProperty("initialPoolSize")]
        public int InitialPoolSize { get; set; }

        [JsonProperty("afterFilteringSize")]
        public int AfterFilteringSize { get; set; }

        [JsonProperty("afterRelinkingSize")]
        public int AfterRelinkingSize { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }
    }
    
}