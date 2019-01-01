using System;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    public interface IAccessTokenProvider
    {
        Task<string> GetAccessToken();
    }
}
