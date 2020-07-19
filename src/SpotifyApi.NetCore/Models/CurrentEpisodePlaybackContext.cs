using Newtonsoft.Json;

namespace SpotifyApi.NetCore
{
    public partial class CurrentEpisodePlaybackContext : CurrentPlaybackContext
    {
        /// <summary>
        /// The currently playing Episode. Can be null.
        /// </summary>
        [JsonProperty("item")]
        public Episode Item { get; set; }
    }
}
