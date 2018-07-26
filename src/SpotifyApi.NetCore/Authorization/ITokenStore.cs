using System;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    // https://docs.microsoft.com/en-nz/dotnet/api/system.collections.concurrent.concurrentdictionary-2?view=netframework-4.7.1#remarks
    
    public interface IBearerTokenStore
    {   
        Task InsertOrReplace(string key, BearerAccessToken token);
        
        Task<BearerAccessToken> Get(string key);

        Task Update(string key, BearerAccessToken token);
    }

    public interface IRefreshTokenStore
    {   
        Task InsertOrReplace(string userHash, string token);
        
        Task<string> Get(string userHash);
    }
}