using SpotifyApi.NetCore.Authorization;
using SpotifyApi.NetCore.Helpers;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

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

        /// <summary>
        /// Get a playlist owned by a Spotify user.
        /// </summary>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.
        /// <param name="fields">Optional. Filters for the query: a comma-separated list of the fields to return. If omitted, all fields are returned. See docs for examples.</param>
        /// <param name="additionalTypes">Optional. A comma-separated list of item types that your 
        /// client supports besides the default track type. Valid types are: `track` and `episode`. 
        /// Note: This parameter was introduced to allow existing clients to maintain their current 
        /// behaviour and might be deprecated in the future. In addition to providing this parameter, 
        /// make sure that your client properly handles cases of new types in the future by checking 
        /// against the type field of each object.</param>
        /// <param name="market">Optional. An <see cref="SpotifyCountryCodes"/> or the string <see cref="SpotifyCountryCodes._From_Token"/>.
        /// Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns>Task of <see cref="PlaylistSimplified"/></returns>
        public async Task<PlaylistSimplified> GetPlaylist(
            string playlistId, 
            string accessToken = null,
            string fields = null,
            string[] additionalTypes = null,
            string market = null)
            => await GetPlaylist<PlaylistSimplified>(
                playlistId, 
                accessToken,
                fields: fields,
                additionalTypes: additionalTypes,
                market: market);

        /// <summary>
        /// Get a playlist owned by a Spotify user.
        /// </summary>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.
        /// <param name="fields">Optional. Filters for the query: a comma-separated list of the fields to return. If omitted, all fields are returned. See docs for examples.</param>
        /// <param name="additionalTypes">Optional. A comma-separated list of item types that your 
        /// client supports besides the default track type. Valid types are: `track` and `episode`. 
        /// Note: This parameter was introduced to allow existing clients to maintain their current 
        /// behaviour and might be deprecated in the future. In addition to providing this parameter, 
        /// make sure that your client properly handles cases of new types in the future by checking 
        /// against the type field of each object.</param>
        /// <param name="market">Optional. An <see cref="SpotifyCountryCodes"/> or the string <see cref="SpotifyCountryCodes._From_Token"/>.
        /// Provide this parameter if you want to apply Track Relinking.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T</returns>
        public async Task<T> GetPlaylist<T>(
            string playlistId, 
            string accessToken = null,
            string fields = null,
            string[] additionalTypes = null,
            string market = null)
        {
            if (string.IsNullOrEmpty(playlistId)) throw new ArgumentNullException(nameof(playlistId));

            var builder = new UriBuilder($"{BaseUrl}/playlists/{SpotifyUriHelper.PlaylistId(playlistId)}");
            builder.AppendToQueryIfValueNotNullOrWhiteSpace("fields", fields);
            builder.AppendToQueryAsCsv("additional_types", additionalTypes);
            builder.AppendToQueryIfValueNotNullOrWhiteSpace("market", market);

            return await GetModel<T>(builder.Uri, accessToken: accessToken);
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
        /// <param name="additionalTypes">Optional. A comma-separated list of item types that your 
        /// client supports besides the default track type. Valid types are: `track` and `episode`. 
        /// Note: This parameter was introduced to allow existing clients to maintain their current 
        /// behaviour and might be deprecated in the future. In addition to providing this parameter, 
        /// make sure that your client properly handles cases of new types in the future by checking 
        /// against the type field of each object.</param>
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
            string[] additionalTypes = null) 
            => await GetTracks<PlaylistPaged>(
                playlistId,
                accessToken: accessToken,
                fields: fields,
                limit: limit,
                offset: offset,
                market: market,
                additionalTypes: additionalTypes);

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
        /// <param name="additionalTypes">Optional. A comma-separated list of item types that your 
        /// client supports besides the default track type. Valid types are: `track` and `episode`. 
        /// Note: This parameter was introduced to allow existing clients to maintain their current 
        /// behaviour and might be deprecated in the future. In addition to providing this parameter, 
        /// make sure that your client properly handles cases of new types in the future by checking 
        /// against the type field of each object.</param>
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

            var builder = new UriBuilder($"{BaseUrl}/playlists/{SpotifyUriHelper.PlaylistId(playlistId)}/tracks");
            builder.AppendToQueryIfValueNotNullOrWhiteSpace("fields", fields);
            builder.AppendToQueryIfValueGreaterThan0("limit", limit);
            builder.AppendToQueryIfValueGreaterThan0("offset", offset);
            builder.AppendToQueryIfValueNotNullOrWhiteSpace("market", market);
            builder.AppendToQueryAsCsv("additional_types", additionalTypes);

            return await GetModel<T>(builder.Uri, accessToken);
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

        #endregion

        #region AddItemsToPlaylist

        /// <summary>
        /// Add one or more items to a user’s playlist.
        /// </summary>
        /// <param name="playlistId">Required. The Spotify ID for the playlist.</param>
        /// <param name="uris">Required. A JSON array of the Spotify URIs to add, can be track or episode URIs. For example: {"uris": ["spotify:track:4iV5W9uYEdYUVa79Axb7Rh","spotify:track:1301WleyT98MSxVHPZCA6M", "spotify:episode:512ojhOuo1ktJprKbVcKyQ"]} A maximum of 100 items can be added in one request.</param>
        /// <param name="position">Optional. The position to insert the items, a zero-based index. For example, to insert the items in the first position: position=0 ; to insert the items in the third position: position=2. If omitted, the items will be appended to the playlist. Items are added in the order they appear in the uris array. For example: {"uris": ["spotify:track:4iV5W9uYEdYUVa79Axb7Rh","spotify:track:1301WleyT98MSxVHPZCA6M", "spotify:episode:512ojhOuo1ktJprKbVcKyQ"], "position": 3}</param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="ModifyPlaylistResponse"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/playlists/add-tracks-to-playlist/
        /// </remarks>
        public async Task<ModifyPlaylistResponse> AddItemsToPlaylist(
            string playlistId,
            string[] uris,
            int? position = null,
            string accessToken = null
            )
        {
            if (string.IsNullOrWhiteSpace(playlistId)) throw new
                    ArgumentException("A valid Spotify playlist id must be specified.");

            if (uris?.Length < 1 || uris?.Length > 100) throw new
                     ArgumentException("A minimum of 1 and a maximum of 100 Spotify uri must be specified.");

            if (position != null && position < 0) throw new
                     ArgumentException("The position if supplied has to be 0 or greater than 0.");

            var builder = new UriBuilder($"{BaseUrl}/playlists/{playlistId}/tracks");
            return (await Post<ModifyPlaylistResponse>(builder.Uri, new { uris, position }, accessToken)).Data;
        }

        #endregion

        #region ChangePlaylistDetails

        /// <summary>
        /// Change a playlist’s name and public/private state. (The user must, of course, own the playlist.)
        /// </summary>
        /// <param name="playlistId">Required. The Spotify ID for the playlist.</param>
        /// <param name="details">Required. A <see cref="PlaylistDetails"/> objects containing Playlist details to change.</param>        
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <remarks>
        /// At least one optional parameter must be supplied.
        /// https://developer.spotify.com/documentation/web-api/reference/playlists/change-playlist-details/
        /// </remarks>
        public async Task ChangePlaylistDetails(
            string playlistId,
            PlaylistDetails details,
            string accessToken = null)
        {
            if (string.IsNullOrWhiteSpace(playlistId)) throw new
                    ArgumentException("A valid Spotify playlist id must be specified.");

            if (details == null) throw new ArgumentNullException(nameof(details));

            var builder = new UriBuilder($"{BaseUrl}/playlists/{playlistId}");
            await Put(builder.Uri, details, accessToken);
        }

        #endregion

        #region CreatePlaylist
        /// <summary>
        /// Create a playlist for a Spotify user. (The playlist will be empty until you add tracks.)
        /// </summary>
        /// <param name="userId">Required. The user’s Spotify user ID.</param>
        /// <param name="details">Required. A <see cref="PlaylistDetails"/> objects containing the new Playlist details.</param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="Playlist"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/playlists/create-playlist/
        /// </remarks>
        public Task<Playlist> CreatePlaylist(
            string userId,
            PlaylistDetails details,
            string accessToken = null
            ) => CreatePlaylist<Playlist>(userId, details, accessToken: accessToken);

        /// <summary>
        /// Create a playlist for a Spotify user. (The playlist will be empty until you add tracks.)
        /// </summary>
        /// <param name="userId">Required. The user’s Spotify user ID.</param>
        /// <param name="details">Required. A <see cref="PlaylistDetails"/> objects containing the new Playlist details.</param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <returns>A Task that, once successfully completed, returns the response deserialized as T.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/playlists/create-playlist/
        /// </remarks>
        public async Task<T> CreatePlaylist<T>(
            string userId,
            PlaylistDetails details,
            string accessToken = null)
        {
            if (string.IsNullOrWhiteSpace(userId)) throw new
                    ArgumentException("A valid Spotify user id must be specified.");

            if (details == null || string.IsNullOrWhiteSpace(details.Name)) throw new
                    ArgumentException("A PlaylistDetails object param with new playlist name must be provided.");

            var builder = new UriBuilder($"{BaseUrl}/users/{userId}/playlists");
            return (await Post<T>(builder.Uri, details, accessToken)).Data;
        }

        #endregion

        #region GetCurrentUsersPlaylists

        /// <summary>
        /// Get a list of the playlists owned or followed by the current Spotify user.
        /// </summary>
        /// <param name="limit">Optional. The maximum number of playlists to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first playlist to return. Default: 0 (the 
        /// first object). Maximum offset: 100,000. Use with limit to get the next set of playlists.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.</param>
        /// <returns>Task of <see cref="PagedPlaylists"/></returns>
        public Task<PagedPlaylists> GetCurrentUsersPlaylists(
            int? limit = null,
            int offset = 0,
            string accessToken = null) => throw new NotImplementedException();

        /// <summary>
        /// Get a list of the playlists owned or followed by the current Spotify user.
        /// </summary>
        /// <param name="limit">Optional. The maximum number of playlists to return. Default: 20. Minimum: 1. Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first playlist to return. Default: 0 (the 
        /// first object). Maximum offset: 100,000. Use with limit to get the next set of playlists.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.</param>
        /// <typeparam name="T">Optional. Type to deserialise response to.</typeparam>
        /// <returns>Task of T</returns>
        public Task<T> GetCurrentUsersPlaylists<T>(
            int? limit = null,
            int offset = 0,
            string accessToken = null) => throw new NotImplementedException();

        #endregion

        #region GetPlaylistCoverImage

        /// <summary>
        /// Get the current image associated with a specific playlist.
        /// </summary>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.</param>
        /// <returns>Task of <see cref="Image[]"/></returns>
        public Task<Image[]> GetPlaylistCoverImage(
            string playlistId,
            string accessToken = null) => throw new NotImplementedException();

        /// <summary>
        /// Get the current image associated with a specific playlist.
        /// </summary>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.</param>
        /// <typeparam name="T">Optional. Type to deserialise response to.</typeparam>
        /// <returns>Task of T</returns>
        public Task<T> GetPlaylistCoverImage<T>(
            string playlistId,
            string accessToken = null) => throw new NotImplementedException();

        #endregion

        #region RemoveItems

        /// <summary>
        /// Remove one or more items from a user’s playlist.
        /// </summary>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="spotifyUris">An array of Spotify URIs of the tracks and episodes to remove.</param>
        /// <param name="snapshotId">Optional. The playlist’s snapshot ID against which you want to 
        /// make the changes. The API will validate that the specified items exist and in the specified 
        /// positions and make the changes, even if more recent changes have been made to the playlist.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.</param>
        /// <returns>Task of <see cref="ModifyPlaylistResponse"/></returns>
        public Task<ModifyPlaylistResponse> RemoveItems(
            string playlistId,
            string[] spotifyUris,
            string snapshotId = null,
            string accessToken = null) => throw new NotImplementedException();

        /// <summary>
        /// Remove one or more items from a user’s playlist.
        /// </summary>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="spotifyUriPositions">An array of tuples containing Spotify URIs of the tracks 
        /// and episodes to remove with their current positions in the playlist of the tracks and 
        /// episodes to remove.</param>
        /// <param name="snapshotId">Optional. The playlist’s snapshot ID against which you want to 
        /// make the changes. The API will validate that the specified items exist and in the specified 
        /// positions and make the changes, even if more recent changes have been made to the playlist.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.</param>
        /// <returns>Task of <see cref="ModifyPlaylistResponse"/></returns>
        public Task<ModifyPlaylistResponse> RemoveItems(
            string playlistId,
            (string uri, int[] positions)[] spotifyUriPositions,
            string snapshotId = null,
            string accessToken = null) => throw new NotImplementedException();

        #endregion

        #region ReorderItems

        /// <summary>
        /// Reorder an item or a group of items in a playlist.
        /// </summary>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="rangeStart">Required. The position of the first item to be reordered.</param>
        /// <param name="insertBefore">Required. The position where the items should be inserted.
        /// To reorder the items to the end of the playlist, simply set <paramref name="insertBefore"/> 
        /// to the position after the last item. Examples: To reorder the first item to the last position 
        /// in a playlist with 10 items, set <paramref name="rangeStart"/> to 0, and <paramref name="insertBefore"/> 
        /// to 10. To reorder the last item in a playlist with 10 items to the start of the playlist, 
        /// set <paramref name="rangeStart"/> to 9, and <paramref name="insertBefore"/> to 0.</param>
        /// <param name="rangeLength">Optional. The amount of items to be reordered. Defaults to 1 if 
        /// not set. The range of items to be reordered begins from the <paramref name="rangeStart"/> 
        /// position, and includes the <paramref name="rangeLength"/> subsequent items. Example: To move the items at 
        /// index 9-10 to the start of the playlist, <paramref name="rangeStart"/> is set to 9, and 
        /// <paramref name="rangeLength"/> is set to 2.</param>
        /// <param name="snapshotId">Optional. The playlist’s snapshot ID against which you want to 
        /// make the changes. The API will validate that the specified items exist and in the specified 
        /// positions and make the changes, even if more recent changes have been made to the playlist.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.</param>
        /// <returns>Task of <see cref="ModifyPlaylistResponse"/></returns>
        public Task<ModifyPlaylistResponse> ReorderItems(
            string playlistId,
            int rangeStart,
            int insertBefore,
            int rangeLength = 1,
            string snapshotId = null,
            string accessToken = null) => throw new NotImplementedException();

        #endregion

        #region ReplaceItems

        /// <summary>
        /// Replace all the items in a playlist, overwriting its existing items. This powerful request 
        /// can be useful for replacing items, re-ordering existing items, or clearing the playlist.
        /// </summary>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <param name="spotifyUris">Optional. A comma-separated list of Spotify URIs to set, can 
        /// be track or episode URIs. A maximum of 100 items can be set in one request.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service.</param>
        public Task ReplaceItems(
            string playlistId,
            string[] spotifyUris,
            string accessToken = null) => throw new NotImplementedException();

        #endregion

    }
}
