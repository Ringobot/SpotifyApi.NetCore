using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using SpotifyApi.NetCore;
using SpotifyApi.NetCore.Tests.Mocks;
using Moq;
using System.Linq;
using System.Net;

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

        [TestMethod]
        public void EncodeArtistIds_2ValidIds_UrlEncodeDoesNotChangeCommaDelimitedIds()
        {
            // arrange
            string[] artists = new[] { "1tpXaFf2F55E7kVJON4j4G", "0oSGxfWSnnOXhD2fKuz2Gy" };

            // act
            string ids = ArtistsApi.EncodeArtistIds(artists);

            // assert
            string expected = string.Join(",", artists);
            Assert.AreEqual(expected, ids, "comma should not be encoded");
        }

        [TestMethod]
        public void EncodeArtistIds_InjectingJavaScript_ScriptTagIsEncoded()
        {
            // arrange
            string[] artists = new[] { "<script>alert('pwnd')</script>", "0oSGxfWSnnOXhD2fKuz2Gy" };

            // act
            string ids = ArtistsApi.EncodeArtistIds(artists);

            // assert
            string expected = string.Join(",", artists.Select(WebUtility.UrlEncode));
            Assert.AreEqual(expected, ids, "script tags should be encoded");
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

    }
}