using System;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace SpotifyApi.NetCore.Authorization
{
    internal static class AuthHelper
    {
        public const string TokenUrl = "https://accounts.spotify.com/api/token";
        public const string AuthorizeUrl = "https://accounts.spotify.com/authorize";

        public static AuthenticationHeaderValue GetHeader(IConfiguration configuration)
        {
            return new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}",
                    configuration["SpotifyApiClientId"], configuration["SpotifyApiClientSecret"])))
            );
        }

        public static void ValidateConfig(IConfiguration config, bool validateRedirectUri = true)
        {
            if (config["SpotifyApiClientId"] == null)
                throw new ArgumentNullException("SpotifyApiClientId", "Expecting configuration value for `SpotifyApiClientId`");
            if (config["SpotifyApiClientSecret"] == null)
                throw new ArgumentNullException("SpotifyApiClientSecret", "Expecting configuration value for `SpotifyApiClientSecret`");
            if (config["SpotifyAuthRedirectUri"] == null)
                throw new ArgumentNullException("SpotifyAuthRedirectUri", "Expecting configuration value for `SpotifyAuthRedirectUri`");
        }
    }
}