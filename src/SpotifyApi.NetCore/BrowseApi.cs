using System;
using System.Net.Http;
using System.Threading.Tasks;
using SpotifyApi.NetCore.Http;

namespace SpotifyApi.NetCore
{
    public class BrowseApi : SpotifyWebApi, IBrowseApi
    {
        public BrowseApi(HttpClient httpClient, IAccountsService accountsService) : base(httpClient, accountsService)
        {
        }

        public async Task<RecommendationsResult> GetRecommendations(string[] seedArtists, string[] seedGenres, string[] seedTracks) 
            => await GetRecommendations<RecommendationsResult>(seedArtists, seedGenres, seedTracks, 0);

        public async Task<RecommendationsResult> GetRecommendations(string[] seedArtists, string[] seedGenres, string[] seedTracks, int limit) 
            => await GetRecommendations<RecommendationsResult>(seedArtists, seedGenres, seedTracks, limit);

        public async Task<T> GetRecommendations<T>(string[] seedArtists, string[] seedGenres, string[] seedTracks, int limit)
        {
            string url = $"{BaseUrl}/recommendations?";

            if (seedArtists != null && seedArtists.Length > 0) url += $"seed_artists={string.Join(",", seedArtists)}&";
            if (seedGenres != null && seedGenres.Length > 0) url += $"seed_genres={string.Join(",", seedGenres)}&";
            if (seedTracks != null && seedTracks.Length > 0) url += $"seed_tracks={string.Join(",", seedTracks)}&";
            if (limit > 0 ) url += $"limit={limit}";

            return await GetModel<T>(url);
        }
    }
}
