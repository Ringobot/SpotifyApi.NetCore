using System.Threading.Tasks;

namespace SpotifyApiDotNetCore
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
        Task<dynamic> GetPlaylists(string username);

        /// <summary>
        /// Get a list of a user's playlists.
        /// </summary>
        /// <returns>The JSON result deserialized to dynamic.</returns>
        Task<dynamic> GetPlaylist(string username, string playlistId);

        Task<dynamic> GetTracks(string username, string playlistId);


    }
}
