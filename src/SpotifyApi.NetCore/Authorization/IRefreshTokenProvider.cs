using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    public interface IRefreshTokenProvider
    {           
        Task<string> GetRefreshToken(string userId);
    }
}