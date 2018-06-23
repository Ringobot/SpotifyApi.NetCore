using System;

namespace SpotifyApiDotNetCore.Cache
{
    /// <summary>
    /// Defines a Cache object.
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// Adds a value to the Cache.
        /// </summary>
        void Add(string key, object value, DateTime absoluteExpiration);
        
        /// <summary>
        /// Gets a value from the Cache by key.
        /// </summary>
        object Get(string key);
    }
}
