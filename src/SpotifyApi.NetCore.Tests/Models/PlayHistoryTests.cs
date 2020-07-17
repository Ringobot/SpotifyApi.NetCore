using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace SpotifyApi.NetCore.Tests.Models
{
    [TestClass]
    public class PlayHistoryTests
    {
        [TestMethod]
        public void PlayedAtDateTime_IsoDateTime_ReturnsEquivalentDateTimeOffsetValue()
        {
            // arrange
            var playHistory = new PlayHistory { PlayedAt = "2020-07-14T07:43:21.064Z" };

            // act & assert
            Assert.AreEqual(
                new DateTimeOffset(2020, 7, 14, 7, 43, 21, 64, TimeSpan.FromHours(0)),
                playHistory.PlayedAtDateTime());
        }

        [TestMethod]
        public void MyTestMethod()
        {
            var timestamp = DateTimeOffset.TryParse("2020-06-30T17:23:18.492Z", out var result);
            Debug.WriteLine(result.ToUnixTimeMilliseconds());
        }
    }
}
