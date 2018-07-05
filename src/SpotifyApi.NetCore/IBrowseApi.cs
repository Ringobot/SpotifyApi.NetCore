using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Defines a wrapper for the Spotify Web Artists API.
    /// </summary>
    public interface IBrowseApi
    {
        Task<dynamic> GetRecommendations(string[] seedArtists, string[] seedGenres, string[] seedTracks);
        Task<dynamic> GetRecommendations(string[] seedArtists, string[] seedGenres, string[] seedTracks, int limit);
    }
}
