using System;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore.Authorization
{
    public interface IUserAccountsService : IAccountsService
    {
        string AuthorizeUrl(string state, string[] scopes);

        //Task<BearerAccessRefreshToken> RequestAccessRefreshToken(string userHash, string code);

        Task<BearerAccessRefreshToken> RequestAccessRefreshToken(string code);

        Task<BearerAccessToken> RefreshUserAccessToken(string refreshToken);
    }

    public interface IAccountsService : IAccessTokenProvider
    {        
    }
}