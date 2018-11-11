using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;

namespace SpotifyApi.NetCore.Tests
{
    [TestClass]
    public class SearchApiTests
    {
        [TestCategory("Integration")]
        [TestMethod]
        public async Task Search_PrinceArtistType_FirstArtistResultIsPrince()
        {
            // arrange
            const string query = "prince";

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new SearchApi(http, accounts);

            // act
            var response = await api.Search<SearchResult>(query, new []{SpotifySearchTypes.Artist}, null, (0,0));

            // assert
            Assert.AreEqual("Prince", response.Artists.Items[0].Name);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task Search_RevolverAlbumType_FirstAlbumResultIsRevolver()
        {
            // arrange
            const string query = "revolver";

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new SearchApi(http, accounts);

            // act
            var response = await api.Search<SearchResult>(query, new []{SpotifySearchTypes.Album}, null, (0,0));

            // assert
            Assert.AreEqual("Revolver", response.Artists.Items[0].Name);
        }


    }
}