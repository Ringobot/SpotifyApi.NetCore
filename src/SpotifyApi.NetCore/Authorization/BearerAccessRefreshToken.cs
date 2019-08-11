using Newtonsoft.Json;
using System;

namespace SpotifyApi.NetCore.Authorization
{
    /// <summary>
    /// A Bearer plus Refresh Token DTO.
    /// </summary>
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

        /// <summary>
        /// A Refresh token.
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }

    /// <summary>
    /// A Bearer (Access) token DTO.
    /// </summary>
    public class BearerAccessToken
    {
        /*
            {
            "access_token": "NgCXRKc...MzYjw",
            "token_type": "bearer",
            "expires_in": 3600,
            }
        */

        /// <summary>
        /// Access (bearer) token.
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Number of seconds after issue that the token expres. <seealso cref="Expires"/>
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// List of scopes that the token is valid for. Can be null.
        /// </summary>
        [JsonProperty("scope")]
        public string Scope { get; set; }

        /// <summary>
        /// The approximate Date and Time that the token expires.
        /// </summary>
        public DateTime? Expires { get; internal set; }
    }

    /// <summary>
    /// Static extensions for <see cref="BearerAccessToken"/> and <see cref="BearerAccessRefreshToken"/>.
    /// </summary>
    public static class BearerAccessTokenExtensions
    {
        /// <summary>
        /// Derive and set the Expires property on a <see cref="BearerAccessToken"/>.
        /// </summary>
        /// <param name="token">A instance of <see cref="BearerAccessToken"/>.</param>
        /// <param name="issuedDateTime">Approximate date and time that the token was issued.</param>
        /// <returns>The derived Expires value.</returns>
        public static DateTime SetExpires(this BearerAccessToken token, DateTime issuedDateTime)
        {
            token.Expires = issuedDateTime.ToUniversalTime().AddSeconds(token.ExpiresIn);
            return token.Expires.Value;
        }

        /// <summary>
        /// Enforces invariants on a <see cref="BearerAccessToken"/>.
        /// </summary>
        /// <param name="token">An instance of <see cref="BearerAccessToken"/></param>
        public static void EnforceInvariants(this BearerAccessToken token)
        {
            if (token.Expires == null) throw new InvalidOperationException("Expires must not be null. Call SetExpires() to set");
        }

        /// <summary>
        /// Enforces invariants on a <see cref="BearerAccessRefreshToken"/>.
        /// </summary>
        /// <param name="token">An instance of <see cref="BearerAccessRefreshToken"/></param>
        public static void EnforceInvariants(this BearerAccessRefreshToken token)
        {
            if (token.RefreshToken == null) throw new InvalidOperationException("RefreshToken must not be null.");
            // enforce the Base invariants (am I doing this right?)
            ((BearerAccessToken)token).EnforceInvariants();
        }
    }
}