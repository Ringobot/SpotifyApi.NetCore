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
            if (trackIds == null || trackIds.Length ==0) throw new ArgumentNullException("trackIds");
            string url = $"{BaseUrl}/tracks/?ids={string.Join(",", trackIds)}";
            if (market != null) url += $"&market={market}";
            return await GetModelFromProperty<T>(url,"tracks");
        }

        #endregion

        public Task<TrackAudioAnalysis> GetTrackAudioAnalysis(string trackId)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetTrackAudioAnalysis<T>(string trackId)
        {
            throw new NotImplementedException();
        }

        public Task<TrackAudioFeatures> GetTrackAudioFeatures(string trackId)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetTrackAudioFeatures<T>(string trackId)
        {
            throw new NotImplementedException();
        }

        public Task<TrackAudioFeatures> GetTracksAudioFeatures(string[] trackId)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetTracksAudioFeatures<T>(string[] trackId)
        {
            throw new NotImplementedException();
        }
    }
}