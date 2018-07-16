using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Defines a wrapper for the Spotify Web Artists API.
    /// </summary>
    public interface IArtistsApi
    {
        Task<dynamic> GetArtist(string artistId);
        Task<dynamic> GetRelatedArtists(string artistId);
        Task<dynamic> SearchArtists(string artist);
        Task<dynamic> SearchArtists(string artist, int limit);
        Task<dynamic> SearchArtists(string artist, (int limit, int offset) limitOffset);
    }
}
