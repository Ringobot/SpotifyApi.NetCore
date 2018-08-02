using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Defines a wrapper for the Spotify Web Playlists API.
    /// </summary>
    public interface IPlaylistsApi
    {
        /// <summary>
        /// Get a list of a user's playlists.
        /// </summary>
        /// <returns>The JSON result deserialized to object (as dynamic).</returns>
        Task<PlaylistsResult> GetPlaylists(string username);
        Task<T> GetPlaylists<T>(string username);

        /// <summary>
        /// Get a list of a user's playlists.
        /// </summary>
        /// <returns>The JSON result deserialized to dynamic.</returns>
        Task<Playlist> GetPlaylist(string username, string playlistId);
        Task<T> GetPlaylist<T>(string username, string playlistId);

        Task<PlaylistTracksResult> GetTracks(string username, string playlistId);
        Task<T> GetTracks<T>(string username, string playlistId);
    }
}
