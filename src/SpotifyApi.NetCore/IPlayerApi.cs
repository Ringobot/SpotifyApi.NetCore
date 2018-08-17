using System;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    public interface IPlayerApi
    {
        Task PlayContext(string userHash, string spotifyUri);
        Task PlayContext(string userHash, string spotifyUri, string offsetTrackUri = null, string deviceId = null);
        Task PlayContext(string userHash, string spotifyUri, int offsetPosition = 0, string deviceId = null);
        Task PlayTracks(string userHash, string[] spotifyTrackUris, string offsetTrackUri = null, string deviceId = null);
        Task PlayTracks(string userHash, string[] spotifyTrackUris, int offsetPosition = 0, string deviceId = null);
        Task Play(string userHash, object data, string deviceId = null);

        //TODO: Task PlayContext((string userHash, string token) userRefreshToken, string spotifyUri);

        Task<Device[]> GetDevices(string userHash);   
    }
}
