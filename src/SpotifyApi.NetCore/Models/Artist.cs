// Thanks @quicktype !
using Newtonsoft.Json;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Artist object (full).
    /// </summary>
    /// <remarks> https://developer.spotify.com/documentation/web-api/reference/object-model/ </remarks>
    public partial class Artist
    {
        /// <summary>
        /// Known external URLs for this artist.
        /// </summary>
        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        /// <summary>
        /// Information about the followers of the artist.
        /// </summary>
        [JsonProperty("followers")]
        public Followers Followers { get; set; }

        /// <summary>
        /// A list of the genres the artist is associated with. For example: "Prog Rock" , "Post-Grunge". 
        /// </summary>
        [JsonProperty("genres")]
        public string[] Genres { get; set; }

        /// <summary>
        /// A link to the Web API endpoint providing full details of the artist.
        /// </summary>
        [JsonProperty("href")]
        public string Href { get; set; }

        /// <summary>
        /// The Spotify ID for the artist.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Images of the artist in various sizes, widest first.
        /// </summary>
        [JsonProperty("images")]
        public Image[] Images { get; set; }

        /// <summary>
        /// The name of the artist
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The popularity of the artist. The value will be between 0 and 100, with 100 being the most 
        /// popular.
        /// </summary>
        [JsonProperty("popularity")]
        public int Popularity { get; set; }

        /// <summary>
        /// The object type: "artist"
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// The Spotify URI for the artist.
        /// </summary>
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}