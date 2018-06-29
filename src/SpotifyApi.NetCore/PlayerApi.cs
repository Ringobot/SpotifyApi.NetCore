using System;
using System.Net.Http;
using System.Threading.Tasks;
using SpotifyApi.NetCore.Http;

namespace SpotifyApi.NetCore
{
    internal class PlayerApi : SpotifyWebApi, IPlayerApi
    {
        public PlayerApi(HttpClient httpClient, IAuthorizationApi authorizationApi) : base(httpClient, authorizationApi)
        {
        }

        public async Task PlayArtist(string userHash, string spotifyUri)
        {
            throw new NotImplementedException();
        }
    }
}
