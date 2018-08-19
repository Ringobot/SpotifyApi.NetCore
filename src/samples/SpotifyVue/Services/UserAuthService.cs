using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using SpotifyApi.NetCore;
using SpotifyApi.NetCore.Authorization;
using SpotifyVue.Models;

namespace SpotifyVue.Services
{
    public class UserAuthService: IRefreshTokenProvider
    {   
        // IRL this would be Table / Cosmos Db
        private readonly ConcurrentDictionary<string, UserAuth> _userAuths = new ConcurrentDictionary<string, UserAuth>();

        public UserAuth CreateUserAuth (string userId)
        {
            // Create the User Auth
            var userAuth = GetUserAuth(userId);
            userAuth = userAuth ?? new UserAuth
            {
                Authorized=false,
                UserId = userId
            };
            
            InsertOrUpdateUserAuth(userId, userAuth);

            return userAuth;
        }

        public Task<string> GetRefreshToken(string userId)
        {
            return Task.FromResult(GetUserAuth(userId).RefreshToken);
        }

        public UserAuth GetUserAuth(string userId)
        {
            UserAuth value = null;
            _userAuths.TryGetValue(userId, out value);
            return value;
        }

        public UserAuth SetUserAuthRefreshToken(string userId, BearerAccessRefreshToken tokens)
        {
            //TODO: No concurrency checking. Blows away any existing record
            var userAuth = GetUserAuth(userId);
            if (userAuth == null) throw new InvalidOperationException($"No valid User Auth record found for user hash \"{userId}\"");
            userAuth.Authorized = true;
            userAuth.RefreshToken = tokens.RefreshToken;
            userAuth.Scopes = tokens.Scope;
            InsertOrUpdateUserAuth(userId, userAuth);
            return userAuth;
        }

        private void InsertOrUpdateUserAuth(string userId, UserAuth userAuth)
        {
            userAuth.EnforceInvariants();
            _userAuths[userId] = userAuth;
        }
    }
}