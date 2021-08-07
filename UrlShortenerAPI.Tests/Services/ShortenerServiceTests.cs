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

        [TestInitialize]
        public void TestInitialize()
        {
            _shortenerService = new Mock<IShortenerService>();
            _configuration = ConfigurationBuilderTest.Build();
            _cache = new MemoryCache(new MemoryCacheOptions());
        }
        
        [TestMethod]
        public void ShouldGenerateShortUrl()
        {
            //Arrange
            string originalUrl = "www.google.com";
            string shortUrl = "";

            _shortenerService.Setup(m => m.GenerateShortUrl(originalUrl))
                .ReturnsAsync(new UrlResponse(originalUrl, shortUrl));

            var serviceCall = new ShortenerService(_configuration, _cache);

            // Act
            var result = serviceCall.GenerateShortUrl(originalUrl).Result;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(!string.IsNullOrEmpty(result.ShortUrl));
        }

        [TestMethod]
        public void ShouldReturnNullIfOriginalUrlIsEmptyGeneratingShortUrl()
        {
            //Arrange
            string originalUrl = "";
            string shortUrl = "";

            _shortenerService.Setup(m => m.GenerateShortUrl(originalUrl))
                .ReturnsAsync(new UrlResponse(originalUrl, shortUrl));

            var serviceCall = new ShortenerService(_configuration, _cache);

            // Act
            var result = serviceCall.GenerateShortUrl(originalUrl).Result;

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ShouldReturnNullIfShortUrlIdIsEmptyGettingOriginalUrl()
        {
            //Arrange
            string shortUrlId = "a90sd8a89s";

            _shortenerService.Setup(m => m.GetOriginalUrl(shortUrlId));

            var serviceCall = new ShortenerService(_configuration, _cache);

            // Act
            var result = serviceCall.GetOriginalUrl(shortUrlId).Result;

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
            
            var serviceCall = new ShortenerService(_configuration, _cache);
            
            // Act
            var resultShortUrl = serviceCall.GenerateShortUrl(originalUrl).Result;

            
            // Arrange
            string shortUrlId = resultShortUrl.ShortUrl;
            _shortenerService.Setup(m => m.GetOriginalUrl(shortUrlId)).Returns(Task.FromResult(originalUrl));
            
            // Act
            var resultOriginalUrl = serviceCall.GetOriginalUrl(shortUrlId).Result;

            // Assert
            Assert.AreEqual(resultOriginalUrl, originalUrl);
        }
    }
}
