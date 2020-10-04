using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cache.Api.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace Cache.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private static readonly string[] Cities = new[]
{
            "Madrid", "Rome", "Paris", "London", "Washington", "Lisbon", "Berlin", "Amsterdam", "Dublin", "Moscow"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        //private readonly IEntityTaggerGenerator _entityTagger;


        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        //[ETagFilter]
        [ETagFilter]
        //[ResponseCache(Location = ResponseCacheLocation.Any, Duration = 20)] //, VaryByQueryKeys = new String[] { "city" })] // 
        [ResponseCache(CacheProfileName = "Default30")]
        public async Task<ActionResult> Get(string city, int numberOfDays = 5)
        {
            var rng = new Random();
            var forecasts = Enumerable.Range(1, numberOfDays).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = 5, //rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
                City = "Madrid", //Cities[rng.Next(Cities.Length)]
            })
            .ToArray();

            //HttpContext.Response.GetTypedHeaders().ETag = new EntityTagHeaderValue("\"E1\"");
            return Ok(forecasts);
        }


    }
}
