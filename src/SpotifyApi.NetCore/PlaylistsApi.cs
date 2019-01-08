using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// An API Wrapper for the Spotify Web API Playlists endpoints.
    /// </summary>
    public class PlaylistsApi : SpotifyWebApi, IPlaylistsApi
    {
        protected internal virtual ISearchApi SearchApi { get; set; }

        public PlaylistsApi(HttpClient httpClient, IAccountsService accountsService) : base(httpClient, accountsService)
        {
            SearchApi = new SearchApi(httpClient, accountsService);
        }

        #region GetPlaylists

        /// <summary>
        /// Get a list of a user's playlists.
        /// </summary>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service,
        /// used for this call only. See constructors for more ways to provide access tokens.</param>
        /// <returns>The JSON result deserialized to object (as dynamic).</returns>
        public async Task<T> GetPlaylists<T>(string username, string accessToken = null)
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentNullException("username");
            return await GetModel<T>($"{BaseUrl}/users/{Uri.EscapeDataString(username)}/playlists", accessToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service,
        /// used for this call only. See constructors for more ways to provide access tokens.</param>
        /// <returns></returns>
        public async Task<PlaylistsSearchResult> GetPlaylists(string username, string accessToken = null)
            => await GetPlaylists<PlaylistsSearchResult>(username, accessToken);

        #endregion

        #region GetPlaylist

        [Obsolete("This endpoint has been deprecated by Spotify and will be removed in the next major release. See https://developer.spotify.com/community/news/2018/06/12/changes-to-playlist-uris/")]
        public async Task<Playlist> GetPlaylist(string username, string playlistId)
            => await GetPlaylist<Playlist>(username, playlistId);

        [Obsolete("This endpoint has been deprecated by Spotify and will be removed in the next major release. See https://developer.spotify.com/community/news/2018/06/12/changes-to-playlist-uris/")]
        public Task<T> GetPlaylist<T>(string username, string playlistId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region GetTracks

        /// <summary>
        /// Get full details of the tracks of a playlist owned by a Spotify user.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="username">The user's Spotify user ID.</param>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <returns></returns>
        [Obsolete("This endpoint has been deprecated by Spotify and will be removed in the next major release. See https://developer.spotify.com/community/news/2018/06/12/changes-to-playlist-uris/")]
        public async Task<PlaylistTracksSearchResult> GetTracks(string username, string playlistId)
            => await GetTracks<PlaylistTracksSearchResult>(username, playlistId);

        /// <summary>
        /// Get full details of the tracks of a playlist owned by a Spotify user.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="username">The user's Spotify user ID.</param>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <returns></returns>

        [Obsolete("This endpoint has been deprecated by Spotify and will be removed in the next major release. See https://developer.spotify.com/community/news/2018/06/12/changes-to-playlist-uris/")]
        public async Task<T> GetTracks<T>(string username, string playlistId)
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentNullException("username");
            if (string.IsNullOrEmpty(playlistId)) throw new ArgumentNullException("playlistId");

            return await GetModel<T>($"{BaseUrl}/users/{Uri.EscapeDataString(username)}/playlists/{Uri.EscapeDataString(playlistId)}/tracks");
        }

        #endregion

        #region SearchPlaylists

        /// <summary>
        /// Get Spotify Catalog information about tracks that match a keyword string.
        /// </summary>
        /// <param name="query">Search query keywords and optional field filters and operators. See
        /// https://developer.spotify.com/documentation/web-api/reference/search/search/#writing-a-query---guidelines</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service,
        /// used for this call only. See constructors for more ways to provide access tokens.</param>
        /// <returns>Task of <see cref="PlaylistsSearchResult" /></returns>
        public async Task<PlaylistsSearchResult> SearchPlaylists(string query, string accessToken = null)
            => (await SearchApi.Search(query, new string[] { SpotifySearchTypes.Playlist }, null, (0, 0), accessToken)).Playlists;

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
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service,
        /// used for this call only. See constructors for more ways to provide access tokens.</param>
        /// <returns>Task of <see cref="PlaylistsSearchResult" /></returns>
        public async Task<PlaylistsSearchResult> SearchPlaylists(string query, (int limit, int offset) limitOffset, string accessToken = null)
            => (await SearchApi.Search(query, new string[] { SpotifySearchTypes.Playlist }, null, limitOffset, accessToken)).Playlists;

        #endregion

    }
}
