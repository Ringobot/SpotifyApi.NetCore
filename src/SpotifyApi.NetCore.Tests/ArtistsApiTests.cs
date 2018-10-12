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
        public async Task GetArtists_2ValidIds_UrlEncodeDoesNotChangeCommaDelimitedIds()
        {
            // arrange
            string[] artists = new[] { "1tpXaFf2F55E7kVJON4j4G", "0oSGxfWSnnOXhD2fKuz2Gy" };

            var mockHttp = new MockHttpClient();
            var accounts = new MockAccountsService().Object;
            var mockArtists = new Mock<ArtistsApi>(mockHttp.HttpClient, accounts) { CallBase = true };
            mockArtists.Setup(a => a.Get<Artist[]>(It.IsAny<string>())).ReturnsAsync(new Artist[] { });

            // act
            await mockArtists.Object.GetArtists(artists);

            // assert
            string expected = $"{SpotifyWebApi.BaseUrl}/artists?ids={string.Join(",", artists)}";
            mockArtists.Verify(a => a.Get<Artist[]>(expected), Times.Once(), "Comma should not be encoded");
        }

        [TestMethod]
        public async Task GetArtists_InjectingJavaScript_ScriptTagIsEncoded()
        {
            // arrange
            string[] artists = new[] { "<script>alert('pwnd')</script>", "0oSGxfWSnnOXhD2fKuz2Gy" };

            var mockHttp = new MockHttpClient();
            var accounts = new MockAccountsService().Object;
            var mockArtists = new Mock<ArtistsApi>(mockHttp.HttpClient, accounts) { CallBase = true };
            mockArtists.Setup(a => a.Get<Artist[]>(It.IsAny<string>())).ReturnsAsync(new Artist[] { });

            // act
            await mockArtists.Object.GetArtists(artists);

            // assert
            string expected = $"{SpotifyWebApi.BaseUrl}/artists?ids={string.Join(",", artists.Select(WebUtility.UrlEncode))}";
            mockArtists.Verify(a => a.Get<Artist[]>(expected), Times.Once(), "script tags should be encoded");
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