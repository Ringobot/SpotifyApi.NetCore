using Newtonsoft.Json;

namespace SpotifyApi.NetCore
{
    public class ModifyPlaylistResponse
    {
        [JsonProperty("snapshot_id")]
        public string SnapshotId { get; set; }
    }
}