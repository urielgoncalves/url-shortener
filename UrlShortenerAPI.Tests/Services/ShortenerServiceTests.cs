using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortenerAPI.Models;
using UrlShortenerAPI.Services;
using UrlShortenerAPI.Tests.Helpers;

namespace UrlShortenerAPI.Tests.Services
{
   [TestClass]
    public class ShortenerServiceTests
    {
        private Mock<IShortenerService> _shortenerService;
        private IConfiguration _configuration;
        private IMemoryCache _cache;
        private IShortenerService _shortenerServiceInstance;

        [TestInitialize]
        public void TestInitialize()
        {
            _shortenerService = new Mock<IShortenerService>();
            _configuration = ConfigurationBuilderTest.Build();
            _cache = new MemoryCache(new MemoryCacheOptions());
            _shortenerServiceInstance = new ShortenerService(_cache);
        }

        [TestMethod]
        public void ShouldGenerateShortUrl()
        {
            //Arrange
            string originalUrl = "www.google.com";
            string shortUrl = "";

            _shortenerService.Setup(m => m.GenerateShortUrl(originalUrl))
                .ReturnsAsync(new UrlResponse(originalUrl, shortUrl));

            // Act
            var result = _shortenerServiceInstance.GenerateShortUrl(originalUrl).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(!string.IsNullOrEmpty(result.ShortUrl));
        }


        [TestMethod]
        public void ShouldValidateIfGeneratedUrlIdAreTheSameForAURL()
        {
            //Arrange
            string originalUrl = "www.google.com";
            string shortUrl = "";

            _shortenerService.Setup(m => m.GenerateShortUrl(originalUrl))
                .ReturnsAsync(new UrlResponse(originalUrl, shortUrl));

            // Act
            UrlResponse firstCall = _shortenerServiceInstance.GenerateShortUrl(originalUrl).Result;
            UrlResponse secondCall = _shortenerServiceInstance.GenerateShortUrl(originalUrl).Result;

            // Assert
            Assert.IsNotNull(firstCall);
            Assert.AreEqual(firstCall.ShortUrl, secondCall.ShortUrl);
        }

        [TestMethod]
        public void ShouldReturnNullIfOriginalUrlIsEmptyGeneratingShortUrl()
        {
            //Arrange
            string originalUrl = "";
            string shortUrl = "";

            _shortenerService.Setup(m => m.GenerateShortUrl(originalUrl))
                .ReturnsAsync(new UrlResponse(originalUrl, shortUrl));

            // Act
            var result = _shortenerServiceInstance.GenerateShortUrl(originalUrl).Result;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ShouldReturnNullIfShortUrlIdIsEmptyGettingOriginalUrl()
        {
            //Arrange
            string shortUrlId = "a90sd8a89s";

            _shortenerService.Setup(m => m.GetOriginalUrl(shortUrlId));

            // Act
            var result = _shortenerServiceInstance.GetOriginalUrl(shortUrlId).Result;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ShouldGetOriginalUrl()
        {
            //Arrange
            string originalUrl = "https://www.google.com";
            string shortUrl = "";

            _shortenerService.Setup(m => m.GenerateShortUrl(originalUrl))
                .ReturnsAsync(new UrlResponse(originalUrl, shortUrl));
            
            // Act
            var resultShortUrl = _shortenerServiceInstance.GenerateShortUrl(originalUrl).Result;

            // Arrange
            string shortUrlId = resultShortUrl.ShortUrl;
            _shortenerService.Setup(m => m.GetOriginalUrl(shortUrlId)).Returns(Task.FromResult(originalUrl));
            
            // Act
            var resultOriginalUrl = _shortenerServiceInstance.GetOriginalUrl(shortUrlId).Result;

            // Assert
            Assert.AreEqual(resultOriginalUrl, originalUrl);
        }

        [TestMethod]
        [Ignore]
        public void ShouldGenerateUniqueIDByOriginalUrl()
        {
            string originalUrl = "www.google.com";
            string expected = "28b83756bd7b0a1d";
            int asciiStart = 31;

            int index = originalUrl.Length - 1;
            long hash = 0;
            while (index >= 0)
            {
                hash = (hash * asciiStart) + originalUrl[index];
                --index;
            }

            var result = hash.ToString("x");

            Assert.AreEqual(expected, result);
        }
    }
}
