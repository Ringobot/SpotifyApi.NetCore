using System;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Defines a wrapper for the Spotify Web Playlists API.
    /// </summary>
    public interface IPlaylistsApi
    {
        #region GetPlaylists

        /// <summary>
        /// Get a list of a user's playlists.
        /// </summary>
        /// <returns>The JSON result deserialized to object (as dynamic).</returns>
        Task<PlaylistsSearchResult> GetPlaylists(string username);
        Task<T> GetPlaylists<T>(string username);

        #endregion

        #region GetPlaylist

        /// <summary>
        /// Get a list of a user's playlists.
        /// </summary>
        /// <returns>The JSON result deserialized to dynamic.</returns>
        [Obsolete("This endpoint has been deprecated by Spotify and will be removed in the next major release. See https://developer.spotify.com/community/news/2018/06/12/changes-to-playlist-uris/")]
        Task<Playlist> GetPlaylist(string username, string playlistId);
        
        [Obsolete("This endpoint has been deprecated by Spotify and will be removed in the next major release. See https://developer.spotify.com/community/news/2018/06/12/changes-to-playlist-uris/")]
        Task<T> GetPlaylist<T>(string username, string playlistId);

        #endregion

        #region GetTracks

        [Obsolete("This endpoint has been deprecated by Spotify and will be removed in the next major release. See https://developer.spotify.com/community/news/2018/06/12/changes-to-playlist-uris/")]
        Task<PlaylistTracksSearchResult> GetTracks(string username, string playlistId);
        
        [Obsolete("This endpoint has been deprecated by Spotify and will be removed in the next major release. See https://developer.spotify.com/community/news/2018/06/12/changes-to-playlist-uris/")]
        Task<T> GetTracks<T>(string username, string playlistId);

        #endregion

        #region SearchPlaylists

        /// <summary>
        /// Get Spotify Catalog information about tracks that match a keyword string.
        /// </summary>
        /// <param name="query">Search query keywords and optional field filters and operators. See
        /// https://developer.spotify.com/documentation/web-api/reference/search/search/#writing-a-query---guidelines</param>
        /// <returns>Task of <see cref="PlaylistsSearchResult" /></returns>
        Task<PlaylistsSearchResult> SearchPlaylists(string query);

        /// <summary>
        /// Get Spotify Catalog information about tracks that match a keyword string.
        /// </summary>
        /// <param name="query">Search query keywords and optional field filters and operators. See
        /// https://developer.spotify.com/documentation/web-api/reference/search/search/#writing-a-query---guidelines</param>
        /// <param name="limit">Optional. Maximum number of results to return. Default: 20, Minimum: 1,
        /// Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first result to return. Default: 0 (the
        /// first result). Maximum offset (including limit): 10,000. Use with limit to get the next
        /// page of search results.</param>
        /// <returns>Task of <see cref="PlaylistsSearchResult" /></returns>
        Task<PlaylistsSearchResult> SearchPlaylists(string query, (int limit, int offset) limitOffset);

        #endregion
    }
}
