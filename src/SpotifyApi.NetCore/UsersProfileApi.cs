using SpotifyApi.NetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    public class UsersProfileApi : SpotifyWebApi, IUsersProfileApi
    {
        #region Constructors

        /// <summary>
        /// Instantiates a new <see cref="UsersProfileApi"/>.
        /// </summary>
        /// <remarks>
        /// Use this constructor when an accessToken will be provided using the `accessToken` parameter 
        /// on each method
        /// </remarks>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/></param>
        public UsersProfileApi(HttpClient httpClient) : base(httpClient)
        {
        }

        /// <summary>
        /// Instantiates a new <see cref="UsersProfileApi"/>.
        /// </summary>
        /// <remarks>
        /// This constructor accepts a Spotify access token that will be used for all calls to the API 
        /// (except when an accessToken is provided using the optional `accessToken` parameter on each method).
        /// </remarks>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/></param>
        /// <param name="accessToken">A valid access token from the Spotify Accounts service</param>
        public UsersProfileApi(HttpClient httpClient, string accessToken) : base(httpClient, accessToken)
        {
        }

        /// <summary>
        /// Instantiates a new <see cref="UsersProfileApi"/>.
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/></param>
        /// <param name="accessTokenProvider">An instance of <see cref="IAccessTokenProvider"/>, e.g. <see cref="Authorization.AccountsService"/>.</param>
        public UsersProfileApi(HttpClient httpClient, IAccessTokenProvider accessTokenProvider) 
            : base(httpClient, accessTokenProvider)
        {
        }

        #endregion

        /// <summary>
        /// Get detailed profile information about the current user (including the current user’s username).
        /// </summary>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of the current user. Reading the user's 
        /// email address requires the `user-read-email` scope; reading country and product subscription 
        /// level requires the `user-read-private` scope. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <returns>See <see cref="User"/></returns>
        /// <remarks> https://developer.spotify.com/documentation/web-api/reference/users-profile/get-current-users-profile/ </remarks>
        public Task<User> GetCurrentUsersProfile(string accessToken = null) => GetCurrentUsersProfile<User>(accessToken: accessToken);

        /// <summary>
        /// Get detailed profile information about the current user (including the current user’s username).
        /// </summary>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of the current user. Reading the user's 
        /// email address requires the `user-read-email` scope; reading country and product subscription 
        /// level requires the `user-read-private` scope. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        /// <remarks> https://developer.spotify.com/documentation/web-api/reference/users-profile/get-current-users-profile/ </remarks>
        public async Task<T> GetCurrentUsersProfile<T>(string accessToken = null)
        {
            var builder = new UriBuilder($"{BaseUrl}/me");
            return await GetModel<T>(builder.Uri, accessToken: accessToken);
        }

        /// <summary>
        /// Get public profile information about a Spotify user.
        /// </summary>
        /// <param name="userId">The user's Spotify user ID.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of the current user. Reading the user's 
        /// email address requires the `user-read-email` scope; reading country and product subscription 
        /// level requires the `user-read-private` scope. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <returns>See <see cref="User"/></returns>
        /// <remarks> https://developer.spotify.com/documentation/web-api/reference/users-profile/get-users-profile/ </remarks>
        public Task<User> GetUsersProfile(string userId, string accessToken = null)
            => GetUsersProfile<User>(userId, accessToken: accessToken);

        /// <summary>
        /// Get public profile information about a Spotify user.
        /// </summary>
        /// <param name="userId">The user's Spotify user ID.</param>
        /// <param name="accessToken">Optional. A valid access token from the Spotify Accounts service. 
        /// The access token must have been issued on behalf of the current user. Reading the user's 
        /// email address requires the `user-read-email` scope; reading country and product subscription 
        /// level requires the `user-read-private` scope. <seealso cref="UserAccountsService"/>
        /// </param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        /// <remarks> https://developer.spotify.com/documentation/web-api/reference/users-profile/get-users-profile/ </remarks>
        public async Task<T> GetUsersProfile<T>(string userId, string accessToken = null)
        {
            var builder = new UriBuilder($"{BaseUrl}/users/{userId}");
            return await GetModel<T>(builder.Uri, accessToken: accessToken);
        }
    }
}
