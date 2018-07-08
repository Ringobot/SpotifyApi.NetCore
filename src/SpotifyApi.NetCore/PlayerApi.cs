using System;
using System.Dynamic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpotifyApi.NetCore.Http;

namespace SpotifyApi.NetCore
{
    public class PlayerApi : SpotifyWebApi, IPlayerApi
    {
        public PlayerApi(HttpClient httpClient, IAuthorizationApi authorizationApi) : base(httpClient, authorizationApi)
        {
        }

        public async Task Play(string userHash, object data, string deviceId = null)
        {
            // url
            string url = $"{BaseUrl}/me/player/play";
            if (deviceId != null) url += $"?device_id={deviceId}";

            await Put(url, userHash, data);
        }

        public async Task PlayContext(string userHash, string spotifyUri, string offsetTrackUri = null, string deviceId = null)
        {
            dynamic data = new JObject(new {context_uri = spotifyUri});
            if (offsetTrackUri != null) data.offset = new JObject(new {uri = offsetTrackUri});

            await Play(userHash, data, deviceId);
        }

        public async Task PlayContext(string userHash, string spotifyUri, int offsetPosition = 0, string deviceId = null)
        {
            dynamic data = new JObject(new {context_uri = spotifyUri});
            if (offsetPosition > 0) data.offset = new JObject(new {position = offsetPosition});
            
            await Play(userHash, data, deviceId);
        }

        public async Task PlayTracks(string userHash, string[] spotifyTrackUris, string offsetTrackUri = null, string deviceId = null)
        {
            dynamic data = new JObject(new {uris = spotifyTrackUris});
            if (offsetTrackUri != null) data.offset = new JObject(new {uri = offsetTrackUri});
            
            await Play(userHash, data, deviceId);
        }

        public async Task PlayTracks(string userHash, string[] spotifyTrackUris, int offsetPosition = 0, string deviceId = null)
        {
            dynamic data = new JObject(new {uris = spotifyTrackUris});
            if (offsetPosition > 0) data.offset = new JObject(new {position = offsetPosition});
            
            await Play(userHash, data, deviceId);
        }

        /// <summary>
        /// Helper to PUT an object as JSON body
        /// </summary>
        protected internal virtual async Task<HttpResponseMessage> Put(string url, string userHash, object data)
        {
            // TODO: Could cause unusual effects if multiple threads mix client auth and user auth?
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _auth.GetAccessToken(userHash));
            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await _http.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
