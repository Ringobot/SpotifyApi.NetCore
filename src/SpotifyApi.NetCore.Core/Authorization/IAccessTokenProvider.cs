using System;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore.Authorization
{
    /// <summary>
    /// Defines a provider of Bearer (Access) Tokens for the Spotify Service
    /// </summary>
    public interface IAccessTokenProvider
    {
        /// <summary>
        /// Returns a valid access token for the Spotify Service
        /// </summary>
        /// <returns></returns>
        Task<string> GetAccessToken();
    }
}
