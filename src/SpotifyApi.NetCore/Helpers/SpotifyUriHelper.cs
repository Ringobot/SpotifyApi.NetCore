using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SpotifyApi.NetCore.Helpers
{
    /// <summary>
    /// Helper for Spotify URI's and Id's
    /// </summary>
    /// <remarks> https://developer.spotify.com/documentation/web-api/#spotify-uris-and-ids </remarks>
    public static class SpotifyUriHelper
    {
        private static readonly Regex SpotifyIdRegEx = new Regex("^[a-zA-Z0-9]+$");
        public static readonly Regex SpotifyUriRegEx = new Regex("spotify:[a-z]+:[a-zA-Z0-9]+$");
        public static readonly Regex SpotifyUserPlaylistUriRegEx = new Regex("spotify:user:[a-z0-9_-]+:playlist:[a-zA-Z0-9]+");

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

        /// <summary>
        /// Converts a Spotify Track Id or URI into a Spotify Id
        /// </summary>
        public static string TrackId(string trackId) => ToId("track", trackId);

        /// <summary>
        /// Converts a Spotify Album Id or URI into a Spotify Id
        /// </summary>
        public static string AlbumId(string albumId) => ToId("album", albumId);

        /// <summary>
        /// Converts a Spotify Artist Id or URI into a Spotify Id
        /// </summary>
        public static string ArtistId(string artistId) => ToId("artist", artistId);

        /// <summary>
        /// Converts a Spotify Playlist Id or URI into a Spotify Id
        /// </summary>
        public static string PlaylistId(string playlistId) => ToId("playlist", playlistId);

        private static string ToUri(string type, string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id), $"Spotify {type} Id cannot be empty or null");

            // if a Spotify URI
            MatchCollection matchesUri = SpotifyUriRegEx.Matches(id);
            if (matchesUri.Count > 0 && SpotifyUriType(matchesUri[0].Value) == type) return matchesUri[0].Value;

            // if a Spotify User Playlist URI
            if (type == "playlist")
            {
                MatchCollection matchesId = SpotifyUserPlaylistUriRegEx.Matches(id);
                if (matchesId.Count > 0) return matchesId[0].Value;
            }

            // if a Spotify Id
            if (SpotifyIdRegEx.IsMatch(id)) return $"spotify:{type}:{id}";
            throw new ArgumentException($"\"{id}\" is not a valid Spotify {type} identifier");
        }

        public static string ToId(string type, string idOrUri, bool throwIfNotValid = true)
        {
            if (string.IsNullOrEmpty(idOrUri)) throw new ArgumentNullException(
                nameof(idOrUri), 
                $"Spotify {type} Id or URI cannot be empty or null");

            // if a Spotify Id
            if (SpotifyIdRegEx.IsMatch(idOrUri)) return idOrUri;

            // if a Spotify URI
            MatchCollection matchesUri = SpotifyUriRegEx.Matches(idOrUri);
            if (matchesUri.Count > 0 && SpotifyUriType(matchesUri[0].Value) == type)
                return matchesUri[0].Value.Split(':').Last();

            // if a Spotify User Playlist URI
            if (type == "playlist")
            {
                MatchCollection matchesId = SpotifyUserPlaylistUriRegEx.Matches(idOrUri);
                if (matchesId.Count > 0) return matchesId[0].Value.Split(':').Last();
            }

            if (throwIfNotValid) throw new ArgumentException($"\"{idOrUri}\" is not a valid Spotify {type} identifier");

            return null;
        }

        //TODO: AlbumId, etc

        public static string SpotifyUriType(string uri) => uri.Split(':')[1];
    }
}
