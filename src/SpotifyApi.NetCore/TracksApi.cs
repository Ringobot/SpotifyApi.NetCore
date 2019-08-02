using System;
using System.Net.Http;
using System.Threading.Tasks;
using SpotifyApi.NetCore.Authorization;

namespace SpotifyApi.NetCore
{
    /// <summary>
    /// Endpoints for retrieving information about one or more tracks from the Spotify catalog.
    /// </summary>
    public class TracksApi : SpotifyWebApi, ITracksApi
    {
        protected internal virtual ISearchApi SearchApi { get; set; }

        #region Constructors

        /// <summary>
        /// This constructor accepts a Spotify access token that will be used for all calls to the API 
        /// (except when an accessToken is provided using the optional `accessToken` parameter on each method).
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/></param>
        /// <param name="accessToken">A valid access token from the Spotify Accounts service</param>
        public TracksApi(HttpClient httpClient, string accessToken): base(httpClient, accessToken)
        {
            SearchApi = new SearchApi(httpClient, accessToken);
        }

        /// <summary>
        /// Use this constructor when an accessToken will be provided using the `accessToken` parameter 
        /// on each method
        /// </summary>
        /// <param name="httpClient">An instance of <see cref="HttpClient"/></param>
        public TracksApi(HttpClient httpClient) : base(httpClient)
        {
            SearchApi = new SearchApi(httpClient);
        }

        public TracksApi(HttpClient httpClient, IAccessTokenProvider accessTokenProvider) : base(httpClient, accessTokenProvider)
        {
            SearchApi = new SearchApi(httpClient, accessTokenProvider);
        }

        #endregion

        #region GetTrack

        /// <summary>
        /// Get Spotify catalog information for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="trackId">The Spotify ID for the track.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 <see cref="SpotifyCountryCode"/> or the
        /// string `from_token`. Provide this parameter if you want to apply Track Relinking.</param>
        /// <returns>Task of Track</returns>
        public Task<Track> GetTrack(string trackId, string market = null, string accessToken = null) 
            => GetTrack<Track>(trackId, market, accessToken);

        /// <summary>
        /// Get Spotify catalog information for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="trackId">The Spotify ID for the track.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 <see cref="SpotifyCountryCode"/> or the
        /// string `from_token`. Provide this parameter if you want to apply Track Relinking.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        public async Task<T> GetTrack<T>(string trackId, string market = null, string accessToken = null)
        {
            string url = $"{BaseUrl}/tracks/{trackId}";
            if (market != null) url += $"?market={market}";
            return await GetModel<T>(url, accessToken);
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
        public async Task<Track[]> GetTracks(string[] trackIds, string market = null, string accessToken = null) 
            => await GetTracks<Track[]>(trackIds, market, accessToken);

        /// <summary>
        /// Get Spotify catalog information for multiple tracks based on their Spotify IDs.
        /// </summary>
        /// <param name="trackIds">An array of the Spotify IDs for the tracks. Maximum: 50 IDs.</param>
        /// <param name="market">Optional. An ISO 3166-1 alpha-2 <see cref="SpotifyCountryCode"/> or the
        /// string `from_token`. Provide this parameter if you want to apply Track Relinking.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        public async Task<T> GetTracks<T>(string[] trackIds, string market = null, string accessToken = null)
        {
            if (trackIds == null || trackIds.Length == 0) throw new ArgumentNullException("trackIds");
            string url = $"{BaseUrl}/tracks/?ids={string.Join(",", trackIds)}";
            if (market != null) url += $"&market={market}";
            return await GetModelFromProperty<T>(url, "tracks", accessToken);
        }

        #endregion

        #region GetTrackAudioAnalysis

        /// <summary>
        /// Get a detailed audio analysis for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="trackId">The Spotify ID for the track.</param>
        /// <returns>Task of <see cref="TrackAudioAnalysis" /></returns>
        public async Task<TrackAudioAnalysis> GetTrackAudioAnalysis(string trackId, string accessToken = null) 
            => await GetTrackAudioAnalysis<TrackAudioAnalysis>(trackId, accessToken);

        /// <summary>
        /// Get a detailed audio analysis for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="trackId">The Spotify ID for the track.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        public async Task<T> GetTrackAudioAnalysis<T>(string trackId, string accessToken = null) 
            => await GetModel<T>($"{BaseUrl}/audio-analysis/{trackId}", accessToken);

        #endregion

        #region GetTrackAudioFeatures

        /// <summary>
        /// Get audio feature information for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="trackId">The Spotify ID for the track.</param>
        /// <returns>Task of <see cref="TrackAudioFeatures" /></returns>
        public async Task<TrackAudioFeatures> GetTrackAudioFeatures(string trackId, string accessToken = null) 
            => await GetTrackAudioFeatures<TrackAudioFeatures>(trackId, accessToken);

        /// <summary>
        /// Get audio feature information for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="trackId">The Spotify ID for the track.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        public async Task<T> GetTrackAudioFeatures<T>(string trackId, string accessToken = null) 
            => await GetModel<T>($"{BaseUrl}/audio-features/{trackId}", accessToken);

        #endregion

        #region GetTracksAudioFeatures

        /// <summary>
        /// Get audio features for multiple tracks based on their Spotify IDs.
        /// </summary>
        /// <param name="trackIds">An array of the Spotify IDs for the tracks. Maximum: 100 IDs.</param>
        /// <returns>Task of <see cref="TrackAudioFeatures[]" /></returns>
        public async Task<TrackAudioFeatures[]> GetTracksAudioFeatures(string[] trackIds, string accessToken = null) 
            => await GetTracksAudioFeatures<TrackAudioFeatures[]>(trackIds, accessToken);

        /// <summary>
        /// Get audio features for multiple tracks based on their Spotify IDs.
        /// </summary>
        /// <param name="trackIds">An array of the Spotify IDs for the tracks. Maximum: 100 IDs.</param>
        /// <typeparam name="T">Optionally provide your own type to deserialise Spotify's response to.</typeparam>
        /// <returns>Task of T. The Spotify response is deserialised as T.</returns>
        public async Task<T> GetTracksAudioFeatures<T>(string[] trackIds, string accessToken = null)
        {
            if (trackIds == null || trackIds.Length == 0) throw new ArgumentNullException("trackIds");
            string url = $"{BaseUrl}/audio-features/?ids={string.Join(",", trackIds)}";
            return await GetModelFromProperty<T>(url, "audio_features", accessToken);
        }

        #endregion

        #region SearchTracks

        /// <summary>
        /// Get Spotify Catalog information about tracks that match a keyword string.
        /// </summary>
        /// <param name="query">Search query keywords and optional field filters and operators. See
        /// https://developer.spotify.com/documentation/web-api/reference/search/search/#writing-a-query---guidelines</param>
        /// <returns>Task of <see cref="SearchResult" /></returns>
        public async Task<TracksSearchResult> SearchTracks(string query, string accessToken = null)
            => (await SearchApi.Search(query, SpotifySearchTypes.Track, null, (0, 0), accessToken)).Tracks;

        /// <summary>
        /// Get Spotify Catalog information about tracks that match a keyword string.
        /// </summary>
        /// <param name="query">Search query keywords and optional field filters and operators. See
        /// https://developer.spotify.com/documentation/web-api/reference/search/search/#writing-a-query---guidelines</param>
        /// <param name="market">Optional. Choose a <see cref="SpotifyCountryCodes"/>. If a country code
        /// is specified, only tracks with content that is playable in that market is returned. </param>
        /// <returns>Task of <see cref="SearchResult" /></returns>
        public async Task<TracksSearchResult> SearchTracks(string query, string market, string accessToken = null)
            => (await SearchApi.Search(query, SpotifySearchTypes.Track, market, (0, 0), accessToken)).Tracks;

        /// <summary>
        /// Get Spotify Catalog information about tracks that match a keyword string.
        /// </summary>
        /// <param name="query">Search query keywords and optional field filters and operators. See
        /// https://developer.spotify.com/documentation/web-api/reference/search/search/#writing-a-query---guidelines</param>
        /// <param name="market">Optional. Choose a <see cref="SpotifyCountryCodes"/>. If a country code
        /// is specified, only tracks with content that is playable in that market is returned. </param>
        /// <param name="limit">Optional. Maximum number of results to return. Default: 20, Minimum: 1,
        /// Maximum: 50.</param>
        /// <param name="offset">Optional. The index of the first result to return. Default: 0 (the
        /// first result). Maximum offset (including limit): 10,000. Use with limit to get the next
        /// page of search results.</param>
        /// <returns>Task of <see cref="SearchResult" /></returns>
        public async Task<TracksSearchResult> SearchTracks(string query, string market, (int limit, int offset) limitOffset, string accessToken = null)
            => (await SearchApi.Search(query, SpotifySearchTypes.Track, market, limitOffset, accessToken)).Tracks;

        #endregion

    }
}