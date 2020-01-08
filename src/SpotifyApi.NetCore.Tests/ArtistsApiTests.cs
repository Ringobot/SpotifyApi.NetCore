using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using SpotifyApi.NetCore.Authorization;

namespace SpotifyApi.NetCore.Tests
{
    [TestClass]
    public class ArtistsApiTests
    {
        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetArtist_ArtistsId_CorrectArtistName()
        {
            // arrange
            const string artistId = "1tpXaFf2F55E7kVJON4j4G";

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new ArtistsApi(http, accounts);

            // act
            var response = await api.GetArtist(artistId);

            // assert
            Assert.AreEqual("Black Rebel Motorcycle Club", response.Name);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task SearchArtists_ArtistName_FirstArtistNameMatches()
        {
            // arrange
            const string artistName = "Radiohead";
            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var artists = new ArtistsApi(http, accounts);

            // act
            var result = await artists.SearchArtists(artistName, 3);

            // assert
            Assert.AreEqual(artistName, result.Artists.Items[0].Name);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task GetArtists_2ValidArtists_ArtistIdsMatch()
        {
            // arrange
            string[] artistIds = new[] { "1tpXaFf2F55E7kVJON4j4G", "0oSGxfWSnnOXhD2fKuz2Gy" };
            
            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());
            var artists = new ArtistsApi(http, accounts);

            // act
            var result = await artists.GetArtists(artistIds);

            // assert
            Assert.AreEqual(artistIds[0], result[0].Id);
            Assert.AreEqual(artistIds[1], result[1].Id);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task GetArtistsTopTracks_NZCountryCode_ArtistIdMatches()
        {
            // arrange
            const string artistId = "1tpXaFf2F55E7kVJON4j4G";
            const string market = SpotifyCountryCodes.New_Zealand;

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());
            var artists = new ArtistsApi(http, accounts);

            // act
            var result = await artists.GetArtistsTopTracks(artistId, market);

            // assert
            Assert.AreEqual(artistId, result[0].Artists[0].Id);
        }

        [TestMethod]
        [ExpectedException(typeof(SpotifyApiErrorException))]
        [TestCategory("Integration")]
        public async Task GetArtistsTopTracks_FromAppToken_SpotifyErrorException()
        {
            // arrange
            const string artistId = "1tpXaFf2F55E7kVJON4j4G";
            const string market = SpotifyCountryCodes._From_Token;

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());
            var artists = new ArtistsApi(http, accounts);

            // act
            var result = await artists.GetArtistsTopTracks(artistId, market);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task GetRelatedArtists_When_Given_Valid_ArtistId_Returns_Artists()
        {
            // arrange
            const string artistId = "6lcwlkAjBPSKnFBZjjZFJs";

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());
            var artists = new ArtistsApi(http, accounts);

            // act
            var result = await artists.GetRelatedArtists(artistId);

            // assert
            Assert.AreNotSame(result.Length, 0);
        }
    }
}