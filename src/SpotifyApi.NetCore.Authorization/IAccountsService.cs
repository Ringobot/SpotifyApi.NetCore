using System;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore.Authorization
{
    /// <summary>
    /// Defines a Spotify Accounts Service for the User (Authorization Code) Flow.
    /// </summary>
    /// <remarks>https://developer.spotify.com/documentation/general/guides/authorization-guide/#authorization-code-flow</remarks>
    public interface IUserAccountsService : IAccountsService
    {
        /// <summary>
        /// Derives and returns a URL for a webpage where a user can choose to grant your app access to their data.
        /// </summary>
        /// <param name="state">Optional, but strongly recommended. Random state value to provides protection against 
        /// attacks such as cross-site request forgery. See important notes in <see cref="https://developer.spotify.com/documentation/general/guides/authorization-guide/#authorization-code-flow"/></param>
        /// <param name="scopes">Optional. A space-separated list of scopes.</param>
        /// <returns>A fully qualified URL.</returns>
        string AuthorizeUrl(string state, string[] scopes);

        /// <summary>
        /// Exchange the authorization code returned by the `/authorize` endpoint for a <see cref="BearerAccessRefreshToken"/>.
        /// </summary>
        /// <param name="code">The authorization code returned from the initial request to the Account /authorize endpoint.</param>
        /// <returns>An instance of <see cref="BearerAccessRefreshToken"/></returns>
        Task<BearerAccessRefreshToken> RequestAccessRefreshToken(string code);

        /// <summary>
        /// Refresh a Bearer (Access) token when it has expired / is about to expire.
        /// </summary>
        /// <param name="refreshToken">The refresh token returned from the authorization code exchange.</param>
        /// <returns>An instance of <see cref="BearerAccessToken"/>.</returns>
        Task<BearerAccessToken> RefreshUserAccessToken(string refreshToken);
    }

    /// <summary>
    /// Defines a Spotify Accounts Service for the Client Credentials (App) Flow.
    /// </summary>
    /// <remarks>https://developer.spotify.com/documentation/general/guides/authorization-guide/#client-credentials-flow</remarks>
    public interface IAccountsService : IAccessTokenProvider
    {        
    }
}