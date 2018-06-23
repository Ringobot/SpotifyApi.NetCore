using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpotifyApiDotNetCore.Http;

namespace SpotifyApiDotNetCore
{
    /// <summary>
    /// An API Wrapper for the Spotify Web API Playlists endpoints.
    /// </summary>
    public class PlaylistsApi : IPlaylistsApi
    {
        internal const string BaseUrl = "https://api.spotify.com/v1";
        private readonly IHttpClient _http;
        private readonly IAuthorizationApi _auth;

        public PlaylistsApi(IHttpClient httpClient, IAuthorizationApi authorizationApi)
        {
            _http = httpClient;
            _auth = authorizationApi;
        }

        /// <summary>
        /// Get a list of a user's playlists.
        /// </summary>
        /// <returns>The JSON result deserialized to object (as dynamic).</returns>
        public async Task<dynamic> GetPlaylists(string username)
        {
            const string urlFormat = BaseUrl + "/users/{0}/playlists";

            if (string.IsNullOrEmpty(username)) throw new ArgumentNullException("username");

            string json = await _http.Get(string.Format(urlFormat, Uri.EscapeDataString(username)),
                new AuthenticationHeaderValue("Bearer", await _auth.GetAccessToken()));
            var playlists = JsonConvert.DeserializeObject(json);
            Trace.TraceInformation("Got Playlists");

            return playlists;
        }

        public Task<dynamic> GetPlaylist(string username, string playlistId)
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
        public async Task<dynamic> GetTracks(string username, string playlistId)
        {
            // /users/{user_id}/playlists/{playlist_id}/tracks
            const string urlFormat = BaseUrl + "/users/{0}/playlists/{1}/tracks";

            if (string.IsNullOrEmpty(username)) throw new ArgumentNullException("username");
            if (string.IsNullOrEmpty(playlistId)) throw new ArgumentNullException("playlistId");

            string json =
                await
                    _http.Get(
                        string.Format(urlFormat, Uri.EscapeDataString(username), Uri.EscapeDataString(playlistId)),
                        new AuthenticationHeaderValue("Bearer", await _auth.GetAccessToken()));
            var tracks = JsonConvert.DeserializeObject(json);
            Trace.TraceInformation("Got Tracks");

            return tracks;
        }
    }
}
