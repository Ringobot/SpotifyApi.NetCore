using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SpotifyApi.NetCore.Http;

namespace SpotifyApi.NetCore
{
    public class ArtistsApi : SpotifyWebApi, IArtistsApi
    {
        public ArtistsApi(HttpClient httpClient, IAuthorizationApi authorizationApi) : base(httpClient, authorizationApi)
        {
        }

        public async Task<dynamic> GetArtist(string artistId)
        {
            return await Get<dynamic>($"{BaseUrl}/artists/{artistId}");
        }

        public async Task<dynamic> GetRelatedArtists(string artistId)
        {
            return await Get<dynamic>($"{BaseUrl}/artists/{artistId}/related-artists");
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

            return await Get<dynamic>($"{BaseUrl}/search?q={Uri.EscapeDataString(artist)}&type=artist&limit={limit}");
        }
    }
}
