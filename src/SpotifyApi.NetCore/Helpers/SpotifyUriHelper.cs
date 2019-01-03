using System;
using System.Text.RegularExpressions;

namespace SpotifyApi.NetCore.Helpers
{
    /// <summary>
    /// Helper for Spotify URI's and Id's
    /// </summary>
    /// <remarks> https://developer.spotify.com/documentation/web-api/#spotify-uris-and-ids </remarks>
    internal static class SpotifyUriHelper
    {
        private static readonly Regex _idRegEx = new Regex("^[a-zA-Z0-9]+$");
        private static readonly Regex _uriRegEx = new Regex("^spotify:[a-z]+:[a-zA-Z0-9]+$");

        /// <summary>
        /// Converts a Spotify Track Id or URI into a Spotify URI
        /// </summary>
        public static string TrackUri(string trackId) => ToUri("track", trackId);

        /// <summary>
        /// Converts a Spotify Album Id or URI into a Spotify URI
        /// </summary>
        public static string AlbumUri(string albumId) => ToUri("album", albumId);

        /// <summary>
        /// Converts a Spotify Artist Id or URI into a Spotify URI
        /// </summary>
        public static string ArtistUri(string artistId) => ToUri("artist", artistId);

        /// <summary>
        /// Converts a Spotify Playlist Id or URI into a Spotify URI
        /// </summary>
        public static string PlaylistUri(string playlistId) => ToUri("playlist", playlistId);

        private static string ToUri(string type, string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id), $"Spotify {type} Id cannot be empty or null");
            if (_uriRegEx.IsMatch(id) && SpotifyUriType(id) == type) return id;
            if (_idRegEx.IsMatch(id)) return $"spotify:{type}:{id}";
            throw new ArgumentException($"\"{id}\" is not a valid Spotify {type} identifier");
        }

        //TODO: AlbumId, etc

        private static string SpotifyUriType(string uri) => uri.Split(':')[1];
    }
}
