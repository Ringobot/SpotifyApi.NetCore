﻿using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpotifyApi.NetCore.Http;

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

        /// <summary>
        /// Get a list of a user's playlists.
        /// </summary>
        /// <returns>The JSON result deserialized to object (as dynamic).</returns>
        public async Task<T> GetPlaylists<T>(string username)
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentNullException("username");
            return await GetModel<T>($"{BaseUrl}/users/{Uri.EscapeDataString(username)}/playlists");
        }

        public async Task<PlaylistsResult> GetPlaylists(string username)
            => await GetPlaylists<PlaylistsResult>(username);

        public async Task<Playlist> GetPlaylist(string username, string playlistId)
            => await GetPlaylist<Playlist>(username, playlistId);

        public Task<T> GetPlaylist<T>(string username, string playlistId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get full details of the tracks of a playlist owned by a Spotify user.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="username">The user's Spotify user ID.</param>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <returns></returns>
        public async Task<PlaylistTracksResult> GetTracks(string username, string playlistId)
            => await GetTracks<PlaylistTracksResult>(username, playlistId);

        /// <summary>
        /// Get full details of the tracks of a playlist owned by a Spotify user.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="username">The user's Spotify user ID.</param>
        /// <param name="playlistId">The Spotify ID for the playlist.</param>
        /// <returns></returns>

        public async Task<T> GetTracks<T>(string username, string playlistId)
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentNullException("username");
            if (string.IsNullOrEmpty(playlistId)) throw new ArgumentNullException("playlistId");

            return await GetModel<T>($"{BaseUrl}/users/{Uri.EscapeDataString(username)}/playlists/{Uri.EscapeDataString(playlistId)}/tracks");
        }

        #region SearchPlaylists

        /// <summary>
        /// Get Spotify Catalog information about tracks that match a keyword string.
        /// </summary>
        /// <param name="query">Search query keywords and optional field filters and operators. See
        /// https://developer.spotify.com/documentation/web-api/reference/search/search/#writing-a-query---guidelines</param>
        /// <returns>Task of <see cref="SearchResult" /></returns>
        public async Task<SearchResult> SearchPlaylists(string query)
            => await SearchApi.Search<SearchResult>(query, new string[] { SpotifySearchTypes.Playlist }, null, (0, 0));

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
        /// <returns>Task of <see cref="SearchResult" /></returns>
        public async Task<SearchResult> SearchPlaylists(string query, (int limit, int offset) limitOffset)
            => await SearchApi.Search<SearchResult>(query, new string[] { SpotifySearchTypes.Playlist }, null, limitOffset);

        #endregion

    }
}
