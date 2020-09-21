using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        //[ResponseCache(Location= ResponseCacheLocation.Any, Duration = 10, VaryByQueryKeys = new String[] { "city" })]
        [ResponseCache(CacheProfileName = "Default30")]
        public IEnumerable<WeatherForecast> Get(string city, int numberOfDays = 5)
        {
            var rng = new Random();
            return Enumerable.Range(1, numberOfDays).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
                City = city
            })
            .ToArray();
        }
    }
}
