using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore.Authorization
{
    // https://docs.microsoft.com/en-nz/dotnet/api/system.collections.concurrent.concurrentdictionary-2?view=netframework-4.7.1#remarks
    
    internal class MemoryTokenStore<T> : ITokenStore<T> where T: BearerAccessToken
    {   
        private readonly ConcurrentDictionary<string, T> _store = new ConcurrentDictionary<string, T>();

        public Task InsertOrReplace(string key, T token)
        {
            _store[key] = token;
            return Task.CompletedTask;
        }
        
        public Task<T> Get(string key)
        {
            T value = null;
            _store.TryGetValue(key, out value);
            return Task.FromResult(value);
        }

        public Task Update(string key, T token)
        {
            if (!_store.ContainsKey(key)) throw new InvalidOperationException($"No token \"{key}\" found to update");
            _store[key] = token;
            return Task.CompletedTask;
        }
    }
}