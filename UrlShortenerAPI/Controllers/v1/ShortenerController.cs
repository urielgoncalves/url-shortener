using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlShortenerAPI.Models;
using UrlShortenerAPI.Services;

namespace UrlShortenerAPI.Controllers_v1
{
    [ApiController]
    [Route("[controller]")]
    public class ShortenerController : ControllerBase
    {
        private readonly IShortenerService _shortenerService;

        public ShortenerController(IShortenerService shortenerService)
        {
            _shortenerService = shortenerService;
        }

        [HttpPost]
        public IActionResult Post([FromBody]UrlRequest originalUrl)
        {
            if (originalUrl == null)
                return BadRequest(new { error = "Empty url" });

            // Generate ID for the short URL
            UrlResponse response = _shortenerService.GenerateShortUrl(originalUrl.Url);

            return Created(response.ShortUrl, response);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_shortenerService.GetGenenatedUrls());
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            string originalUrl = _shortenerService.GetOriginalUrl(id);

            if (string.IsNullOrEmpty(originalUrl))
                return NotFound(id);

            return Redirect(originalUrl);
        }
    }
}