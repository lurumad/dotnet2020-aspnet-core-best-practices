using System;
using System.Linq;
using System.Threading.Tasks;
using Cache.Api.Filters;
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

        private static readonly string[] Cities = new[]
{
            "Madrid", "Rome", "Paris", "London", "Washington", "Lisbon", "Berlin", "Amsterdam", "Dublin", "Moscow"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 5)]
        public async Task<ActionResult> GetAll()
        {
            var rng = new Random();
            var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(5),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
                City = Cities[rng.Next(Cities.Length)]
            })
            .ToArray();
            return Ok(forecasts);
        }

        [HttpGet]
        [Route("{city}")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 30, VaryByQueryKeys = new String[] { "city" })]
        public async Task<ActionResult> GetForCity(string city)
        {
            var rng = new Random();
            var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(5),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
                City = city
            })
            .ToArray();
            return Ok(forecasts);
        }

        [HttpGet]
        [Route("~/private")]
        [ResponseCache(CacheProfileName = "PrivateShort")]
        public async Task<ActionResult> GetAllPrivate()
        {
            var rng = new Random();
            var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(5),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
                City = Cities[rng.Next(Cities.Length)]
            })
            .ToArray();
            return Ok(forecasts);
        }

        [HttpGet]
        [Route("~/etag")]
        [ETagFilter]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 20)]
        public async Task<ActionResult> GetWithEtag()
        {
            var rng = new Random();
            var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(5),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)],
                City = Cities[rng.Next(Cities.Length)]
            })
            .ToArray();
            return Ok(forecasts);
        }


    }
}
