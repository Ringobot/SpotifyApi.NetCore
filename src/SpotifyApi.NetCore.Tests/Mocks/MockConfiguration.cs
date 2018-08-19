using System;
using Microsoft.Extensions.Configuration;
using Moq;

namespace SpotifyApi.NetCore.Tests.Mocks 
{
    public class MockConfiguration : Mock<IConfiguration>
    {
        public MockConfiguration()
        {
            SetupGet(c=>c["SpotifyApiClientId"]).Returns("(SpotifyApiClientId)");
            SetupGet(c=>c["SpotifyApiClientSecret"]).Returns("(SpotifyApiClientSecret)");
            SetupGet(c=>c["SpotifyAuthRedirectUri"]).Returns("(SpotifyAuthRedirectUri)");
            SetupGet(c=>c["SpotifyAuthRefreshToken"]).Returns("(SpotifyAuthRefreshToken)");
        }
    }
}