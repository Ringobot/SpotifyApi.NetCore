using System;
using Newtonsoft.Json;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Episode object (full) (public)
    /// </summary>
    /// <remarks>
    /// https://developer.spotify.com/documentation/web-api/reference/object-model/#episode-object-full
    /// </remarks>
    public partial class Episode
    {
        /// <summary>
        /// A URL to a 30 second preview (MP3 format) of the episode. null if not available.
        /// </summary>
        [JsonProperty("audio_preview_url")]
        public Uri AudioPreviewUrl { get; set; }

        /// <summary>
        /// A description of the episode.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// The episode length in milliseconds.
        /// </summary>
        [JsonProperty("duration_ms")]
        public long DurationMs { get; set; }

        /// <summary>
        /// Whether or not the episode has explicit content (true = yes it does; false = no it does not OR unknown).
        /// </summary>
        [JsonProperty("explicit")]
        public bool Explicit { get; set; }

        /// <summary>
        /// External URLs for this episode.
        /// </summary>
        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        /// <summary>
        /// A link to the Web API endpoint providing full details of the episode.
        /// </summary>
        [JsonProperty("href")]
        public Uri Href { get; set; }

        /// <summary>
        /// The Spotify ID for the episode.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The cover art for the episode in various sizes, widest first.
        /// </summary>
        [JsonProperty("images")]
        public Image[] Images { get; set; }

        /// <summary>
        /// True if the episode is hosted outside of Spotify’s CDN.
        /// </summary>
        [JsonProperty("is_externally_hosted")]
        public bool IsExternallyHosted { get; set; }

        /// <summary>
        /// True if the episode is playable in the given market. Otherwise false.
        /// </summary>
        [JsonProperty("is_playable")]
        public bool IsPlayable { get; set; }

        /// <summary>
        /// Note: This field is deprecated and might be removed in the future. Please use the languages field instead. The language used in the episode, identified by a ISO 639 code.
        /// </summary>
        [JsonProperty("language")]
        public string Language { get; set; }

        /// <summary>
        /// A list of the languages used in the episode, identified by their ISO 639 code.
        /// </summary>
        [JsonProperty("languages")]
        public string[] Languages { get; set; }

        /// <summary>
        /// The name of the episode.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The date the episode was first released, for example "1981-12-15". Depending on the precision, it might be shown as "1981" or "1981-12".
        /// </summary>
        [JsonProperty("release_date")]
        public DateTimeOffset ReleaseDate { get; set; }

        /// <summary>
        /// The precision with which release_date value is known: "year", "month", or "day".
        /// </summary>
        [JsonProperty("release_date_precision")]
        public string ReleaseDatePrecision { get; set; }

        /// <summary>
        /// The user’s most recent position in the episode. Set if the supplied access token is a user token and has the scope user-read-playback-position.
        /// </summary>
        [JsonProperty("resume_point")]
        public ResumePoint ResumePoint { get; set; }

        /// <summary>
        /// The show on which the episode belongs.
        /// </summary>
        [JsonProperty("show")]
        public Show Show { get; set; }

        /// <summary>
        /// The object type: "episode".
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// The Spotify URI for the episode.
        /// </summary>
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}
