using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SpotifyApi.NetCore.Authorization;

namespace SpotifyApi.NetCore.Tests 
{
    /// <summary>
    /// Helpers for tests
    /// </summary>
    /// <remarks>Not thread safe. Will need to be refactored if tests runs are parallelized.</remarks>
    internal static class TestsHelper
    {
        private static UserAccountsService _accounts = new UserAccountsService(GetLocalConfig());
        private static string _accessToken;
        private static IConfiguration _config;

        internal static IConfiguration GetLocalConfig()
        {
            if (_config == null)
            {
                _config = new ConfigurationBuilder()
                // Using "..", "..", ".." vs. "..\\..\\.." for system-agnostic path resolution
                // Reference: https://stackoverflow.com/questions/14899422/how-to-navigate-a-few-folders-up#comment97806320_30902714
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", ".."))
                .AddJsonFile("appsettings.local.json", false)
                .Build();
            }

            return _config;
        }

        /// <summary>
        /// Gets and caches a user access token. App setting "SpotifyUserRefreshToken" must contain a valid 
        /// user refresh token for this to work. Does not refesh token when expired.
        /// </summary>
        internal static async Task<string> GetUserAccessToken()
        {
            // not thread safe (last one wins)
            if (_accessToken == null)
            {
                var tokens = await _accounts.RefreshUserAccessToken(GetLocalConfig()["SpotifyUserRefreshToken"]);
                _accessToken = tokens.AccessToken;
            }

            return _accessToken;
        }
    }
}