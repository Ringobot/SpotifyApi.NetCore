using System;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    // https://docs.microsoft.com/en-nz/dotnet/api/system.collections.concurrent.concurrentdictionary-2?view=netframework-4.7.1#remarks
    
    public interface ITokenStore<T> where T: BearerAccessToken
    {   
        Task InsertOrReplace(string key, T token);
        
        Task<T> Get(string key);

        Task Update(string key, T token);
    }
}