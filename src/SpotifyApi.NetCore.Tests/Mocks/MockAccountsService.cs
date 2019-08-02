using System;
using Moq;
using SpotifyApi.NetCore.Authorization;

namespace SpotifyApi.NetCore.Tests.Mocks
{
    public class MockAccountsService : Mock<IAccessTokenProvider>
    {
        public MockAccountsService()
        {
            const string token = "abcd1234";
            Setup(s => s.GetAccessToken()).ReturnsAsync(token);
        }
    }
}