using System.Collections.Generic;
using System.Threading.Tasks;
using UrlShortenerAPI.Models;

namespace UrlShortenerAPI.Services
{
    public interface IShortenerService
    {
        Task<UrlResponse> GenerateShortUrl(string url);

        Task<string> GetOriginalUrl(string id);
    }
}