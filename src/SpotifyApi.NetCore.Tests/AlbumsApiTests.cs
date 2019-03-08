using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore.Tests
{
    [TestClass]
    public class AlbumsApiTests
    {
        [TestCategory("Integration")]
        [TestMethod]
        public async Task GetAlbum_AlbumsId_CorrectAlbumName()
        {
            // arrange
            // spotify:album:5ObHI23lQY2S5FGizlNrob
            const string albumId = "5ObHI23lQY2S5FGizlNrob";

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new AlbumsApi(http, accounts);

            // act
            var response = await api.GetAlbum(albumId);

            // assert
            Assert.AreEqual("Trojan Presents: Dub", response.Name);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task SearchAlbums_AlbumName_FirstAlbumNameMatches()
        {
            // arrange
            const string albumName = "Trojan Presents: Dub";
            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var albums = new AlbumsApi(http, accounts);

            // act
            var result = await albums.SearchAlbums(albumName);

            // assert
            Assert.AreEqual(albumName, result.Albums.Items[0].Name);
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task GetAlbums_2ValidAlbums_AlbumIdsMatch()
        {
            // arrange
            // spotify:album:49PXnWG6cuZbCZSolJWrYa
            string[] albumIds = new[] { "5ObHI23lQY2S5FGizlNrob", "49PXnWG6cuZbCZSolJWrYa" };

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());
            var albums = new AlbumsApi(http, accounts);

            // act
            var result = await albums.GetAlbums(albumIds);

            // assert
            Assert.AreEqual(albumIds[0], result[0].Id);
            Assert.AreEqual(albumIds[1], result[1].Id);
        }

    }
}
