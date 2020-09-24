using Dotnet2020.Api.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dotnet2020.Api.Features.HttpContext
{
    [Route("api/httpcontext")]
    [ApiController]
    public class HttpContextController : ControllerBase
    {
        private readonly ILogger<HttpContextController> logger;
        private readonly IHttpClientFactory httpClientFactory;

        public HttpContextController(
            ILogger<HttpContextController> logger,
            IHttpClientFactory httpClientFactory)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        [Route("badsearch")]
        public async Task<IActionResult> BadSearch(string query)
        {
            var google = BadDoSearch($"https://www.google.com/search?q={query}");
            var bing = BadDoSearch($"https://www.bing.com/search?q={query}");
            var yandex = BadDoSearch($"https://yandex.com/search/?text={query}");

            await Task.WhenAll(google, bing, yandex);

            return Ok();
        }

        private async Task<string> BadDoSearch(string query)
        {
            try
            {
                logger.StartingSearchLogger(HttpContext.Request.QueryString.Value);

                var client = httpClientFactory.CreateClient();

                var response = await client.GetAsync(query);

                logger.FinishingSearchLogger(HttpContext.Request.QueryString.Value);

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception exception)
            {
                logger.ErrorSearchLogger(HttpContext.Request.QueryString.Value, exception);
            }

            return string.Empty;
        }

        [Route("search")]
        public async Task<IActionResult> Search(string query)
        {
            var path = HttpContext.Request.QueryString.Value;
            var google = DoSearch($"https://www.google.com/search?q={query}", path);
            var bing = DoSearch($"https://www.bing.com/search?q={query}", path);
            var yandex = DoSearch($"https://yandex.com/search/?text={query}", path);

            await Task.WhenAll(google, bing, yandex);

            return Ok();
        }

        private async Task<string> DoSearch(string query, string path)
        {
            try
            {
                logger.StartingSearchLogger(path);

                var client = httpClientFactory.CreateClient();

                var response = await client.GetAsync(query);

                logger.FinishingSearchLogger(path);

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception exception)
            {
                logger.ErrorSearchLogger(path, exception);
            }

            return string.Empty;
        }
    }
}
