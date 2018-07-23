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
        
        [JsonProperty("scope")]
        public string Scope { get; set; }

        public override int GetHashCode()
        {
            // return Scope + RefreshToken +  AccessToken.GetHashCode()?
            throw new NotImplementedException();
        }

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

        // DMO

        public DateTime? Expires { get; private set; }

        public DateTime SetExpires(DateTime now)
        {
            Expires = now.ToUniversalTime().AddMilliseconds(ExpiresIn);
            return Expires.Value;
        }

        public override int GetHashCode()
        {
            // return AccessToken.GetHashCode()?
            throw new NotImplementedException();
        }

        public void EnforceInvariants()
        {
            if (Expires == null) throw new InvalidOperationException("Expires must not be null. Call SetExpires() to set");
        }
    }
}