using Newtonsoft.Json;

namespace SpotifyApi.NetCore
{
    public class PlaylistDetails
    {
        /// <summary>
        /// The name for the new playlist, for example "Your Coolest Playlist". This name does not 
        /// need to be unique; a user may have several playlists with the same name.
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        /// Defaults to true. If true the playlist will be public, if false it will be private. To 
        /// be able to create private playlists, the user must have granted the playlist-modify-private scope .
        /// </summary>
        [JsonProperty("public", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Public { get; set; }

        /// <summary>
        /// Defaults to false. If true the playlist will be collaborative. Note that to create a collaborative 
        /// playlist you must also set public to false . To create collaborative playlists you must have 
        /// granted playlist-modify-private and playlist-modify-public scopes.
        /// </summary>
        [JsonProperty("collaborative", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Collaborative { get; set; }

        /// <summary>
        /// Value for playlist description as displayed in Spotify Clients and in the Web API.
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
    }
}