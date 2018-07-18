using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SpotifyApi.NetCore.Tests
{
    [TestClass]
    [TestCategory("Integration")]
    public class PlayerApiTests
    {
        [TestMethod]
        public async Task Play_SpotifyUri_200Response()
        {
            // arrange
            const string userHash = "E11AC28538A7C0A827A726DD9B30B710FC1FCAFFFE2E86FCA853AB90E7C710D2";
            const string spotifyUri = "spotify:user:palsvensson:playlist:2iL5fr6OmN8f4yoQvvuWSf";
            
            var mockAuthUser = new Mock<IUserAuthEntity>();
            mockAuthUser.SetupProperty(u=>u.UserHash, userHash);

            var http = new HttpClient();
            var mockData = new Mock<IUserAuthData>();
            mockData.Setup(d=>d.Get(It.IsAny<string>())).ReturnsAsync(mockAuthUser.Object);

            var auth = new UserAuthApi(http, TestsHelper.GetLocalConfig(), mockData.Object);
            var api = new PlayerApi(http, auth);

            // act
            await api.PlayContext(userHash,spotifyUri);
            
            // assert
        }
    }
}