using Newtonsoft.Json;

namespace UrlShortenerAPI.Models
{
    public class UrlResponse
    {
        public UrlResponse(string original, string shortened)
        {
            this.Original = original;
            this.ShortUrl = shortened;
        }

        [JsonProperty("original")]
        public string Original { get; }

        [JsonProperty("short_url")]
        public string ShortUrl { get; }
    }
}