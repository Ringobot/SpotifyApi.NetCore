using System;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    public interface IPlayerApi
    {
        Task PlayArtist(string userHash, string spotifyUri);
    }
}
