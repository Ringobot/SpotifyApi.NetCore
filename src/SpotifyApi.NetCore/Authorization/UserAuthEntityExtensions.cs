using System;

namespace SpotifyApi.NetCore.Authorization
{
    internal static class UserAuthEntityExtensions
    {
        public static void AssertUserHashAndState(this IUserAuthEntity actualUserAuth, string expectedUserHash, string expectedState){
            if (actualUserAuth.UserHash != expectedUserHash) 
                throw new InvalidOperationException($"Invalid UserHash property value for {actualUserAuth}. Expected UserHash = \"{expectedUserHash}\", actual UserHash = \"{actualUserAuth.UserHash}\"");
            if (actualUserAuth.State != expectedState) 
                throw new InvalidOperationException($"Invalid State property value for {actualUserAuth}. Expected State = \"{expectedState}\", actual State = \"{actualUserAuth.State}\"");
        }

        /// Sets all token properties, clears state and code properties
        public static void SetPropertiesFromAuthCode(this IUserAuthEntity userAuth, AuthorizationTokens authCode)
        {
            userAuth.AccessToken = authCode.accessToken;
            userAuth.AuthUrl = authCode.authUrl;
            userAuth.TokenType = authCode.tokenType;
            userAuth.Scope = authCode.scope;
            userAuth.Expiry = authCode.expires;
            userAuth.RefreshToken = authCode.refreshToken;
            userAuth.State = null;
            userAuth.Code = null;
        }
    }
}