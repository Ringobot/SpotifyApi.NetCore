using System;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore.Authorization
{
    public interface IBearerTokenStore
    {   
        Task InsertOrReplace(string key, BearerAccessToken token);
        
        Task<BearerAccessToken> Get(string key);
    }
}