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
        /// The country of the user, as set in the user's account profile. An ISO 3166-1 alpha-2 country 
        /// code. This field is only available when the current user has granted access to the 
        /// user-read-private scope.
        /// </summary>
        [JsonProperty("country", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; }

        /// <summary>
        /// The name displayed on the user’s profile. null if not available.
        /// </summary>
        [JsonProperty("display_name", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayName { get; set; }

        /// <summary>
        /// The user's email address, as entered by the user when creating their account. Important! 
        /// This email address is unverified; there is no proof that it actually belongs to the user. 
        /// This field is only available when the current user has granted access to the user-read-email 
        /// scope.
        /// </summary>
        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

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
        /// The user's Spotify subscription level: "premium", "free", etc. (The subscription level 
        /// "open" can be considered the same as "free".) This field is only available when the 
        /// current user has granted access to the user-read-private scope.
        /// </summary>
        [JsonProperty("product", NullValueHandling = NullValueHandling.Ignore)]
        public string Product { get; set; }

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

    public partial class ResumePoint
    {
        [JsonProperty("fully_played")]
        public bool FullyPlayed { get; set; }

        [JsonProperty("resume_position_ms")]
        public long ResumePositionMs { get; set; }
    }
}