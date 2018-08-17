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
        private readonly ConcurrentDictionary<string, UserAuth> _userAuths = new ConcurrentDictionary<string, UserAuth>();

        public UserAuth GetUserAuth(string userId)
        {
            UserAuth value = null;
            _userAuths.TryGetValue(userId, out value);
            return value;
        }

        public string CreateAndStoreState(string userId) 
        {
            // Store the state
            string state = Guid.NewGuid().ToString("N");
            _states.TryAdd(state, userId);

            // Create the User Auth
            var userAuth = GetUserAuth(userId);
            userAuth = userAuth ?? new UserAuth
            {
                Authorized=false,
                UserId = userId
            };
            
            InsertOrUpdateUserAuth(userId, userAuth);
            
            return state;
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
            => (_userAuths.Any(u=>u.Value.UserId == userHash)) 
                ? _userAuths.Where(u=>u.Value.UserId == userHash).First().Value
                : null;

        public UserAuth SetUserAuthRefreshToken(string userId, BearerAccessRefreshToken tokens)
        {
            //TODO: No concurrency checking. Blows away any existing record
            var userAuth = GetByUserHash(userId);
            if (userAuth == null) throw new InvalidOperationException($"No valid User Auth record found for user hash \"{userId}\"");
            userAuth.Authorized = true;
            userAuth.RefreshToken = tokens.RefreshToken;
            userAuth.Scopes = tokens.Scope;
            InsertOrUpdateUserAuth(userId, userAuth);
            return userAuth;
        }

        private void InsertOrUpdateUserAuth(string spotifyUserName, UserAuth userAuth)
        {
            userAuth.EnforceInvariants();
            _userAuths[spotifyUserName] = userAuth;
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