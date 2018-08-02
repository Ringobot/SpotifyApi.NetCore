using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpotifyApi.NetCore.Authorization;
using SpotifyApi.NetCore.Tests.Mocks;

namespace SpotifyApi.NetCore.Tests.Integration
{
    [TestClass]
    [TestCategory("Integration")]
    public class CompetingAccessTokenRequests
    {
        [TestMethod]
        public async Task TwoCompetingAccessTokenRequestsGetConsistentResults()
        {
            const string artistId = "1tpXaFf2F55E7kVJON4j4G";

            var http1 = new HttpClient();
            var accounts1 = new AccountsService(http1, TestsHelper.GetLocalConfig());
            var artists1 = new ArtistsApi(http1, accounts1);

            var http2 = new HttpClient();
            var accounts2 = new AccountsService(http2, TestsHelper.GetLocalConfig());
            var artists2 = new ArtistsApi(http2, accounts2);

            // act
            await artists1.GetArtist(artistId);
            await artists2.GetArtist(artistId);

            // assert
            // no error
        }

        [TestMethod]
        public async Task TwoCompetingAccessTokenRequestsSameHttpClientGetConsistentResults()
        {
            const string artistId = "1tpXaFf2F55E7kVJON4j4G";

            var http1 = new HttpClient();
            var accounts1 = new AccountsService(http1, TestsHelper.GetLocalConfig());
            var artists1 = new ArtistsApi(http1, accounts1);

            var accounts2 = new AccountsService(http1, TestsHelper.GetLocalConfig());
            var artists2 = new ArtistsApi(http1, accounts2);

            // act
            await artists1.GetArtist(artistId);
            await artists2.GetArtist(artistId);

            // assert
            // no error
        }

        //TODO: Something is up - the first error is not being thrown to the test runner...
        //[TestMethod]
        public async Task TwoCompetingUserAccessTokenRequestsGetConsistentResults()
        {
            const string userHash = "E11AC28538A7C0A827A726DD9B30B710FC1FCAFFFE2E86FCA853AB90E7C710D2";
            const string spotifyUri = "spotify:user:palsvensson:playlist:2iL5fr6OmN8f4yoQvvuWSf";

            var store = new MockRefreshTokenStore(userHash).Object;

            var http1 = new HttpClient();
            var accounts1 = new UserAccountsService(http1, TestsHelper.GetLocalConfig(), store);
            var player1 = new PlayerApi(http1, accounts1);

            var http2 = new HttpClient();
            var accounts2 = new UserAccountsService(http2, TestsHelper.GetLocalConfig(), store);
            var player2 = new PlayerApi(http2, accounts2);

            // act
            //try
            //{
                //TODO: Call Device method instead
                await player1.PlayContext(userHash, spotifyUri);
            //}
            //catch (SpotifyApiErrorException ex)
            //{
                //Trace.WriteLine(ex.Message);
            //}

            //try
            //{
                await player2.PlayContext(userHash, spotifyUri);
            //}
            //catch (SpotifyApiErrorException ex)
            //{
              //  Trace.WriteLine(ex.Message);
            //}

            // assert
            // no error
        }
    }
}
