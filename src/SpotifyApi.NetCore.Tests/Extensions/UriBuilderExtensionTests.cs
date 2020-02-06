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
        public void AppendQueryParam_OneParam_WellFormed()
        {
            // arrange
            var builder = new UriBuilder("https://api.spotify.com/v1/browse/new-releases");

            // act
            builder.AppendToQuery("country", "nz");

            // assert
            Assert.AreEqual("?country=nz", builder.Uri.Query);
        }

        [TestMethod]
        public void AppendQueryParam_TwoParams_WellFormed()
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
        public void AppendQueryParam_ParamInOriginalUrlString_WellFormed()
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
        public void AppendQueryParam_TwoParamsDifferentValues_WellFormed()
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
        public void AppendQueryParam_SpaceInParamValue_EscapedAndWellFormed()
        {
            // arrange
            var builder = new UriBuilder("https://api.spotify.com/v1/browse/new-releases");

            // act
            builder.AppendToQuery("artist", "Massive Attack");

            // assert
            Assert.AreEqual("?artist=Massive%20Attack", builder.Uri.Query);
        }
    }
}
