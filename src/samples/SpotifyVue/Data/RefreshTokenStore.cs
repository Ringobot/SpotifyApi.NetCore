using System.Threading.Tasks;
using SpotifyApi.NetCore;

namespace SpotifyVue.Data
{
    public class RefreshTokenStore : IRefreshTokenStore
    {
        private readonly UserAuthStorage _userAuthStorage;
        public RefreshTokenStore(UserAuthStorage userAuthStorage)
        {
            _userAuthStorage = userAuthStorage;
        }

        public Task<string> Get(string userHash)
        {
            return Task.FromResult(_userAuthStorage.GetByUserHash(userHash).RefreshToken);
        }

        //TODO: =>Set?
        public Task InsertOrReplace(string userHash, string token)
        {
            //TODO: Remove this method
            return Task.CompletedTask;
        }
    }
}