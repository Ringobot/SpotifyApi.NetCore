using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SpotifyApi.NetCore.Models
{
    /// <summary>
    /// Domain Model Object that parses and describes a Spotify URI or Id.
    /// </summary>
    public class SpotifyUri
    {
        internal static readonly Regex SpotifyUriRegEx = new Regex("spotify:[a-z]+:[a-zA-Z0-9]+$");
        internal static readonly Regex SpotifyUserPlaylistUriRegEx = new Regex("^spotify:user:[a-z0-9_-]+:playlist:[a-zA-Z0-9]+$");
        internal static readonly Regex SpotifyUserCollectionUriRegEx = new Regex("^spotify:user:[a-z0-9_-]+:collection:[a-z]+:[a-zA-Z0-9]+$");
        internal static readonly Regex SpotifyIdRegEx = new Regex("^[a-zA-Z0-9]+$");

        public SpotifyUri(string inputValue, string type = null)
        {
            if (string.IsNullOrWhiteSpace(inputValue)) throw new ArgumentNullException(nameof(inputValue));

            InputValue = inputValue;
            string trimUri = inputValue.Trim();
            string[] uriParts = trimUri.Split(':');

            // Spotify URI
            MatchCollection matchesUri = SpotifyUriRegEx.Matches(trimUri);
            if (matchesUri.Count > 0)
            {
                // spotify:playlist:0TnOYISbd1XYRBk9myaseg
                Uri = FullUri = matchesUri[0].Value;
                ItemType = TypeFromUri(Uri);
                Id = FromUriToId(Uri);
                IsSpotifyUri = true;
                IsValid = type == null || type == ItemType;
                return;
            }

            // if a Spotify Id
            if (SpotifyIdRegEx.IsMatch(trimUri))
            {
                if (type != null && new[] { "album", "artist", "track", "playlist" }.Contains(type))
                {
                    FullUri = Uri = $"spotify:{type}:{trimUri}";
                    IsSpotifyUri = true;
                }

                Id = trimUri;
                IsValid = true;
            }

            // Spotify User Collection URI
            if (SpotifyUserCollectionUriRegEx.IsMatch(trimUri))
            {
                // 0       1    2              3          4      5
                // spotify:user:daniellarsennz:collection:artist:65XA3lk0aG9XejO8y37jjD

                Uri = $"spotify:{uriParts[4]}:{uriParts[5]}";
                FullUri = trimUri;
                Id = FromUriToId(Uri);
                ItemType = TypeFromUri(Uri);
                IsSpotifyUri = true;
                IsUserCollectionUri = true;
                IsValid = type == null || type == ItemType;
                return;
            }

            // Spotify User Playlist URI
            if (SpotifyUserPlaylistUriRegEx.IsMatch(trimUri))
            {
                // 0       1    2          3        4
                // spotify:user:1298341199:playlist:6RTNx0BJWjbmJuEfvMau3r

                Uri = $"spotify:{uriParts[3]}:{uriParts[4]}";
                FullUri = trimUri;
                Id = FromUriToId(Uri);
                ItemType = TypeFromUri(Uri);
                IsSpotifyUri = true;
                IsUserPlaylistUri = true;
                IsValid = type == null || type == ItemType;
                return;
            }
        }

        public bool IsSpotifyUri { get; private set; }

        public bool IsUserPlaylistUri { get; private set; }

        public bool IsUserCollectionUri { get; private set; }

        public bool IsValid { get; private set; }

        public string InputValue { get; private set; }

        public string FullUri { get; private set; }

        public string Uri { get; private set; }

        public string ItemType { get; private set; }

        public string Id { get; private set; }

        private static string TypeFromUri(string uri) => uri.Split(':')[1];

        private static string FromUriToId(string uri) => uri.Split(':').LastOrDefault();
    }
}
