using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    // https://docs.microsoft.com/en-nz/dotnet/api/system.collections.concurrent.concurrentdictionary-2?view=netframework-4.7.1#remarks
    
    public class MemoryRefreshTokenStore : IRefreshTokenStore 
    {   
        private readonly ConcurrentDictionary<string, string> _store;

        public MemoryRefreshTokenStore()
        {
            _store = new ConcurrentDictionary<string, string>();
        }

        public MemoryRefreshTokenStore(IEnumerable<KeyValuePair<string, string>> collection)
        {
            _store = new ConcurrentDictionary<string, string>(collection);
        }

        public Task InsertOrReplace(string userHash, string token)
        {
            _store[userHash] = token;
            return Task.CompletedTask;
        }
        
        public Task<string> Get(string userHash)
        {
            string value = null;
            _store.TryGetValue(userHash, out value);
            return Task.FromResult(value);
        }
    }
}