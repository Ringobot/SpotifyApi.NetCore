using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace SpotifyApi.NetCore
{
    public interface IAuthorizationCodeService
    {
        (string url, string state) RequestAuthorizationUrl(string userHash);
        Task<AuthorizationTokens> RequestTokens(string queryString);
        Task<AuthorizationTokens> RequestTokens(string userHash, string state, string queryState, string code);
        Task<AuthorizationTokens> RequestTokens(IEnumerable<KeyValuePair<String, StringValues>> queryCollection);
    }
}