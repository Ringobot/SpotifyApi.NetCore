using System.Collections.Concurrent;
using SpotifyVue.Models;
using System.Linq;
using System;

namespace SpotifyVue.Data
{
    public class UserAuthStorage
    {
        // IRL this would be Table / Cosmos Db
        private readonly ConcurrentDictionary<string, UserAuth> _storage = new ConcurrentDictionary<string, UserAuth>();

        public UserAuth Get(string spotifyUserName)
        {
            UserAuth value = null;
            _storage.TryGetValue(spotifyUserName, out value);
            return value;
        }

        public UserAuth GetByUserHash(string userHash)
        {
            //TODO: Ugh
            return _storage.Where(u=>u.Value.UserHash == userHash).First().Value;
        }

        internal void Update(string spotifyUserName, UserAuth userAuth)
        {
            //TODO: Concurrency checking
            _storage[spotifyUserName] = userAuth;
        }
    }
}