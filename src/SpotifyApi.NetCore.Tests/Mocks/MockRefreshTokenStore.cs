using System;
using Microsoft.Extensions.Configuration;
using Moq;
using SpotifyApi.NetCore.Authorization;

namespace SpotifyApi.NetCore.Tests.Mocks 
{
    public class MockRefreshTokenStore : Mock<IRefreshTokenProvider>
    {
        public MockRefreshTokenStore(string userHash, IConfiguration config = null)
        {
            // setup a mock to return the refresh token
            config = config ?? TestsHelper.GetLocalConfig();
            Setup(s=>s.GetRefreshToken(userHash)).ReturnsAsync(config["SpotifyAuthRefreshToken"]);
        }
    }
}