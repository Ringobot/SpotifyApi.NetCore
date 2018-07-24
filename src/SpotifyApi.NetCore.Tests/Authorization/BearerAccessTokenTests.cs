
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpotifyApi.NetCore.Authorization;

namespace SpotifyApi.NetCore.Tests.Authorization
{
    [TestClass]
    public class BearerAccessTokenTests
    {
        [TestMethod]
        public void SetExpires_NotUtcDate_IsUtcDate()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void EnforceInvariants_RefreshTokenNotSet_Throws()
        {
            var token = new BearerAccessRefreshToken{ExpiresIn = 3600};
            token.SetExpires(DateTime.UtcNow);
            token.EnforceInvariants();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void BearerAccessTokenEnforceInvariants_ExpiresNotSet_Throws()
        {
            var token = new BearerAccessToken();
            token.EnforceInvariants();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void BearerAccessRefreshTokenEnforceInvariants_RefreshTokenSetExpiresNotSet_Throws()
        {
            var token = new BearerAccessRefreshToken{RefreshToken = "abc"};
            token.EnforceInvariants();
        }

        [TestMethod]
        public void EnforceInvariants_RefreshTokenAndExpiresSet_DoesNotThrow()
        {
            var token = new BearerAccessRefreshToken{Expires = DateTime.UtcNow, RefreshToken = "abc"};
            token.EnforceInvariants();
        }

        [TestMethod]
        public void SetExpires_ExpiresIn3600_ExpiryIs1HourGreaterThanNow()
        {
            // arrange
            var token = new BearerAccessToken{ExpiresIn = 3600};
            var now = DateTime.UtcNow;

            // act
            token.SetExpires(now);

            // assert
            Assert.AreEqual(now.AddSeconds(3600), token.Expires.Value);
        }
    }
}