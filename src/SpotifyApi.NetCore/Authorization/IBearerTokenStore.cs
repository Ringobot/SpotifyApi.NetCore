using System;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore.Authorization
{
    public interface IBearerTokenStore
    {   
        Task InsertOrReplace(string key, BearerAccessToken token);
        
        Task<BearerAccessToken> Get(string key);
    }

    public interface IRefreshTokenStore
    {   
        Task InsertOrReplace(string userHash, string token);
        
        Task<string> Get(string userHash);
    }
}