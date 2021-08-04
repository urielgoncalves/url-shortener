using System.Collections.Generic;
using System.Threading.Tasks;
using UrlShortenerAPI.Models;

namespace UrlShortenerAPI.Services
{
    public interface IShortenerService
    {
        // Generate short url
        UrlResponse GenerateShortUrl(string url);

        // Get url's generated
        IEnumerable<string> GetGenenatedUrls();

        // Get single url 
        string GetOriginalUrl(string id);
    }
}