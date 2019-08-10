using Newtonsoft.Json;
using System;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Full Album Object.
    /// </summary>
    /// <remarks> https://developer.spotify.com/documentation/web-api/reference/object-model/ </remarks>
    public partial class Album
    {
        /// <summary>
        /// The type of the album: one of "album" , "single" , or "compilation".
        /// </summary>
        [JsonProperty("album_type")]
        public string AlbumType { get; set; }

        /// <summary>
        /// The artists of the album.
        /// </summary>
        [JsonProperty("artists")]
        public Artist[] Artists { get; set; }

        /// <summary>
        /// The markets in which the album is available:
        /// </summary>
        [JsonProperty("available_markets")]
        public string[] AvailableMarkets { get; set; }

        /// <summary>
        /// Known external URLs for this album.
        /// </summary>
        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        /// <summary>
        /// A link to the Web API endpoint providing full details of the album.
        /// </summary>
        [JsonProperty("href")]
        public Uri Href { get; set; }

        /// <summary>
        /// The Spotify ID for the album.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The cover art for the album in various sizes, widest first.
        /// </summary>
        [JsonProperty("images")]
        public Image[] Images { get; set; }

        /// <summary>
        /// The name of the album. In case of an album takedown, the value may be an empty string.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The date the album was first released, for example "1981-12-15". Depending on the precision, it might be shown as "1981" or "1981-12".
        /// </summary>
        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }

        /// <summary>
        /// The precision with which release_date value is known: "year" , "month" , or "day".
        /// </summary>
        [JsonProperty("release_date_precision")]
        public string ReleaseDatePrecision { get; set; }

        [JsonProperty("total_tracks")]
        public long TotalTracks { get; set; }

        /// <summary>
        /// The object type: “album”
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// The Spotify URI for the album.
        /// </summary>
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}