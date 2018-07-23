using System;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    public interface ITokenStore<T> where T: BearerAccessToken
    {   
        Task InsertOrReplace(string key, T token);
        
        Task<T> Get(string key);

        Task<bool> TryUpdate(string key, T newToken, T comparisonToken);
    }
}