using System.Threading.Tasks;

namespace SpotifyApi.NetCore.Authorization
{
    public interface IRefreshTokenProvider
    {           
        Task<string> GetRefreshToken(string userId);
    }
}