// using System.Threading.Tasks;
// using SpotifyApi.NetCore;
// using SpotifyVue.Services;

// namespace SpotifyVue.Data
// {
//     public class RefreshTokenStore : IRefreshTokenStore
//     {
//         private readonly SpotifyAuthService _authService;
//         public RefreshTokenStore(SpotifyAuthService authService)
//         {
//             _authService = authStorage;
//         }

//         public Task<string> Get(string userHash)
//         {
//             return Task.FromResult(_userAuthStorage.GetByUserHash(userHash).RefreshToken);
//         }

//         //TODO: =>Set?
//         public Task InsertOrReplace(string userHash, string token)
//         {
//             //TODO: Remove this method
//             return Task.CompletedTask;
//         }
//     }
// }