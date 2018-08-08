using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace SpotifyApi.NetCore.Authorization
{
    public static class StateHelper
    {
        private const char StateFormatDelimiter = '|';

        public static string EncodeState((string userHash, string state) userAuth) => $"{userAuth.userHash}{StateFormatDelimiter}{userAuth.state}";

        public static (string userHash, string state) DecodeState(string stateParam)
        {
            var parts = stateParam.Split(StateFormatDelimiter);
            if (parts.Length != 2) ThrowStateException();
            return (parts[0], parts[1]);
        }

        public static void ThrowStateException() => throw new InvalidOperationException("Invalid state parameter");

        public static string UserHash(string username)
        {
            //https://stackoverflow.com/a/33245817/610731

            using (var algorithm = SHA256.Create())
            {
                return String.Concat(algorithm.ComputeHash
                (
                    Encoding.ASCII.GetBytes(username.ToLower())
                )
                .Select(b => b.ToString("x2")))
                .ToUpper();
            }
        }
    }
}