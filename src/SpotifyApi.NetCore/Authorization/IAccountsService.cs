using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    public interface IUserAccountsService : IAccountsService
    {
        string AuthorizeUrl(string state, string[] scopes);

        Task<BearerAccessRefreshToken> RequestAccessRefreshToken(string userHash, string code);
        
        /// <summary>
        /// Returns a bearer access token to use for a Request to the Spotify API for a specific User.
        /// </summary>
        /// <returns>A Bearer token as (awaitable) Task of BearerAccessToken</returns>
        Task<BearerAccessToken> GetUserAccessToken(string userHash);
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