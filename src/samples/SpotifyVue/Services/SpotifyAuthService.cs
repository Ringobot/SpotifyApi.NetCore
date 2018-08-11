using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using SpotifyApi.NetCore;
using SpotifyApi.NetCore.Authorization;
using SpotifyVue.Models;

namespace SpotifyVue.Services
{
    public class SpotifyAuthService: IRefreshTokenStore
    {        
        /// A dictionary of state, userHash for verification of authorization callback
        /// IRL this would be Redis, Table Storage or Cosmos DB
        private readonly ConcurrentDictionary<string, string> _states = new ConcurrentDictionary<string, string>();

        // IRL this would be Table / Cosmos Db
        private readonly ConcurrentDictionary<string, UserAuth> _storage = new ConcurrentDictionary<string, UserAuth>();

        public UserAuth GetUserAuth(string spotifyUserName)
        {
            UserAuth value = null;
            _storage.TryGetValue(spotifyUserName, out value);
            return value;
        }

        public string CreateAndStoreState(string spotifyUserName) 
        {
            // Store the state
            string state = Guid.NewGuid().ToString("N");
            string userHash = StateHelper.UserHash(spotifyUserName);
            _states.TryAdd(state, userHash);

            // Create the User Auth
            var userAuth = GetUserAuth(spotifyUserName);
            userAuth = userAuth ?? new UserAuth
            {
                Authorized=false,
                SpotifyUserName = spotifyUserName,
                UserHash = userHash
            };
            InsertOrUpdateUserAuth(userAuth.SpotifyUserName, userAuth);
            
            // encode the userHash and state into one "state" string
            return StateHelper.EncodeState((userHash, state));
        }

        /// returns userHash
        public string DecodeAndValidateState(string state)
        {
            // decode the state param
            var userState = StateHelper.DecodeState(state);

            string userHash = null;
            // if no state found
            if (!_states.TryGetValue(userState.state, out userHash)) throw new InvalidOperationException("state param is invalid");
            // if stored userHash does not match decoded userHash
            if (userHash != userState.userHash) throw new InvalidOperationException("state param is invalid");

            return userHash;
        }

        public UserAuth GetByUserHash(string userHash) 
            => (_storage.Any(u=>u.Value.UserHash == userHash)) 
                ? _storage.Where(u=>u.Value.UserHash == userHash).First().Value
                : null;

        public UserAuth SetUserAuthRefreshToken(string userHash, BearerAccessRefreshToken tokens)
        {
            //TODO: No concurrency checking. Blows away any existing record
            var userAuth = GetByUserHash(userHash);
            if (userAuth == null) throw new InvalidOperationException($"No valid User Auth record found for user hash \"{userHash}\"");
            userAuth.Authorized = true;
            userAuth.RefreshToken = tokens.RefreshToken;
            userAuth.Scopes = tokens.Scope;
            InsertOrUpdateUserAuth(userAuth.SpotifyUserName, userAuth);
            return userAuth;
        }

        private void InsertOrUpdateUserAuth(string spotifyUserName, UserAuth userAuth)
        {
            userAuth.EnforceInvariants();
            _storage[spotifyUserName] = userAuth;
        }

        Task IRefreshTokenStore.InsertOrReplace(string userHash, string token)
        {
            return Task.CompletedTask;
        }

        Task<string> IRefreshTokenStore.Get(string userHash)
        {
            return Task.FromResult(GetByUserHash(userHash).RefreshToken);
        }
    }
}