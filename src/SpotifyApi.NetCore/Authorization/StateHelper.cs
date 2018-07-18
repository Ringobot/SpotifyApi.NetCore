using System;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace SpotifyApi.NetCore.Authorization
{
    internal static class StateHelper
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
    }
}