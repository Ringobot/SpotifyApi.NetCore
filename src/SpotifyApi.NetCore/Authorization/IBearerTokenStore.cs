using System.Threading.Tasks;

namespace SpotifyApi.NetCore.Authorization
{
    /// <summary>
    /// Defines a Bearer Token cache store for use with <see cref="AccountsService"/>.
    /// </summary>
    public interface IBearerTokenStore
    {
        /// <summary>
        /// Inserts or replaces a <see cref="BearerAccessToken"/>.
        /// </summary>
        /// <param name="key">The identifier key for the token.</param>
        /// <param name="token">The token as <see cref="BearerAccessToken"/>.</param>
        Task InsertOrReplace(string key, BearerAccessToken token);

        /// <summary>
        /// Get a <see cref="BearerAccessToken"/> by its key.
        /// </summary>
        Task<BearerAccessToken> Get(string key);
    }
}