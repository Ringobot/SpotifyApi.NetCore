using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpotifyApi.NetCore.Authorization;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SpotifyApi.NetCore.Tests.Integration
{
    [TestClass]
    [TestCategory("Integration")]
    public class UsageTests
    {
        /// The Usage examples for README.md
        [TestMethod]
        public async Task Usage1()
        {
            // HttpClient and AccountsService can be reused. 
            // Tokens are automatically cached and refreshed
            var http = new HttpClient();
            var accounts = new AccountsService(http, TestsHelper.GetLocalConfig());

            // Get an artist by Spotify Artist Id
            var artists = new ArtistsApi(http, accounts);
            var artist = await artists.GetArtist("1tpXaFf2F55E7kVJON4j4G");
            string artistName = artist.Name;
            Trace.WriteLine($"Artist.Name = {artistName}");

            // Get recommendations based on seed Artist Ids
            var browse = new BrowseApi(http, accounts);
            var result = await browse.GetRecommendations(new[] { "1tpXaFf2F55E7kVJON4j4G", "4Z8W4fKeB5YxbusRsdQVPb" }, null, null);
            string firstTrackName = result.Tracks[0].Name;
            Trace.WriteLine($"First recommendation = {firstTrackName}");

            // Page through a list of tracks in a Playlist
            var playlists = new PlaylistsApi(http, accounts);
            int limit = 100;
            var playlist = await playlists.GetTracks("4h4urfIy5cyCdFOc1Ff4iN", limit: limit);
            int offset = 0;
            int j = 0;
            // using System.Linq
            while (playlist.Items.Any())
            {
                for (int i = 0; i < playlist.Items.Length; i++)
                {
                    Trace.WriteLine($"Track #{j += 1}: {playlist.Items[i].Track.Artists[0].Name} / {playlist.Items[i].Track.Name}");
                }
                offset += limit;
                playlist = await playlists.GetTracks("4h4urfIy5cyCdFOc1Ff4iN", limit: limit, offset: offset);
            }
        }
    }
}