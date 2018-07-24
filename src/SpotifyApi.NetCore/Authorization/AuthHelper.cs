using System;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace SpotifyApi.NetCore.Authorization
{
    //TODO Move back into Accounts Service?
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
    }
}