using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Defines a Spotify Authorisation Token service.
    /// </summary>
    public interface IAuthorizationApi
    {
        /// <summary>
        /// Returns a bearer access token to use for a Request to the Spotify API.
        /// </summary>
        /// <returns>A Bearer token as (awaitable) Task of string</returns>
        Task<string> GetAccessToken();

        /// <summary>
        /// Returns a bearer access token to use for a Request to the Spotify API for a specific User.
        /// </summary>
        /// <returns>A Bearer token as (awaitable) Task of string</returns>
        Task<string> GetAccessToken(string userHash);
    }
}