using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrlShortenerAPI.Models;
using UrlShortenerAPI.Services;

namespace UrlShortenerAPI.Controllers_v1
{
    [ApiController]
    [Route("/")]
    public class ShortenerController : ControllerBase
    {
        private readonly IShortenerService _shortenerService;

        public ShortenerController(IShortenerService shortenerService)
        {
            _shortenerService = shortenerService;
        }

        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]UrlRequest urlRequest)
        {
            if (urlRequest == null)
                return BadRequest(new { error = "Empty url" });

            UrlResponse response = await _shortenerService.GenerateShortUrl(urlRequest.Url);

            return Created(response?.ShortUrl, response);
        }
       
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            string originalUrl = await _shortenerService.GetOriginalUrl(id);

            if (string.IsNullOrEmpty(originalUrl))
                return NotFound($"Url not found: {id}");

            return Redirect(originalUrl);
        }
    }
}