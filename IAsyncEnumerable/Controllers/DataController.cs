using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IAsyncEnumerable.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private readonly ILogger<DataController> _logger;

        public DataController(ILogger<DataController> logger)
        {
            _logger = logger;
        }

        [HttpGet]

        public IActionResult Get()
        {
            return Ok(GetData());
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {

                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        static async Task<IEnumerable<int>> GetData()
        {
            var datas = new List<int>();
            for (int i = 1; i <= 10; i++)
            {
                await Task.Delay(500);
                datas.Add(i);
            }

            return datas;
        }

        static async IAsyncEnumerable<int> GetDataAsyncEnum()
        {
            for (int i = 1; i <= 10; i++)
            {
                await Task.Delay(500);
                yield return i;
            }
        }
    }
}
