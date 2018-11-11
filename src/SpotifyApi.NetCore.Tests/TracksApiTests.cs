using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;

namespace SpotifyApi.NetCore.Tests
{
    [TestClass]
    public class TracksApiTests
    {
        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetTrack_TrackId_CorrectTrackName()
        {
            // arrange
            const string trackId = "5lA3pwMkBdd24StM90QrNR";

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new TracksApi(http, accounts);

            // act
            var response = await api.GetTrack(trackId);

            // assert
            Assert.AreEqual("P.Y.T. (Pretty Young Thing)", response.Name);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetTrack_TrackIdNoMarket_MarketsArrayExists()
        {
            // arrange
            const string trackId = "5lA3pwMkBdd24StM90QrNR";

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new TracksApi(http, accounts);

            // act
            var response = await api.GetTrack(trackId);

            // assert
            Assert.IsTrue(response.AvailableMarkets.Length > 0);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetTrack_TrackIdMarket_AvailableMarketsIsNull()
        {
            // arrange
            const string trackId = "5lA3pwMkBdd24StM90QrNR";
            const string market = SpotifyCountryCodes.New_Zealand;

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new TracksApi(http, accounts);

            // act
            var response = await api.GetTrack(trackId, market);

            // assert
            Assert.IsNull(response.AvailableMarkets);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetTracks_TrackIds_CorrectTrackNames()
        {
            // arrange
            string[] trackIds = new[] { "11dFghVXANMlKmJXsNCbNl", "20I6sIOMTCkB6w7ryavxtO", "7xGfFoTpQ2E7fRF5lN10tr" };

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new TracksApi(http, accounts);

            // act
            var response = await api.GetTracks(trackIds);

            // assert
            Assert.AreEqual("Cut To The Feeling", response[0].Name);
            Assert.AreEqual("Call Me Maybe", response[1].Name);
            Assert.AreEqual("Run Away With Me", response[2].Name);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetTracks_TrackIdsNoMarket_MarketsArrayPopulated()
        {
            // arrange
            string[] trackIds = new[] { "5lA3pwMkBdd24StM90QrNR", "20I6sIOMTCkB6w7ryavxtO", "7xGfFoTpQ2E7fRF5lN10tr" };

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new TracksApi(http, accounts);

            // act
            var response = await api.GetTracks(trackIds);

            // assert
            Assert.IsTrue(response[0].AvailableMarkets.Length > 0);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetTracks_TrackIdsMarket_AvailableMarketsIsNull()
        {
            // arrange
            string[] trackIds = new[] { "5lA3pwMkBdd24StM90QrNR", "20I6sIOMTCkB6w7ryavxtO", "7xGfFoTpQ2E7fRF5lN10tr" };

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());
            var api = new TracksApi(http, accounts);

            // act
            var response = await api.GetTracks(trackIds, SpotifyCountryCodes.New_Zealand);

            // assert
            Assert.IsNull(response[0].AvailableMarkets);
        }

        [TestCategory("Integration")]
        //[TestMethod] // not a reliable test - intermittent gateway timeout on Spotify end
        public async Task GetTrackAudioAnalysis_TrackId_BarsIsNotNullOrZero()
        {
            // arrange
            const string trackId = "5lA3pwMkBdd24StM90QrNR";

            var http = new HttpClient();
            http.Timeout = TimeSpan.FromSeconds(30);
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new TracksApi(http, accounts);

            // act
            var response = await api.GetTrackAudioAnalysis(trackId);

            // assert
            Assert.IsTrue(response.Bars != null && response.Bars.Length > 0);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetTrackAudioFeatures_TrackId_TempoNotZero()
        {
            // arrange
            const string trackId = "5lA3pwMkBdd24StM90QrNR";

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new TracksApi(http, accounts);

            // act
            var response = await api.GetTrackAudioFeatures(trackId);

            // assert
            Assert.IsTrue(response.Tempo != 0);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetTracksAudioFeatures_TrackIds_ThreeFeatures()
        {
            // arrange
            string[] trackIds = new[] { "5lA3pwMkBdd24StM90QrNR", "20I6sIOMTCkB6w7ryavxtO", "7xGfFoTpQ2E7fRF5lN10tr" };

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new TracksApi(http, accounts);

            // act
            var response = await api.GetTracksAudioFeatures(trackIds);

            // assert
            Assert.IsTrue(response != null && response.Length == 3);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task SearchTracks_Isrc_CorrectResult()
        {
            // arrange
            const string isrc = "USUM71703861";
            const string query = "isrc:" + isrc;

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new TracksApi(http, accounts);

            // act
            var response = await api.SearchTracks(query);

            // assert
            Assert.AreEqual(isrc, response.Items[0].ExternalIds.Isrc);
        }

    }
}