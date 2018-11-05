using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpotifyApi.NetCore.Http;

namespace SpotifyApi.NetCore
{
    public class TracksApi : SpotifyWebApi, ITracksApi
    {
        public TracksApi(HttpClient httpClient, IAccountsService accountsService) : base(httpClient, accountsService)
        {
        }

        #region GetTrack

        /// <summary>
        /// Get Spotify catalog information for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="trackId">The Spotify ID for the track.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 <see cref="SpotifyCountryCode"/> or the
        /// string `from_token`. Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns>Task of Track</returns>
        public Task<Track> GetTrack(string trackId, string market = null) => GetTrack<Track>(trackId, market);

        /// <summary>
        /// Get Spotify catalog information for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="trackId">The Spotify ID for the track.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 <see cref="SpotifyCountryCode"/> or the
        /// string `from_token`. Provide this parameter if you want to apply Track Relinking.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        public async Task<T> GetTrack<T>(string trackId, string market = null)
        {
            string url = $"{BaseUrl}/tracks/{trackId}";
            if (market != null) url += $"?market={market}";
            return await GetModel<T>(url);
        }

        #endregion

        #region GetTracks

        /// <summary>
        /// Get Spotify catalog information for multiple tracks based on their Spotify IDs.
        /// </summary>
        /// <param name="trackIds">An array of the Spotify IDs for the tracks. Maximum: 50 IDs.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 <see cref="SpotifyCountryCode"/> or the
        /// string `from_token`. Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns>Task of Track[]</returns>
        public async Task<Track[]> GetTracks(string[] trackIds, string market = null) => await GetTracks<Track[]>(trackIds, market);

        /// <summary>
        /// Get Spotify catalog information for multiple tracks based on their Spotify IDs.
        /// </summary>
        /// <param name="trackIds">An array of the Spotify IDs for the tracks. Maximum: 50 IDs.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 <see cref="SpotifyCountryCode"/> or the
        /// string `from_token`. Provide this parameter if you want to apply Track Relinking.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        public async Task<T> GetTracks<T>(string[] trackIds, string market = null)
        {
            if (trackIds == null || trackIds.Length == 0) throw new ArgumentNullException("trackIds");
            string url = $"{BaseUrl}/tracks/?ids={string.Join(",", trackIds)}";
            if (market != null) url += $"&market={market}";
            return await GetModelFromProperty<T>(url, "tracks");
        }

        #endregion

        #region GetTrackAudioAnalysis

        /// <summary>
        /// Get a detailed audio analysis for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="trackId">The Spotify ID for the track.</param>
        /// <returns>Task of <see cref="TrackAudioAnalysis" /></returns>
        public async Task<TrackAudioAnalysis> GetTrackAudioAnalysis(string trackId) => await GetTrackAudioAnalysis<TrackAudioAnalysis>(trackId);

        /// <summary>
        /// Get a detailed audio analysis for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="trackId">The Spotify ID for the track.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        public async Task<T> GetTrackAudioAnalysis<T>(string trackId) => await GetModel<T>($"{BaseUrl}/audio-analysis/{trackId}");

        #endregion

        #region GetTrackAudioFeatures

        /// <summary>
        /// Get audio feature information for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="trackId">The Spotify ID for the track.</param>
        /// <returns>Task of <see cref="TrackAudioFeatures" /></returns>
        public async Task<TrackAudioFeatures> GetTrackAudioFeatures(string trackId) => await GetTrackAudioFeatures<TrackAudioFeatures>(trackId);

        /// <summary>
        /// Get audio feature information for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="trackId">The Spotify ID for the track.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        public async Task<T> GetTrackAudioFeatures<T>(string trackId) => await GetModel<T>($"{BaseUrl}/audio-features/{trackId}");

        #endregion

        #region GetTracksAudioFeatures

        /// <summary>
        /// Get audio features for multiple tracks based on their Spotify IDs.
        /// </summary>
        /// <param name="trackIds">An array of the Spotify IDs for the tracks. Maximum: 100 IDs.</param>
        /// <returns>Task of <see cref="TrackAudioFeatures[]" /></returns>
        public async Task<TrackAudioFeatures[]> GetTracksAudioFeatures(string[] trackIds) => await GetTracksAudioFeatures<TrackAudioFeatures[]>(trackIds);

        /// <summary>
        /// Get audio features for multiple tracks based on their Spotify IDs.
        /// </summary>
        /// <param name="trackIds">An array of the Spotify IDs for the tracks. Maximum: 100 IDs.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        public async Task<T> GetTracksAudioFeatures<T>(string[] trackIds)
        {
            if (trackIds == null || trackIds.Length == 0) throw new ArgumentNullException("trackIds");
            string url = $"{BaseUrl}/audio-features/?ids={string.Join(",", trackIds)}";
            return await GetModelFromProperty<T>(url, "audio_features");
        }

        #endregion
    }
}