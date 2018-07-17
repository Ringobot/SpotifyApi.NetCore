using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace SpotifyApi.NetCore
{
    public interface IAuthorizationCodeService
    {
        Task<string> RequestAuthorizationUrl(string userHash);
        Task RequestToken(string queryString);
        Task RequestToken(string state, string code);
        Task RequestToken(IEnumerable<KeyValuePair<String, StringValues>> queryCollection);
    }
}