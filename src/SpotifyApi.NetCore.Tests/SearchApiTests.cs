using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Linq;
using SpotifyApi.NetCore.Authorization;

namespace SpotifyApi.NetCore.Tests
{
    [TestClass]
    public class SearchApiTests
    {
        [TestCategory("Integration")]
        [TestMethod]
        public async Task Search_PrinceArtistType_PrinceFoundInArtistResults()
        {
            // arrange
            const string query = "prince";
            string[] types = new[] { SpotifySearchTypes.Artist };

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new SearchApi(http, accounts);

            // act
            var response = await api.Search<SearchResult>(query, types, null, (0, 0));

            // assert
            Assert.IsTrue(response.Artists.Items.Any(i=>i.Name == "Prince"));
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task Search_RevolverAlbumType_AlbumItemsGreaterThanZero()
        {
            // arrange
            const string query = "revolver";
            string[] types = new[] { SpotifySearchTypes.Album };

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new SearchApi(http, accounts);

            // act
            var response = await api.Search<SearchResult>(query, types, null, (0, 0));

            // assert
            Assert.IsTrue(response.Albums.Items.Length > 0);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task Search_LucyQueryTrackType_TracksItemsGreaterThanZero()
        {
            // arrange
            const string query = "lucy";
            string[] types = new[] { SpotifySearchTypes.Track };

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new SearchApi(http, accounts);

            // act
            var response = await api.Search<SearchResult>(query, types, null, (0, 0));

            // assert
            Assert.IsTrue(response.Tracks.Items.Length > 0);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task Search_DanceQueryPlaylistType_PlaylistsItemsGreaterThanZero()
        {
            // arrange
            const string query = "dance";
            string[] types = new[] { SpotifySearchTypes.Playlist };

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new SearchApi(http, accounts);

            // act
            var response = await api.Search<SearchResult>(query, types, null, (0, 0));

            // assert
            Assert.IsTrue(response.Playlists.Items.Length > 0);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task Search_RevivalQueryAlbumArtistTrackTypes_AlbumsArtistsTracksItemsGreaterThanZero()
        {
            // arrange
            const string query = "revival";
            string[] types = new[] { SpotifySearchTypes.Album, SpotifySearchTypes.Artist, SpotifySearchTypes.Track };

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new SearchApi(http, accounts);

            // act
            var response = await api.Search<SearchResult>(query, types, null, (0, 0));

            // assert
            Assert.IsTrue(response.Albums.Items.Length > 0
                && response.Artists.Items.Length > 0
                && response.Tracks.Items.Length > 0);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task Search_Limit5_TracksItemsLengthEquals5()
        {
            // arrange
            const string query = "dance";
            string[] types = new[] { SpotifySearchTypes.Track };
            var limitOffset = (5, 0);

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new SearchApi(http, accounts);

            // act
            var response = await api.Search<SearchResult>(query, types, null, limitOffset);

            // assert
            Assert.AreEqual(limitOffset.Item1, response.Tracks.Items.Length);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task Search_Limit5Offset5_TracksOffsetEquals5()
        {
            // arrange
            const string query = "dance";
            string[] types = new[] { SpotifySearchTypes.Track };
            var limitOffset = (5, 9);

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new SearchApi(http, accounts);

            // act
            var response = await api.Search<SearchResult>(query, types, null, limitOffset);

            // assert
            Assert.AreEqual(limitOffset.Item2, response.Tracks.Offset);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task Search_TrackIsrc_CorrectTrackReturned()
        {
            // arrange
            const string isrc = "USUM71703861";
            const string query = "isrc:" + isrc;
            string[] types = new[] { SpotifySearchTypes.Track };
            var limitOffset = (1, 0);

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new SearchApi(http, accounts);

            // act
            var response = await api.Search<SearchResult>(query, types, null, limitOffset);

            // assert
            Assert.AreEqual(isrc, response.Tracks.Items[0].ExternalIds.Isrc);
        }

        [TestCategory("Integration")]
        [TestMethod]
        public async Task Search_NonExistentIsrc_ItemsZero()
        {
            // arrange
            const string isrc = "NOPE12345678";
            const string query = "isrc:" + isrc;
            string[] types = new[] { SpotifySearchTypes.Track };
            var limitOffset = (1, 0);

            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            var api = new SearchApi(http, accounts);

            // act
            var response = await api.Search<SearchResult>(query, types, null, limitOffset);

            // assert
            Assert.AreEqual(0, response.Tracks.Items.Length);
        }

    }
}