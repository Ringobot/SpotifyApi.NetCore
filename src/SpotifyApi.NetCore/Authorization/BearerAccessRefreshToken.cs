using System;
using Newtonsoft.Json;

namespace SpotifyApi.NetCore
{
    public class BearerAccessRefreshToken : BearerAccessToken
    {
        /*
            {
            "access_token": "NgCXRK...MzYjw",
            "token_type": "Bearer",
            "scope": "user-read-private user-read-email",
            "expires_in": 3600,
            "refresh_token": "NgAagA...Um_SHo"
            }        
        */
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }

    public class BearerAccessToken
    {
        /*
            {
            "access_token": "NgCXRKc...MzYjw",
            "token_type": "bearer",
            "expires_in": 3600,
            }
        */        

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        /// Can be null
        [JsonProperty("scope")]
        public string Scope { get; set; }

        public DateTime? Expires { get; internal set; }
    }

    public static class BearerAccessTokenExtensions
    {
        public static DateTime SetExpires(this BearerAccessToken token, DateTime now)
        {
            token.Expires = now.ToUniversalTime().AddSeconds(token.ExpiresIn);
            return token.Expires.Value;
        }

        public static void EnforceInvariants(this BearerAccessToken token)
        {
            if (token.Expires == null) throw new InvalidOperationException("Expires must not be null. Call SetExpires() to set");
        }

        public static void EnforceInvariants(this BearerAccessRefreshToken token)
        {
            if (token.RefreshToken == null) throw new InvalidOperationException("RefreshToken must not be null.");
            // enforce the Base invariants (am I doing this right?)
            ((BearerAccessToken)token).EnforceInvariants();
        }
    }
}