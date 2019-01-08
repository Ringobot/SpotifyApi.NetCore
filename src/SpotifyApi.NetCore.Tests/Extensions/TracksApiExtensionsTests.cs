using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using SpotifyApi.NetCore.Extensions;
using Moq;

namespace SpotifyApi.NetCore.Tests.Extensions
{
    [TestClass]
    public class TracksApiExtensionsTests
    {
        [TestMethod]
        public async Task GetTrackByIsrcCode_ValidIsrc_ReturnsFirstItem()
        {
            // arrange
            const string isrc = "GB0409700200";
            var tracks = new[] { new Track { ExternalIds = new ExternalIds { Isrc = "GB0409700200" } } };
            var mockTracksApi = new Mock<ITracksApi>();
            mockTracksApi
                .Setup(a => a.SearchTracks(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<(int, int)>(), It.IsAny<string>()))
                .ReturnsAsync(new TracksSearchResult { Items = tracks });

            // act
            var track = await TracksApiExtensions.GetTrackByIsrcCode(mockTracksApi.Object, isrc);

            // assert
            Assert.AreEqual(tracks[0], track);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task GetTrackByIsrcCode_InvalidIsrc_Exception()
        {
            // arrange
            const string isrc = "NOPE";
            var mockTracksApi = new Mock<ITracksApi>();

            // act
            var track = await TracksApiExtensions.GetTrackByIsrcCode(mockTracksApi.Object, isrc);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetTrackByIsrcCode_Isrc_CorrectResult()
        {
            // arrange
            const string isrc = "GBBKS1700108";

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new TracksApi(http, accounts);

            // act
            var track = await api.GetTrackByIsrcCode(isrc);

            // assert
            Assert.AreEqual(isrc, track.ExternalIds.Isrc);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetTrackByIsrcCode_NonExistentIsrc_ResultIsNull()
        {
            // arrange
            const string isrc = "NOPE12345678";

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new TracksApi(http, accounts);

            // act
            var track = await api.GetTrackByIsrcCode(isrc);

            // assert
            Assert.IsNull(track);
        }

    }
}