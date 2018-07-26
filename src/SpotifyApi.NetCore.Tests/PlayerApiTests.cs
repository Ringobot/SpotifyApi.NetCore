using System;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpotifyApi.NetCore.Authorization;
using SpotifyApi.NetCore.Tests.Integration;

namespace SpotifyApi.NetCore.Tests
{
    [TestClass]
    [TestCategory("Integration")]
    public class PlayerApiTests
    {
        //MemoryBearerTokenStore _store = new MemoryBearerTokenStore();

        [TestMethod]
        [TestCategory("Integration")]
        public async Task PlayContext_SpotifyUri_200Response()
        {
            // arrange
            const string userHash = "E11AC28538A7C0A827A726DD9B30B710FC1FCAFFFE2E86FCA853AB90E7C710D2";
            const string spotifyUri = "spotify:user:palsvensson:playlist:2iL5fr6OmN8f4yoQvvuWSf";
            
            var http = new HttpClient();
            //var store = new MemoryBearerTokenStore<BearerAccessRefreshToken>();
            var config = TestsHelper.GetLocalConfig();
            
            var accounts = new UserAccountsService(http, config, new MockRefreshTokenStore(userHash).Object, null);
            var api = new PlayerApi(http, accounts);

            // act
            try
            {
                await api.PlayContext(userHash,spotifyUri);    
            }
            catch (System.Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
            
            
            // assert
        }
    }
}