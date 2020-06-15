using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    public interface IUsersProfileApi
    {
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
        Task<User> GetCurrentUsersProfile(string accessToken = null);

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
        Task<T> GetCurrentUsersProfile<T>(string accessToken = null);

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
        Task<User> GetUsersProfile(string userId, string accessToken = null);

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
        Task<T> GetUsersProfile<T>(string userId, string accessToken = null);
    }
}
