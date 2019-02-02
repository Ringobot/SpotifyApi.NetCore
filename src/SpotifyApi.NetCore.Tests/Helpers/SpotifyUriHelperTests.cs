using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpotifyApi.NetCore.Helpers;

namespace SpotifyApi.NetCore.Tests.Helpers
{
    [TestClass]
    public class SpotifyUriHelperTests
    {
        [TestMethod]
        public void PlaylistUri_PlaylistUriMoreThan3Parts_ExtractsValidUri()
        {
            // arrange
            const string playlistUri = "spotify:playlist:0TnOYISbd1XYRBk9myaseg";
            string fullUri = $"spotify:user:{playlistUri}";

            // act
            string uri = SpotifyUriHelper.PlaylistUri(fullUri);

            // assert
            Assert.AreEqual(playlistUri, uri);
        }

        [TestMethod]
        public void PlaylistUri_NumericUsername_ExtractsValidUri()
        {
            // arrange
            const string fullUri = "spotify:user:1298341199:playlist:6RTNx0BJWjbmJuEfvMau3r";

            // act
            string uri = SpotifyUriHelper.PlaylistUri(fullUri);

            // assert
            Assert.AreEqual(fullUri, uri);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArtistUri_ArtistUriNull_ThrowsArgumentException()
        {
            // arrange
            const string uri = null;

            // act
            SpotifyUriHelper.ArtistUri(uri);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArtistUri_ArtistUriEmptyString_ThrowsArgumentException()
        {
            // arrange
            const string uri = "";

            // act
            SpotifyUriHelper.ArtistUri(uri);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ArtistUri_ArtistUriWhitespaceString_ThrowsArgumentException()
        {
            // arrange
            const string uri = " ";

            // act
            SpotifyUriHelper.ArtistUri(uri);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ArtistUri_ArtistUriLeadingWhitespace_ThrowsArgumentException()
        {
            // arrange
            const string uri = " spotify:album:0TnOYISbd1XYRBk9myaseg";

            // act
            SpotifyUriHelper.ArtistUri(uri);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ArtistUri_AlbumUri_ThrowsArgumentException()
        {
            // arrange
            const string uri = "spotify:album:0TnOYISbd1XYRBk9myaseg";

            // act
            SpotifyUriHelper.ArtistUri(uri);
        }

        [TestMethod]
        public void ArtistUri_ValidArtistUri_ReturnsArtistUri()
        {
            // arrange
            const string uri = "spotify:artist:0TnOYISbd1XYRBk9myaseg";

            // act
            string result = SpotifyUriHelper.ArtistUri(uri);

            // assert
            Assert.AreSame(uri, result);
        }

        [TestMethod]
        public void AlbumUri_ValidAlbumUri_ReturnsAlbumUri()
        {
            // arrange
            const string uri = "spotify:album:0TnOYISbd1XYRBk9myaseg";

            // act
            string result = SpotifyUriHelper.AlbumUri(uri);

            // assert
            Assert.AreSame(uri, result);
        }

        [TestMethod]
        public void PlaylistUri_ValidPlaylistUri_ReturnsPlaylistUri()
        {
            // arrange
            const string uri = "spotify:playlist:0TnOYISbd1XYRBk9myaseg";

            // act
            string result = SpotifyUriHelper.PlaylistUri(uri);

            // assert
            Assert.AreSame(uri, result);
        }

        [TestMethod]
        public void TrackUri_ValidTrackUri_ReturnsTrackUri()
        {
            // arrange
            const string uri = "spotify:track:0TnOYISbd1XYRBk9myaseg";

            // act
            string result = SpotifyUriHelper.TrackUri(uri);

            // assert
            Assert.AreSame(uri, result);
        }

    }
}
