using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Defines a wrapper for the Spotify Web Artists API.
    /// </summary>
    public interface IBrowseApi
    {
        Task<RecommendationsResult> GetRecommendations(string[] seedArtists, string[] seedGenres, string[] seedTracks);
        Task<RecommendationsResult> GetRecommendations(string[] seedArtists, string[] seedGenres, string[] seedTracks, int limit);
        Task<T> GetRecommendations<T>(string[] seedArtists, string[] seedGenres, string[] seedTracks, int limit);
    }
}
