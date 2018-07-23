using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace SpotifyApi.NetCore
{
    public interface IAccountsService
    {
        string AuthorizeUrl(string state);

        Task<BearerAccessRefreshToken> RequestAccessRefreshToken(string userHash, string code);
        
        /// <summary>
        /// Returns a bearer access token to use for a Request to the Spotify API.
        /// </summary>
        /// <returns>A Bearer token as (awaitable) Task of BearerAccessToken</returns>
        Task<BearerAccessToken> GetAppAccessToken();

        /// <summary>
        /// Returns a bearer access token to use for a Request to the Spotify API for a specific User.
        /// </summary>
        /// <returns>A Bearer token as (awaitable) Task of BearerAccessToken</returns>
        Task<BearerAccessToken> GetUserAccessToken(string userHash);

    }
}