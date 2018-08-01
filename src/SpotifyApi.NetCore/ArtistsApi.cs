using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    public class ArtistsApi : SpotifyWebApi, IArtistsApi
    {
        public ArtistsApi(HttpClient httpClient, IAccountsService accountsService) : base(httpClient, accountsService)
        {
        }

        public async Task<Artist> GetArtist(string artistId) => await GetArtist<Artist>(artistId);

        public async Task<T> GetArtist<T>(string artistId) => await Get<T>($"{BaseUrl}/artists/{artistId}");

        public async Task<Artist[]> GetRelatedArtists(string artistId) => await GetRelatedArtists<Artist[]>(artistId);

        public async Task<T> GetRelatedArtists<T>(string artistId) => await Get<T>($"{BaseUrl}/artists/{artistId}/related-artists");

        public async Task<Artist[]> SearchArtists(string artist) => await SearchArtists(artist, (0, 0));
        public async Task<Artist[]> SearchArtists(string artist, int limit)
        {
            return await SearchArtists(artist, (limit, 0));
        }
        public async Task<Artist[]> SearchArtists(string artist, (int limit, int offset) limitOffset) => await SearchArtists<Artist[]>(artist, limitOffset);

        public async Task<T> SearchArtists<T>(string artist, (int limit, int offset) limitOffset)
        {
            string url = $"{BaseUrl}/search?q={Uri.EscapeDataString(artist)}&type=artist";

            if (limitOffset.limit > 0 && limitOffset.limit <= 50)
            {
                url += $"&limit={limitOffset.limit}";
            }

            if (limitOffset.offset > 0 && limitOffset.offset <= 10000)
            {
                url += $"&offset={limitOffset.offset}";
            }

            return await Get<T>(url);
        }
    }
}
