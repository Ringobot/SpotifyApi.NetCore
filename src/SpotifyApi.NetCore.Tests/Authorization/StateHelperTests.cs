
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpotifyApi.NetCore.Authorization;

namespace SpotifyApi.NetCore.Tests.Authorization
{
    [TestClass]
    public class StateHelperTests
    {
        [TestMethod]
        public void EncodeState_UserHashAndState_EqualsDecodeState()
        {
            // arrange
            (string, string) userAuth = ("E11AC28538A7C0A827A726DD9B30B710FC1FCAFFFE2E86FCA853AB90E7C710D2", "e80aa62d1eec4041946386b1fe5ad055");

            // act
            var encoded = StateHelper.EncodeState(userAuth);

            // assert
            // EncodeState . DecodeState is a deterministic function
            Assert.AreEqual(StateHelper.DecodeState(encoded), userAuth, "The tuple returned by DecodeState should match exactly the input to EncodeState");
        }

        [TestMethod]
        public void UserHash_UserName_MatchesExpectedValue()
        {
            // arrange
            const string userName = "DanielLarsenNZ";
            const string expected = "0429593EB273D4F3116216A2FEF8CF4EF9ADA6D3246C8AFD1234050CE2D10B27"; //daniellarsennz https://passwordsgenerator.net/sha256-hash-generator/

            // act
            string result = StateHelper.UserHash(userName);

            // assert
            Assert.AreEqual(expected, result);
        }
    }
}