using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpotifyApi.NetCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

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

            // act
            var playedAt = playHistory.PlayedAtDateTime;

            // assert
            Assert.AreEqual(new DateTimeOffset(2020, 7, 14, 7, 43, 21, 64, TimeSpan.FromHours(0)), playedAt);
        }
    }
}
