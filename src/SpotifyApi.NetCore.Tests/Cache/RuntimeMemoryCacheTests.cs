using System;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpotifyApi.NetCore.Cache;

namespace SpotifyApi.NetCore.Tests.Cache
{
    [TestClass]
    public class RuntimeMemoryCacheTests
    {
        [TestMethod]
        public void Add_Value_CacheAddCalled()
        {
            // https://github.com/aspnet/Caching/blob/dev/src/Microsoft.Extensions.Caching.Abstractions/MemoryCacheExtensions.cs#L49

            // Arrange
            var expiry = DateTime.Now.AddHours(1);

            var mockObjectCache = new Mock<IMemoryCache>();
            var mockCacheEntry = new Mock<ICacheEntry>();
            mockObjectCache.Setup(c=>c.CreateEntry(It.IsAny<object>())).Returns(()=> mockCacheEntry.Object);
            var cache = new RuntimeMemoryCache(mockObjectCache.Object);

            // Act
            cache.Add("abc123", "def345", expiry);

            // Assert
            mockObjectCache.Verify(c => c.CreateEntry(It.IsAny<object>()));
        }

        [TestMethod]
        public void Get_Key_CacheGetCalled()
        {
            // Arrange
            var mockObjectCache = new Mock<IMemoryCache>();
            var cache = new RuntimeMemoryCache(mockObjectCache.Object);

            // Act
            cache.Get("abc123");

            // Assert
            object output = null;
            mockObjectCache.Verify(c => c.TryGetValue(It.IsAny<object>(), out output));
        }
    }
}
