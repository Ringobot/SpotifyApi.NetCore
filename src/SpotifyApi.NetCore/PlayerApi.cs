using System;
using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpotifyApi.NetCore.Http;

namespace SpotifyApi.NetCore
{
    // https://developer.spotify.com/documentation/web-api/reference/player/
    // Implemented: 1/13 = 8%
    public class PlayerApi : SpotifyWebApi, IPlayerApi
    {
        private  readonly IUserAccountsService _userAccounts;
        public PlayerApi(HttpClient httpClient, IUserAccountsService userAccountsService) : base(httpClient, userAccountsService)
        {
            if (userAccountsService == null) throw new ArgumentNullException("userAccountsService");
            _userAccounts = userAccountsService;
        }

        #region PlayContext

        public async Task Play(string userHash, object data, string deviceId = null)
        {
            // url
            string url = $"{BaseUrl}/me/player/play";
            if (deviceId != null) url += $"?device_id={deviceId}";

            await Put(url, userHash, data);
        }

        public async Task PlayContext(string userHash, string spotifyUri, string offsetTrackUri = null, string deviceId = null)
        {
            dynamic data = JObject.FromObject(new {context_uri = spotifyUri});
            if (offsetTrackUri != null) data.offset = JObject.FromObject(new {uri = offsetTrackUri});

            await Play(userHash, data, deviceId);
        }

        public async Task PlayContext(string userHash, string spotifyUri, int offsetPosition = 0, string deviceId = null)
        {
            dynamic data = JObject.FromObject(new {context_uri = spotifyUri});
            if (offsetPosition > 0) data.offset = JObject.FromObject(new {position = offsetPosition});
            
            await Play(userHash, data, deviceId);
        }

        public async Task PlayContext(string userHash, string spotifyUri)
        {
            await PlayContext(userHash, spotifyUri, null, null);
        }

        #endregion

        #region PlayTracks

        public async Task PlayTracks(string userHash, string[] spotifyTrackUris, string offsetTrackUri = null, string deviceId = null)
        {
            dynamic data = JObject.FromObject(new {uris = spotifyTrackUris});
            if (offsetTrackUri != null) data.offset = JObject.FromObject(new {uri = offsetTrackUri});
            
            await Play(userHash, data, deviceId);
        }

        public async Task PlayTracks(string userHash, string[] spotifyTrackUris, int offsetPosition = 0, string deviceId = null)
        {
            dynamic data = JObject.FromObject(new {uris = spotifyTrackUris});
            if (offsetPosition > 0) data.offset = JObject.FromObject(new {position = offsetPosition});
            
            await Play(userHash, data, deviceId);
        }

        #endregion

        #region GetDevices

        public async Task<Device[]> GetDevices(string userHash) => 
            await GetModelFromProperty<Device[]>($"{BaseUrl}/me/player/devices", "devices", 
            (await _userAccounts.GetUserAccessToken(userHash)).AccessToken);

        /// <summary>
        /// Helper to PUT an object as JSON body
        /// </summary>
        protected internal virtual async Task<HttpResponseMessage> Put(string url, string userHash, object data)
        {
            // TODO: Could cause unusual effects if multiple threads mix client auth and user auth?
            _http.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", (await _userAccounts.GetUserAccessToken(userHash)).AccessToken);
            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _http.PutAsync(url, content);

            await RestHttpClient.CheckForErrors(response);            
            
            return response;
        }

        #endregion
        
        #region GetCurrentPlaybackInfo

        /// <summary>
        /// BETA. Get information about the user’s current playback state, including track, track progress, and active device.
        /// </summary>
        /// <param name="market">Optional. A <see cref="SpotifyCountryCodes" /> or the string from_token.
        /// Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns>Task of <see cref="CurrentPlaybackContext"/></returns>
        /// <remarks>The access token must have the `user-read-playback-state` scope authorized in order
        /// to read information.</remarks>
        Task<CurrentPlaybackContext> GetCurrentPlaybackInfo(string market = null);

        /// <summary>
        /// BETA. Get information about the user’s current playback state, including track, track progress, and active device.
        /// </summary>
        /// <param name="market">Optional. A <see cref="SpotifyCountryCodes" /> or the string from_token.
        /// Provide this parameter if you want to apply Track Relinking.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T</returns>
        /// <remarks>The access token must have the `user-read-playback-state` scope authorized in order
        /// to read information.</remarks>
        Task<T> GetCurrentPlaybackInfo<T>(string market = null);

        #endregion

    }
}
