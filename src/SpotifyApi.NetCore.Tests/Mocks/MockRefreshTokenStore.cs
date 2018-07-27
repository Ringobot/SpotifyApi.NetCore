using System;
using Moq;

namespace SpotifyApi.NetCore.Tests.Mocks 
{
    public class MockRefreshTokenStore : Mock<IRefreshTokenStore>
    {
        public MockRefreshTokenStore(string userHash)
        {
            // setup a mock to return the refresh token
            var config = TestsHelper.GetLocalConfig();
            Setup(s=>s.Get(userHash)).ReturnsAsync(config["SpotifyAuthRefreshToken"]);
        }
    }
}