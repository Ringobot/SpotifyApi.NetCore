using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpotifyApi.NetCore.Helpers;
using SpotifyApi.NetCore.Models;

namespace SpotifyApi.NetCore.Tests.Helpers
{
    [TestClass]
    public class TimeRangeHelperTests
    {
        [TestMethod]
        public void TimeRangeString_ShortTerm_AreEqual()
        {
            // assert
            Assert.AreEqual("short_term", TimeRangeHelper.TimeRangeString(TimeRange.ShortTerm));
        }

        [TestMethod]
        public void TimeRangeString_MediumTerm_AreEqual()
        {
            // assert
            Assert.AreEqual("medium_term", TimeRangeHelper.TimeRangeString(TimeRange.MediumTerm));
        }

        [TestMethod]
        public void TimeRangeString_LongTerm_AreEqual()
        {
            // assert
            Assert.AreEqual("long_term", TimeRangeHelper.TimeRangeString(TimeRange.LongTerm));
        }
    }
}
