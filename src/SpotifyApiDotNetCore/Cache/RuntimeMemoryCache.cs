using System;
using System.Diagnostics;
using Microsoft.Extensions.Caching.Memory;

namespace SpotifyApiDotNetCore.Cache
{
    /// <summary>
    /// A Cache service based on <see cref="ObjectCache"/>
    /// </summary>
    public class RuntimeMemoryCache : ICache
    {
        private readonly MemoryCache _cache;
        //MemoryCache myCache = new MemoryCache(new MemoryCacheOptions());
        /// <summary>
        /// Instantiates a new <see cref="RuntimeMemoryCache"/>
        /// </summary>
        /// <param name="objectCache"></param>
        public RuntimeMemoryCache(MemoryCache objectCache)
        {
            _cache = objectCache;
        }

        /// <summary>
        /// Adds a value to the Cache.
        /// </summary>
        public void Add(string key, object value, DateTime absoluteExpiration)
        {
            _cache.Set(key, value, absoluteExpiration);
            Trace.TraceInformation("Added cache item " + key);
        }

        /// <summary>
        /// Gets a value from the Cache by key.
        /// </summary>
        public object Get(string key)
        {
            var value = _cache.Get(key);
            Trace.TraceInformation("Got cache value " + key);
            return value;
        }
    }
}