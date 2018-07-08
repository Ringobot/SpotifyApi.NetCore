using System;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    public interface IPlayerApi
    {
        Task Play(string userHash, string spotifyUri);
    }
}
