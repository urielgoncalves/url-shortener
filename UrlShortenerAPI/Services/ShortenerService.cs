using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using UrlShortenerAPI.Models;

namespace UrlShortenerAPI.Services
{
    public class ShortenerService : IShortenerService
    {
        private const string BASE_SHORT_URL_KEY = "BaseShortUrl";
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;

        public ShortenerService(IConfiguration configuration, IMemoryCache cache)
        {
            _configuration = configuration; 
            _cache = cache;
        }

        public UrlResponse GenerateShortUrl(string url)
        {
            string shortUrlId = GenerateUniqueID();
            _cache.Set(shortUrlId, HandleUrlProtocol(url));

            return new UrlResponse(url, BuildShortUrl(shortUrlId));
        }

        public IEnumerable<string> GetGenenatedUrls()
        {
            throw new NotImplementedException();
        }

        public string GetOriginalUrl(string id)
        {
            if (!_cache.TryGetValue(id, out string originalUrl))
                return null;

            return originalUrl;
        }

        private string GenerateUniqueID()
        {
            var ticks = new DateTime(2021, 1, 1).Ticks;
            var ans = DateTime.Now.Ticks - ticks;
            var uniqueId = ans.ToString("x");

            return uniqueId;
        }

        private static string HandleUrlProtocol(string url)
        {
            if (!url.StartsWith("http"))
                return $"https://{url}";

            return url;
        }

        private string BuildShortUrl(string shortUrlId) =>
            string.Format(
                _configuration.GetValue<string>(BASE_SHORT_URL_KEY).ToString()
                , shortUrlId);
    }
}
