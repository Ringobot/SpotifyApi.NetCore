using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore.Authorization
{
    /// <summary>
    /// Simple Accounts Service implementation for when bearer token is provided by another service
    /// </summary>
    internal class SimpleAccountsService : IUserAccountsService
    {
        private readonly string _bearerToken;

        public SimpleAccountsService(string bearerToken)
        {
            _bearerToken = bearerToken;
        }

        public string AuthorizeUrl(string state, string[] scopes)
        {
            throw new NotImplementedException();
        }

        public Task<BearerAccessToken> GetAppAccessToken()
        {
            return Task.FromResult(new BearerAccessToken { AccessToken = _bearerToken });
        }

        public Task<BearerAccessToken> GetUserAccessToken(string userHash)
        {
            return GetAppAccessToken();
        }

        public Task<BearerAccessRefreshToken> RequestAccessRefreshToken(string userHash, string code)
        {
            throw new NotImplementedException();
        }

        public Task<BearerAccessRefreshToken> RequestAccessRefreshToken(string code)
        {
            throw new NotImplementedException();
        }
    }
}
