using Newtonsoft.Json;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// External URL object
    /// </summary>
    /// <remarks>
    /// https://developer.spotify.com/documentation/web-api/reference/object-model/#external-url-object
    /// </remarks>
    public partial class ExternalUrls
    {
        [JsonProperty("spotify")]
        public string Spotify { get; set; }
    }

    /// <summary>
    /// Followers object
    /// </summary>
    /// <remarks>
    /// https://developer.spotify.com/documentation/web-api/reference/object-model/#followers-object
    /// </remarks>
    public partial class Followers
    {
        /// <summary>
        /// A link to the Web API endpoint providing full details of the followers; null if not available.
        /// </summary>
        [JsonProperty("href")]
        public string Href { get; set; }

        /// <summary>
        /// The total number of followers.
        /// </summary>
        [JsonProperty("total")]
        public int Total { get; set; }
    }

    /// <summary>
    /// Image object
    /// </summary>
    /// <remarks>
    /// https://developer.spotify.com/documentation/web-api/reference/object-model/#image-object
    /// </remarks>
    public partial class Image
    {
        /// <summary>
        /// The image height in pixels. If unknown: null or not returned.
        /// </summary>
        [JsonProperty("height", NullValueHandling = NullValueHandling.Ignore)]
        public int? Height { get; set; }

        /// <summary>
        /// The source URL of the image.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// The image width in pixels. If unknown: null or not returned.
        /// </summary>
        [JsonProperty("width", NullValueHandling = NullValueHandling.Ignore)]
        public int? Width { get; set; }
    }

    /// <summary>
    /// User object (public)
    /// </summary>
    /// <remarks>
    /// https://developer.spotify.com/documentation/web-api/reference/object-model/#user-object-public
    /// </remarks>
    public partial class User
    {
        /// <summary>
        /// The name displayed on the user’s profile. null if not available.
        /// </summary>
        [JsonProperty("display_name", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayName { get; set; }

        /// <summary>
        /// Known public external URLs for this user.
        /// </summary>
        [JsonProperty("external_urls")]
        public ExternalUrls ExternalUrls { get; set; }

        /// <summary>
        /// Information about the followers of this user.
        /// </summary>
        [JsonProperty("followers")]
        public Followers Followers { get; set; }

        /// <summary>
        /// A link to the Web API endpoint for this user.
        /// </summary>
        [JsonProperty("href")]
        public string Href { get; set; }

        /// <summary>
        /// The Spotify user ID for this user.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The user’s profile image.
        /// </summary>
        [JsonProperty("images")]
        public Image[] Images { get; set; }

        /// <summary>
        /// The object type: "user"
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// The Spotify URI for this user.
        /// </summary>
        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
    }
}