using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore.Authorization
{
    // https://docs.microsoft.com/en-nz/dotnet/api/system.collections.concurrent.concurrentdictionary-2?view=netframework-4.7.1#remarks

    /// <summary>
    /// An internal, in-memory Bearer Token cache store for use with <see cref="AccountsService"/>.
    /// </summary>
    internal class MemoryBearerTokenStore : IBearerTokenStore
    {
        private readonly ConcurrentDictionary<string, BearerAccessToken> _store = new ConcurrentDictionary<string, BearerAccessToken>();

        /// <summary>
        /// Inserts or replaces a <see cref="BearerAccessToken"/>.
        /// </summary>
        /// <param name="key">The identifier key for the token.</param>
        /// <param name="token">The token as <see cref="BearerAccessToken"/>.</param>
        public Task InsertOrReplace(string key, BearerAccessToken token)
        {
            _store[key] = token;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Get a <see cref="BearerAccessToken"/> by its key.
        /// </summary>
        public Task<BearerAccessToken> Get(string key)
        {
            _store.TryGetValue(key, out BearerAccessToken value);
            return Task.FromResult(value);
        }
    }
}