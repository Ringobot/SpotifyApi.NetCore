using System;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpotifyApiDotNetCore.Cache;

namespace SpotifyApiDotNetCore.Tests.Cache
{
    [TestClass]
    public class RuntimeMemoryCacheTests
    {
        [TestMethod]
        public void Add_Value_CacheAddCalled()
        {
            // Arrange
            var expiry = DateTime.Now.AddHours(1);

            var mockObjectCache = new Mock<MemoryCache>();
            var cache = new RuntimeMemoryCache(mockObjectCache.Object);

            // Act
            cache.Add("abc123", "def345", expiry);

            // Assert
            mockObjectCache.Verify(c => c.Set("abc123", "def345", expiry));
        }

        [TestMethod]
        public void Get_Key_CacheGetCalled()
        {
            // Arrange
            var mockObjectCache = new Mock<MemoryCache>();
            var cache = new RuntimeMemoryCache(mockObjectCache.Object);

            // Act
            cache.Get("abc123");

            // Assert
            mockObjectCache.Verify(c => c.Get("abc123"));
        }
    }
}
