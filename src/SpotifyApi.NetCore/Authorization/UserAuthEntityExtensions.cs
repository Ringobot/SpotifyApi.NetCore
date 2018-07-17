using System;

namespace SpotifyApi.NetCore
{
    internal static class UserAuthEntityExtensions
    {
        public static void AssertUserHashAndState(this IUserAuthEntity actualUserAuth, string expectedUserHash, string expectedState){
            if (actualUserAuth.UserHash != expectedUserHash) 
                throw new InvalidOperationException($"Invalid UserHash property value for {actualUserAuth}. Expected UserHash = \"{expectedUserHash}\", actual UserHash = \"{actualUserAuth.UserHash}\"");
            if (actualUserAuth.State != expectedState) 
                throw new InvalidOperationException($"Invalid State property value for {actualUserAuth}. Expected State = \"{expectedState}\", actual State = \"{actualUserAuth.State}\"");
        }
    }
}