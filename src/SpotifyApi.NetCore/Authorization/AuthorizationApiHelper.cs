using System;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace SpotifyApi.NetCore
{
    internal static class AuthorizationApiHelper
    {
        public const string TokenUrl = "https://accounts.spotify.com/api/token";

        public static AuthenticationHeaderValue GetHeader(IConfiguration configuration)
        {
            return new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}",
                    configuration["SpotifyApiClientId"], configuration["SpotifyApiClientSecret"])))
            );
        }
    }
}