using System;

namespace SpotifyVue.Models
{
    public class UserAuth
    {
        public string SpotifyUserName { get; set; }
        public string UserHash { get; set; }
        public string RefreshToken { get; set; }
        public bool Authorized { get; set; }
        public string Scopes { get; set; }
    }

    internal static class UserAuthExtensions
    {
        public static void EnforceInvariants(this UserAuth userAuth)
        {
            if (string.IsNullOrEmpty(userAuth.SpotifyUserName)) throw new InvalidOperationException("SpotifyUserName must not be null");
            if (string.IsNullOrEmpty(userAuth.UserHash)) throw new InvalidOperationException("UserHash must not be null");
        }
    }
}