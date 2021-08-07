using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public Task<UrlResponse> GenerateShortUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return Task.FromResult<UrlResponse>(null);

            string shortUrlId = GenerateUniqueID();
            _cache.Set(shortUrlId, HandleUrlProtocol(url));

            return Task.FromResult(new UrlResponse(url, shortUrlId));
        }

        public Task<string> GetOriginalUrl(string id)
        {
            if (!_cache.TryGetValue(id, out string originalUrl))
                return Task.FromResult<string>(null);

            return Task.FromResult(originalUrl);
        }

        private string GenerateUniqueID()
        {
            //TODO: generate algotithm
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
