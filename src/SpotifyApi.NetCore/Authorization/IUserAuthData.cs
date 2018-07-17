using System;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    public interface IUserAuthData
    {
        IUserAuthEntity Create(string userHash, string state);
        Task<IUserAuthEntity> InsertOrReplace(IUserAuthEntity userAuth);
        Task<IUserAuthEntity> Get(string userHash);
        Task Update(IUserAuthEntity userAuth);
    }
}