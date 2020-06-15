using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpotifyApi.NetCore.Authorization;
using SpotifyApi.NetCore.Tests.Mocks;

namespace SpotifyApi.NetCore.Tests.Integration
{
    [TestClass]
    public class AuthorizationCodeFlowTests 
    {
        UserAccountsService _accounts;
        //IRefreshTokenProvider _refreshTokenProvider;

        //const string UserHash = "E11AC28538A7C0A827A726DD9B30B710FC1FCAFFFE2E86FCA853AB90E7C710D2";

        [TestInitialize]
        public void Initialize()
        {
            //_refreshTokenProvider = new MockRefreshTokenStore(UserHash).Object;
            _accounts = new UserAccountsService(new HttpClient(), TestsHelper.GetLocalConfig());
        }

        //[TestMethod]  // only used for manual debugging
        public void ControllerAuthorize1()
        {
            // controller creates state, saves a hash (userHash, state)
            string state = Guid.NewGuid().ToString("N");
     
            // controller encodes userHash and state (this is optional)            
            // controller calls Helper to get Auth URL (userHash, state)
            string url = _accounts.AuthorizeUrl(state, new[]
            {
                "user-modify-playback-state",
                "user-read-playback-state",
                "playlist-read-collaborative",
                "playlist-modify-public",
                "playlist-modify-private",
                "playlist-read-private",
                "user-read-email"
            });

            // controller redirects to URL
        }

        //[TestMethod]  // only used for manual debugging
        public async Task ControllerAuthorize2()
        {
            // spotify calls back to localhost /authorize/spotify
            const string codeParam = "AQCe0Z2XeJBVenMgu11ujAiUTNFk9r2ksjXgd6y4mQRuyMlxvS9qrHjFbugf-M2g91YZXi6a-ZfOWxqFVIg03wYp7LTTspDZqdm9QMelvJYe0jQHEJYwNM3BQ-BEityqruHeOGSAfE7hQcrZSFYFJyJQBMO7awDVem5ha00tkx6OfOBs6k9HKRbaROTKFSO4raowBWOFG9CLQPl6hS1o49DeJsh7mm54PFOgRF2SDZoa_IyGhDrfFvpStu6588N3MAyZ0Bqg-TEJM0M74vlBdbZtxqYqolM2TpHU_Eh6njfN0-u4xzi15rlyvuCGdQPcqLZLvwVRsZqMc_1w5t28jqP2tjv2yBmM0Znknc4A3Y5hSSrbF0hxW_JuzUU544hqQAUrDfDDPRkpk992UAf00E53rR1-4lE";

            // decodes state, gets hash, checks state
            //Assert.AreEqual(_userHashstate.UserHash, decoded.userHash);
            //Assert.AreEqual(_userHashstate.State, decoded.state);

            // controller calls Accounts Service to get access and refresh tokens
            // account service updates store
            var token = await _accounts.RequestAccessRefreshToken(codeParam);
            Trace.WriteLine(token.RefreshToken);
        }
    }
}
