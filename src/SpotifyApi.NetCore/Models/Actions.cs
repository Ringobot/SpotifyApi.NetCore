using Newtonsoft.Json;

namespace SpotifyApi.NetCore
{
    public class Actions
    {
        [JsonProperty("disallows")]
        public Disallows Disallows { get; set; }
    }

    public class Disallows
    {
        [JsonProperty("interrupting_playback")]
        public bool InterruptingPlayback { get; set; }

        [JsonProperty("pausing")]
        public bool Pausing { get; set; }

        [JsonProperty("resuming")]
        public bool Resuming { get; set; }

        [JsonProperty("seeking")]
        public bool Seeking { get; set; }

        [JsonProperty("skipping_next")]
        public bool SkippingNext { get; set; }

        [JsonProperty("skipping_prev")]
        public bool SkippingPrev { get; set; }

        [JsonProperty("toggling_repeat_context")]
        public bool TogglingRepeatContext { get; set; }

        [JsonProperty("toggling_shuffle")]
        public bool TogglingShuffle { get; set; }

        [JsonProperty("toggling_repeat_track")]
        public bool TogglingRepeatTrack { get; set; }

        [JsonProperty("transferring_playback")]
        public bool TransferringPlayback { get; set; }
    }
}
