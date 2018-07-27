using System;
using Moq;

namespace SpotifyApi.NetCore.Tests.Mocks
{
    public class MockAccountsService : Mock<IAccountsService>
    {
        public MockAccountsService()
        {
            var token = new BearerAccessToken { AccessToken = "abcd1234", ExpiresIn = 3600 };
            token.SetExpires(DateTime.UtcNow);
            Setup(s => s.GetAppAccessToken()).ReturnsAsync(token);
        }
    }
}