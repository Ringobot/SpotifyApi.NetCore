using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Defines a wrapper for the Spotify Web Artists API.
    /// </summary>
    public interface IBrowseApi
    {
        Task<dynamic> GetRecommendation(string[] seedArtists, string[] seedGenres, string[] seedTracks);
        Task<dynamic> GetRecommendation(string[] seedArtists, string[] seedGenres, string[] seedTracks, int limit);
    }
}
