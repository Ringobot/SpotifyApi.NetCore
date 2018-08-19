using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore.Authorization
{
    // https://docs.microsoft.com/en-nz/dotnet/api/system.collections.concurrent.concurrentdictionary-2?view=netframework-4.7.1#remarks
    
    internal class MemoryBearerTokenStore : IBearerTokenStore 
    {   
        private readonly ConcurrentDictionary<string, BearerAccessToken> _store = new ConcurrentDictionary<string, BearerAccessToken>();

        public Task InsertOrReplace(string key, BearerAccessToken token)
        {
            _store[key] = token;
            return Task.CompletedTask;
        }
        
        public Task<BearerAccessToken> Get(string key)
        {
            BearerAccessToken value = null;
            _store.TryGetValue(key, out value);
            return Task.FromResult(value);
        }

        public Task Update(string key, BearerAccessToken token)
        {
            if (!_store.ContainsKey(key)) throw new InvalidOperationException($"No token \"{key}\" found to update");
            _store[key] = token;
            return Task.CompletedTask;
        }
    }
}