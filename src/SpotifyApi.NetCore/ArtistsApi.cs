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
            return await SearchArtists(artist, (0, 0));
        }
        public async Task<dynamic> SearchArtists(string artist, int limit)
        {
            return await SearchArtists(artist, (limit, 0));
        }

        public async Task<dynamic> SearchArtists(string artist, (int limit, int offset) limitOffset)
        {
            var token = await _auth.GetAccessToken();

            string url = $"{BaseUrl}/search?q={Uri.EscapeDataString(artist)}&type=artist";

            if (limitOffset.limit > 0 && limitOffset.limit <= 50)
            {
                url += $"&limit={limitOffset.limit}";
            }

            if (limitOffset.offset > 0 && limitOffset.offset <= 10000)
            {
                url += $"&offset={limitOffset.offset}";
            }

            return await Get<dynamic>(url);
        }
    }
}
