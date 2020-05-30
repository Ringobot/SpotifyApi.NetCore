using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using SpotifyApi.NetCore.Extensions;

namespace SpotifyApi.NetCore.Tests.Extensions
{
    [TestClass]
    public class UriBuilderExtensionsTests
    {
        [TestMethod]
        public void AppendToQuery_OneParam_WellFormed()
        {
            // arrange
            var builder = new UriBuilder("https://api.spotify.com/v1/browse/new-releases");

            // act
            builder.AppendToQuery("country", "nz");

            // assert
            Assert.AreEqual("?country=nz", builder.Uri.Query);
        }

        [TestMethod]
        public void AppendToQuery_TwoParams_WellFormed()
        {
            // arrange
            var builder = new UriBuilder("https://api.spotify.com/v1/browse/new-releases");

            // act
            builder.AppendToQuery("country", "nz");
            builder.AppendToQuery("limit", 10);

            // assert
            Assert.AreEqual("?country=nz&limit=10", builder.Uri.Query);
        }

        [TestMethod]
        public void AppendToQuery_ParamInOriginalUrlString_WellFormed()
        {
            // arrange
            var builder = new UriBuilder("https://api.spotify.com/v1/browse/new-releases?local=en-nz");

            // act
            builder.AppendToQuery("country", "nz");
            builder.AppendToQuery("limit", 10);

            // assert
            Assert.AreEqual("?local=en-nz&country=nz&limit=10", builder.Uri.Query);
        }

        [TestMethod]
        public void AppendToQuery_TwoParamsDifferentValues_WellFormed()
        {
            // arrange
            var builder = new UriBuilder("https://api.spotify.com/v1/browse/new-releases");

            // act
            builder.AppendToQuery("artist", "radiohead");
            builder.AppendToQuery("artist", "metallica");

            // assert
            Assert.AreEqual("?artist=radiohead&artist=metallica", builder.Uri.Query);
        }

        [TestMethod]
        public void AppendToQuery_SpaceInParamValue_EscapedAndWellFormed()
        {
            // arrange
            var builder = new UriBuilder("https://api.spotify.com/v1/browse/new-releases");

            // act
            builder.AppendToQuery("artist", "Massive Attack");

            // assert
            Assert.AreEqual("?artist=Massive%20Attack", builder.Uri.Query);
        }

        [TestMethod]
        public void AppendToQueryIfValueNotNullOrWhiteSpace_QueryNotSetAndValueIsNull_QueryIsNullEmpty()
        {
            // arrange
            var builder = new UriBuilder("https://api.spotify.com/v1/browse/new-releases");

            // act
            builder.AppendToQueryIfValueNotNullOrWhiteSpace("artist", null);

            // assert
            Assert.IsTrue(string.IsNullOrEmpty(builder.Uri.Query));
        }

        [TestMethod]
        public void AppendToQueryIfValueNotNullOrWhiteSpace_QuerySetAndValueIsNull_QueryNotAppended()
        {
            // arrange
            var builder = new UriBuilder("https://api.spotify.com/v1/browse/new-releases");
            builder.AppendToQuery("1", "1");

            // act
            builder.AppendToQueryIfValueNotNullOrWhiteSpace("2", null);

            // assert
            Assert.AreEqual("?1=1", builder.Uri.Query);
        }

        [TestMethod]
        public void AppendToQueryIfValueGreaterThan0_QuerySetAndValueIsNull_QueryNotAppended()
        {
            // arrange
            var builder = new UriBuilder("https://api.spotify.com/v1/browse/new-releases");
            builder.AppendToQuery("1", "1");

            // act
            builder.AppendToQueryIfValueGreaterThan0("2", null);

            // assert
            Assert.AreEqual("?1=1", builder.Uri.Query);
        }

        [TestMethod]
        public void AppendToQueryIfValueGreaterThan0_QueryNotSetAndValueIs0_QueryIsNullOrEmpty()
        {
            // arrange
            var builder = new UriBuilder("https://api.spotify.com/v1/browse/new-releases");

            // act
            builder.AppendToQueryIfValueGreaterThan0("1", 0);

            // assert
            Assert.IsTrue(string.IsNullOrEmpty(builder.Uri.Query));
        }

        [TestMethod]
        public void AppendToQueryAsCsv_ThreeValues_WellFormed()
        {
            // arrange
            var builder = new UriBuilder("https://api.spotify.com/v1/browse/new-releases");

            // act
            builder.AppendToQueryAsCsv("numbers", new[] { "1", "2", "3" });

            // assert
            Assert.AreEqual("?numbers=1,2,3", builder.Uri.Query);
        }

        [TestMethod]
        public void AppendToQueryAsTimestampIso8601_DateTime_ExpectedFormat()
        {
            // arrange
            var builder = new UriBuilder("https://api.spotify.com/v1/browse/featured-playlists");

            // act
            builder.AppendToQueryAsTimestampIso8601("timestamp", new DateTime(2014, 10, 23, 9, 0, 0));

            // assert
            Assert.AreEqual("?timestamp=2014-10-23T09:00:00", builder.Uri.Query);
        }

    }
}
