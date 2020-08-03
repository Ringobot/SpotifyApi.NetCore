using Newtonsoft.Json;

namespace SpotifyApi.NetCore
{
    public class ModifyPlaylistResponse
    {
        /// <summary>
        /// The snapshot_id can be used to identify your playlist version in future requests.
        /// </summary>
        [JsonProperty("snapshot_id")]
        public string SnapshotId { get; set; }
    }
}