using System;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore.Authorization
{
    public interface IAccessTokenProvider
    {
        Task<string> GetAccessToken();
    }
}
