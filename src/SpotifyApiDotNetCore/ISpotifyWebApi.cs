using System;
using System.Threading.Tasks;

namespace SpotifyApiDotNetCore
{
    public interface ISpotifyWebApi
    {
        Task<dynamic> SearchArtists(string artist);
        Task<dynamic> SearchArtists(string artist, int limit);

        Task<dynamic> GetPlaylists(string username);
        Task<dynamic> GetPlaylists(string username, int offset);

        Task<dynamic> GetRecommendation(string artistSeed);
        Task<dynamic> GetRecommendation(string artistSeed, int limit);

        Task<dynamic> GetRelatedArtists(string artistId);

        Task<dynamic> GetArtist(string artistId);

        Task PlayArtist(string userHash,  string spotifyUri);
    }
}
