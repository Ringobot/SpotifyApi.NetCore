using SpotifyApi.NetCore.Authorization;
using SpotifyApi.NetCore.Helpers;
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
        #region constructors

        public PlaylistsApi(HttpClient httpClient, IAccessTokenProvider accessTokenProvider) : base(httpClient, accessTokenProvider)
        {
            SearchApi = new SearchApi(httpClient, accessTokenProvider);
        }

        public PlaylistsApi(HttpClient httpClient, string accessToken) : base(httpClient, accessToken)
        {
            SearchApi = new SearchApi(httpClient, accessToken);
        }

        public PlaylistsApi(HttpClient httpClient) : base(httpClient)
        {
            SearchApi = new SearchApi(httpClient);
        }

        #endregion

        protected internal virtual ISearchApi SearchApi { get; set; }

        #region GetPlaylists

        /// <summary>
        /// Get a list of a user's playlists.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.</param>
        /// <param name="limit">Optional. The maximum number of playlists to return. Default: 20. Minimum: 
        /// 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first playlist to return. Default: 0 (the 
        /// first object). Maximum offset: 100.000. Use with limit to get the next set of playlists.</param>
        /// <returns><see cref="PlaylistsSearchResult"/></returns>
        public async Task<PlaylistsSearchResult> GetPlaylists(
            string username,
            string accessToken = null,
            int? limit = null,
            int offset = 0)
            => await GetPlaylists<PlaylistsSearchResult>(username, accessToken);

        /// <summary>
        /// Get a list of a user's playlists.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.</param>
        /// <param name="limit">Optional. The maximum number of playlists to return. Default: 20. Minimum: 
        /// 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first playlist to return. Default: 0 (the 
        /// first object). Maximum offset: 100.000. Use with limit to get the next set of playlists.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>The JSON result deserialized as `T`.</returns>
        public async Task<T> GetPlaylists<T>(
            string username,
            string accessToken = null,
            int? limit = null,
            int offset = 0)
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentNullException("username");

            string url = $"{BaseUrl}/users/{Uri.EscapeDataString(username)}/playlists";

            if ((limit ?? 0) > 0 || offset > 0)
            {
                url += "?";

                if ((limit ?? 0) > 0) url += $"limit={limit.Value}&";
                if (offset > 0) url += $"offset={offset}";
            }

            return await GetModel<T>(url, accessToken);
        }

        #endregion

        #region GetPlaylist

        public async Task<PlaylistSimplified> GetPlaylist(string playlistId, string accessToken = null)
            => await GetPlaylist<PlaylistSimplified>(playlistId, accessToken);

        public async Task<T> GetPlaylist<T>(string playlistId, string accessToken = null)
        {
            if (string.IsNullOrEmpty(playlistId)) throw new ArgumentNullException(nameof(playlistId));
            return await GetModel<T>($"{BaseUrl}/playlists/{SpotifyUriHelper.PlaylistId(playlistId)}", accessToken);
        }

        #endregion

        #region GetTracks

        /// <summary>
        /// Get full details of the tracks of a playlist owned by a Spotify user.
        /// </summary>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.
        /// <param name="fields">Optional. Filters for the query: a comma-separated list of the fields to return. If omitted, all fields are returned. See docs for examples.</param>
        /// <param name="limit">Optional. The maximum number of tracks to return. Default: 100. Minimum: 1. Maximum: 100.</param>
        /// <param name="offset">Optional. The index of the first track to return. Default: 0 (the first object).</param>
        /// <param name="market">Optional. An <see cref="SpotifyCountryCodes"/> or the string <see cref="SpotifyCountryCodes._From_Token"/>.
        /// Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns>Task of <see cref="PlaylistPaged"/></returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/playlists/get-playlists-tracks/
        /// </remarks>
        public async Task<PlaylistPaged> GetTracks(
            string playlistId,
            string accessToken = null,
            string fields = null,
            int? limit = null,
            int offset = 0,
            string market = null,
            string[] additionalTypes = null) => await GetTracks<PlaylistPaged>(
                playlistId,
                accessToken,
                fields,
                limit,
                offset,
                market,
                additionalTypes);

        /// <summary>
        /// Get full details of the tracks of a playlist owned by a Spotify user.
        /// </summary>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.
        /// <param name="fields">Optional. Filters for the query: a comma-separated list of the fields to return. If omitted, all fields are returned. See docs for examples.</param>
        /// <param name="limit">Optional. The maximum number of tracks to return. Default: 100. Minimum: 1. Maximum: 100.</param>
        /// <param name="offset">Optional. The index of the first track to return. Default: 0 (the first object).</param>
        /// <param name="market">Optional. An <see cref="SpotifyCountryCodes"/> or the string <see cref="SpotifyCountryCodes._From_Token"/>.
        /// Provide this parameter if you want to apply Track Relinking.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/playlists/get-playlists-tracks/
        /// </remarks>
        public async Task<T> GetTracks<T>(
            string playlistId,
            string accessToken = null,
            string fields = null,
            int? limit = null,
            int offset = 0,
            string market = null,
            string[] additionalTypes = null)
        {
            if (string.IsNullOrEmpty(playlistId)) throw new ArgumentNullException(nameof(playlistId));
            string url = $"{BaseUrl}/playlists/{SpotifyUriHelper.PlaylistId(playlistId)}/tracks";
            if (!string.IsNullOrEmpty(fields) || (limit ?? 0) > 0 || offset > 0 || !string.IsNullOrEmpty(market))
            {
                url += "?";

                if (!string.IsNullOrEmpty(fields)) url += $"fields={fields}&";
                if ((limit ?? 0) > 0) url += $"limit={limit.Value}&";
                if (offset > 0) url += $"offset={offset}&";
                if (!string.IsNullOrEmpty(market)) url += $"market={market}";
            }

            return await GetModel<T>(url, accessToken);
        }

        #endregion

        #region SearchPlaylists

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
        public async Task<PlaylistsSearchResult> SearchPlaylists(
            string query,
            int? limit = null,
            int offset = 0,
            string accessToken = null)
            => (await SearchApi.Search(
                query,
                SpotifySearchTypes.Playlist, limit: limit, offset: offset, accessToken: accessToken)).Playlists;

        public Task<PlaylistSimplified> GetPlaylist(string playlistId, string accessToken = null, string fields = null, string[] additionalTypes = null, string market = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetPlaylist<T>(string playlistId, string accessToken = null, string fields = null, string[] additionalTypes = null, string market = null)
        {
            throw new NotImplementedException();
        }

        public Task<ModifyPlaylistResponse> AddItems(string playlistId, string[] spotifyUris, int? position = null, string accessToken = null)
        {
            throw new NotImplementedException();
        }

        public Task ChangePlaylistDetails(string playlistId, PlaylistDetails details, string accessToken = null)
        {
            throw new NotImplementedException();
        }

        public Task<Playlist> CreatePlaylist(string userId, PlaylistDetails details, string accessToken = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> CreatePlaylist<T>(string userId, PlaylistDetails details, string accessToken = null)
        {
            throw new NotImplementedException();
        }

        public Task<PagedPlaylists> GetCurrentUsersPlaylists(int? limit = null, int offset = 0, string accessToken = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetCurrentUsersPlaylists<T>(int? limit = null, int offset = 0, string accessToken = null)
        {
            throw new NotImplementedException();
        }

        public Task<Image[]> GetPlaylistCoverImage(string playlistId, string accessToken = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetPlaylistCoverImage<T>(string playlistId, string accessToken = null)
        {
            throw new NotImplementedException();
        }

        public Task<ModifyPlaylistResponse> RemoveItems(string playlistId, string[] spotifyUris, string snapshotId = null, string accessToken = null)
        {
            throw new NotImplementedException();
        }

        public Task<ModifyPlaylistResponse> RemoveItems(string playlistId, (string uri, int[] positions)[] spotifyUriPositions, string snapshotId = null, string accessToken = null)
        {
            throw new NotImplementedException();
        }

        public Task<ModifyPlaylistResponse> ReorderItems(string playlistId, int rangeStart, int insertBefore, int rangeLength = 1, string snapshotId = null, string accessToken = null)
        {
            throw new NotImplementedException();
        }

        public Task ReplaceItems(string playlistId, string[] spotifyUris, string accessToken = null)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
