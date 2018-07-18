using System;

namespace SpotifyApi.NetCore
{
    public class AuthorizationTokens
    {
        public string accessToken { get; set; }
        public string authUrl { get; set; }
        public string tokenType { get; set; }
        public string scope { get; set; }
        public DateTime expires { get; set; }
        public string refreshToken { get; set; }
    }
}