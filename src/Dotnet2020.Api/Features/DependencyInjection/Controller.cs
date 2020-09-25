using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Dotnet2020.Api.Features.DependencyInjection
{
    [Route("api/options")]
    [ApiController]
    public class Controller : ControllerBase
    {
        private readonly MultitenancyOptions options;

        public Controller(IOptions<MultitenancyOptions> options)
        {
            this.options = options?.Value ?? throw new System.ArgumentNullException(nameof(options));
        }

        [HttpGet]
        [Route("")]
        public ActionResult Get()
        {
            return Ok(options.TenantIds);
        }
    }
}
