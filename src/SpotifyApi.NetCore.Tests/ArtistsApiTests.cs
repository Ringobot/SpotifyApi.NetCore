using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using SpotifyApi.NetCore;

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
        public async Task GetArtist_ArtistName_FirstArtistNameMatches()
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
    }
}