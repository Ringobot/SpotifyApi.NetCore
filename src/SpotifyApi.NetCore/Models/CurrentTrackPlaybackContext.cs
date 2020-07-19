using Newtonsoft.Json;

namespace SpotifyApi.NetCore
{
    public partial class CurrentTrackPlaybackContext : CurrentPlaybackContext
    {
        /// <summary>
        /// The currently playing track. Can be null.
        /// </summary>
        [JsonProperty("item")]
        public Track Item { get; set; }
    }
}
