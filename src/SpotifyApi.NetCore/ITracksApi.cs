using System.Threading.Tasks;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Defines a wrapper for the Spotify Web Tracks API.
    /// </summary>
    public interface ITracksApi
    {
        #region GetTrack

        Task<Track> GetTrack(string trackId, string market = null);

        Task<T> GetTrack<T>(string trackId, string market = null);

        #endregion

        #region GetTracks

        Task<Track> GetTracks(string[] trackIds, string market = null);

        Task<T> GetTracks<T>(string[] trackIds, string market = null);

        #endregion

        #region GetTrackAudioAnalysis

        Task<TrackAudioAnalysis> GetTrackAudioAnalysis(string trackId);

        Task<T> GetTrackAudioAnalysis<T>(string trackId);

        #endregion

        #region GetTrackAudioFeatures

        Task<TrackAudioFeatures> GetTrackAudioFeatures(string trackId);

        Task<T> GetTrackAudioFeatures<T>(string trackId);

        #endregion

        #region GetTracksAudioFeatures

        Task<TrackAudioFeatures> GetTracksAudioFeatures(string[] trackId);

        Task<T> GetTracksAudioFeatures<T>(string[] trackId);

        #endregion
    }
}