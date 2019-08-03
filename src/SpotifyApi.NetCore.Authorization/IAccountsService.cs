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

    public interface IAccountsService
    {        
        /// <summary>
        /// Returns a bearer access token to use for a Request to the Spotify API.
        /// </summary>
        /// <returns>A Bearer token as (awaitable) Task of BearerAccessToken</returns>
        Task<BearerAccessToken> GetAppAccessToken();
    }
}