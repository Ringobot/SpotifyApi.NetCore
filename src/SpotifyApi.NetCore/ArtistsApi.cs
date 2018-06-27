using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpotifyApi.NetCore.Http;

namespace SpotifyApi.NetCore
{
    public class ArtistsApi : SpotifyWebApi, IArtistsApi
    {
        public ArtistsApi(IHttpClient httpClient, IAuthorizationApi authorizationApi) : base(httpClient, authorizationApi)
        {
        }

        public async Task<dynamic> GetArtist(string artistId)
        {
            return JsonConvert.DeserializeObject(
                await _http.Get($"{BaseUrl}/artists/{artistId}",
                    new AuthenticationHeaderValue("Bearer", await _auth.GetAccessToken()))
                );
        }

        public async Task<dynamic> GetRelatedArtists(string artistId)
        {
            return JsonConvert.DeserializeObject(
                await _http.Get($"{BaseUrl}/artists/{artistId}/related-artists",
                    new AuthenticationHeaderValue("Bearer", await _auth.GetAccessToken()))
                );
        }

        public async Task<dynamic> SearchArtists(string artist)
        {
            return await SearchArtists(artist, 50);
        }

        public async Task<dynamic> SearchArtists(string artist, int limit)
        {
            var token = await _auth.GetAccessToken();

            if (limit <= 0)
            {
                limit = 50;
            }

            // https://api.spotify.com/v1/search?q=radiohead&type=artist

            return JsonConvert.DeserializeObject(
                await _http.Get($"{BaseUrl}/search?q={Uri.EscapeDataString(artist)}&type=artist&limit={limit}",
                    new AuthenticationHeaderValue("Bearer", await _auth.GetAccessToken()))
                );
        }
    }
}
