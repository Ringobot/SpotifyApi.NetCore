using System;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    internal class SpotifyWebApi : ISpotifyWebApi
    {
        public Task<dynamic> SearchArtists(string artist)
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> SearchArtists(string artist, int limit = 50)
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> GetPlaylists(string username)
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> GetPlaylists(string username, int offset = 0)
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> GetRecommendation(string artistSeed)
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> GetRecommendation(string artistSeed, int limit = 20)
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> GetRelatedArtists(string artistId)
        {
            throw new NotImplementedException();
        }

        public Task<dynamic> GetArtist(string artistId)
        {
            throw new NotImplementedException();
        }

        public Task PlayArtist(string userHash, string spotifyUri)
        {
            throw new NotImplementedException();
        }
    }
}