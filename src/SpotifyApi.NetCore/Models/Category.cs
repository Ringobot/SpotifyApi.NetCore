using Newtonsoft.Json;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Category object (full)
    /// </summary>
    /// <remarks> https://developer.spotify.com/documentation/web-api/reference/browse/get-category/#categoryobject </remarks>
    public partial class Category
    {
        /// <summary>
        /// A link to the Web API endpoint returning full details of the category.
        /// </summary>
        [JsonProperty("href")]
        public string Href { get; set; }

        /// <summary>
        /// The category icon, in various sizes.
        /// </summary>
        [JsonProperty("icons")]
        public Image[] Icons { get; set; }

        /// <summary>
        /// The Spotify category ID of the category.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The name of the category.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
