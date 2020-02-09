using Newtonsoft.Json;

namespace SpotifyApi.NetCore
{
    public class FeaturedPlaylists : PagedPlaylists
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
