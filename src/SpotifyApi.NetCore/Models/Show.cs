using System;
using Newtonsoft.Json;

namespace SpotifyApi.NetCore.Models
{
    /// <summary>
    /// Show object (full) (public)
    /// </summary>
    /// <remarks>
    /// https://developer.spotify.com/documentation/web-api/reference/object-model/#show-object-full
    /// </remarks>
    public partial class Show
    {
        /// <summary>
        /// A list of the countries in which the show can be played, identified by their ISO 3166-1 alpha-2 code.
        /// </summary>
        [JsonProperty("available_markets")]
        public string[] AvailableMarkets { get; set; }

        /// <summary>
        /// The copyright statements of the show.
        /// </summary>
        [JsonProperty("copyrights")]
        public object[] Copyrights { get; set; }

        /// <summary>
        /// A description of the show.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Whether or not the show has explicit content (true = yes it does; false = no it does not OR unknown).
        /// </summary>
        [JsonProperty("explicit")]
        public bool Explicit { get; set; }

        /// <summary>
        /// A list of the show’s episodes.
        /// </summary>
        [JsonProperty("episodes")]
        public PagedShows Episodes { get; set; }

        /// <summary>
        /// Known external URLs for this show.
        /// </summary>
        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        /// <summary>
        /// A link to the Web API endpoint providing full details of the show.
        /// </summary>
        [JsonProperty("href")]
        public Uri Href { get; set; }

        /// <summary>
        /// The Spotify ID for the show.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The cover art for the show in various sizes, widest first.
        /// </summary>
        [JsonProperty("images")]
        public Image[] Images { get; set; }

        /// <summary>
        /// True if all of the show’s episodes are hosted outside of Spotify’s CDN. This field might be null in some cases.
        /// </summary>
        [JsonProperty("is_externally_hosted")]
        public bool IsExternallyHosted { get; set; }

        /// <summary>
        /// A list of the languages used in the show, identified by their ISO 639 code.
        /// </summary>
        [JsonProperty("languages")]
        public string[] Languages { get; set; }

        /// <summary>
        /// The media type of the show.
        /// </summary>
        [JsonProperty("media_type")]
        public string MediaType { get; set; }

        /// <summary>
        /// The name of the show.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The publisher of the show.
        /// </summary>
        [JsonProperty("publisher")]
        public string Publisher { get; set; }

        /// <summary>
        /// The object type: “show”.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// The Spotify URI for the show.
        /// </summary>
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}
