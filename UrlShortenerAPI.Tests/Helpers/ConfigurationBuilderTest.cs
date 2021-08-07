using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace UrlShortenerAPI.Tests.Helpers
{
    public static class ConfigurationBuilderTest
    {
        public static IConfigurationRoot Build()
        {
            var myConfiguration = new Dictionary<string, string>
            {
                {"BaseShortUrl", "https://localhost:5001/shortener/{0}"}
            };

            return new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();
        }
    }
}
