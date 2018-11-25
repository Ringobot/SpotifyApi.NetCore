using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    public interface IAccessTokenProvider
    {
        Task<string> GetAccessToken();
    }
}
