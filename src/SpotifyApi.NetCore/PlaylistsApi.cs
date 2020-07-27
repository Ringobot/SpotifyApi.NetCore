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
            string market = null) => await GetTracks<PlaylistPaged>(
                playlistId,
                accessToken,
                fields,
                limit,
                offset,
                market);

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
            string market = null)
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

        #endregion

        #region AddItemsToPlaylist
        /// <summary>
        /// Add one or more items to a user’s playlist.
        /// </summary>
        /// <param name="playlistId">Required. The Spotify ID for the playlist.</param>
        /// <param name="uris">Required. A JSON array of the Spotify URIs to add, can be track or episode URIs. For example: {"uris": ["spotify:track:4iV5W9uYEdYUVa79Axb7Rh","spotify:track:1301WleyT98MSxVHPZCA6M", "spotify:episode:512ojhOuo1ktJprKbVcKyQ"]} A maximum of 100 items can be added in one request.</param>
        /// <param name="position">Optional. The position to insert the items, a zero-based index. For example, to insert the items in the first position: position=0 ; to insert the items in the third position: position=2. If omitted, the items will be appended to the playlist. Items are added in the order they appear in the uris array. For example: {"uris": ["spotify:track:4iV5W9uYEdYUVa79Axb7Rh","spotify:track:1301WleyT98MSxVHPZCA6M", "spotify:episode:512ojhOuo1ktJprKbVcKyQ"], "position": 3}</param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PlaylistSnapshotID"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/playlists/add-tracks-to-playlist/
        /// </remarks>
        public Task<PlaylistSnapshotID> AddItemsToPlaylist(
            string playlistId,
            string[] uris,
            int? position = null,
            string accessToken = null
            ) => AddItemsToPlaylist<PlaylistSnapshotID>(playlistId, uris, position, accessToken);

        /// <summary>
        /// Add one or more items to a user’s playlist.
        /// </summary>
        /// <param name="playlistId">Required. The Spotify ID for the playlist.</param>
        /// <param name="uris">Required. A JSON array of the Spotify URIs to add, can be track or episode URIs. For example: {"uris": ["spotify:track:4iV5W9uYEdYUVa79Axb7Rh","spotify:track:1301WleyT98MSxVHPZCA6M", "spotify:episode:512ojhOuo1ktJprKbVcKyQ"]} A maximum of 100 items can be added in one request.</param>
        /// <param name="position">Optional. The position to insert the items, a zero-based index. For example, to insert the items in the first position: position=0 ; to insert the items in the third position: position=2. If omitted, the items will be appended to the playlist. Items are added in the order they appear in the uris array. For example: {"uris": ["spotify:track:4iV5W9uYEdYUVa79Axb7Rh","spotify:track:1301WleyT98MSxVHPZCA6M", "spotify:episode:512ojhOuo1ktJprKbVcKyQ"], "position": 3}</param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <returns>A Task that, once successfully completed, returns a full <see cref="PlaylistSnapshotID"/> object.</returns>
        /// <remarks>
        /// https://developer.spotify.com/documentation/web-api/reference/playlists/add-tracks-to-playlist/
        /// </remarks>
        public async Task<T> AddItemsToPlaylist<T>(
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
            return (await Post<T>(builder.Uri, new { uris, position }, accessToken)).Data;
        }

        #endregion

        #region ChangePlaylistDetails
        class PutDataPayloadPlaylistDetails
        {
            [JsonProperty("name")]
            public string Name { get; set; }
            [JsonProperty("public")]
            public bool? Public { get; set; }
            [JsonProperty("collaborative")]
            public bool? Collaborative { get; set; }
            [JsonProperty("description")]
            public string Description { get; set; }

            public PutDataPayloadPlaylistDetails(string name, bool? makePublic, bool? collaborative, string description)
            {
                Name = name;
                Public = makePublic;
                Collaborative = collaborative;
                Description = description;
            }

            public bool ShouldSerializeName()
            {
                return !string.IsNullOrEmpty(Name);
            }

            public bool ShouldSerializePublic()
            {
                return Public != null;
            }

            public bool ShouldSerializeCollaborative ()
            {
                return Collaborative != null;
            }

            public bool ShouldSerializeDescription()
            {
                return !string.IsNullOrEmpty(Description);
            }
        }

        /// <summary>
        /// Change a playlist’s name and public/private state. (The user must, of course, own the playlist.)
        /// </summary>
        /// <param name="playlistId">Required. The Spotify ID for the playlist.</param>
        /// <param name="name">Optional. The new name for the playlist, for example "My New Playlist Title".</param>
        /// <param name="makePublic">Optional. If true the playlist will be public, if false it will be private.</param>
        /// <param name="collaborative">Optional. If true , the playlist will become collaborative and other users will be able to modify the playlist in their Spotify client. Note: You can only set collaborative to true on non-public playlists.</param>
        /// <param name="description">Optional. Value for playlist description as displayed in Spotify Clients and in the Web API.</param>
        /// <param name="accessToken">The bearer token which is gotten during the authentication/authorization process.</param>
        /// <remarks>
        /// At least one optional parameter must be supplied.
        /// https://developer.spotify.com/documentation/web-api/reference/playlists/change-playlist-details/
        /// </remarks>
        public async Task ChangePlaylistDetails(
            string playlistId,
            string name = null,
            bool? makePublic = null,
            bool? collaborative = null,
            string description = null,
            string accessToken = null
            )
        {
            if (string.IsNullOrWhiteSpace(playlistId)) throw new
                    ArgumentException("A valid Spotify playlist id must be specified.");

            if (string.IsNullOrWhiteSpace(name) && makePublic == null &&
                collaborative == null && string.IsNullOrWhiteSpace(description)) throw new
                    ArgumentException("At least one of the optional parameters must be supplied.");

            var builder = new UriBuilder($"{BaseUrl}/playlists/{playlistId}");
            var data = new PutDataPayloadPlaylistDetails(name, makePublic, collaborative, description);
            await Put(builder.Uri, data, accessToken);
        }
        #endregion
    }
}
