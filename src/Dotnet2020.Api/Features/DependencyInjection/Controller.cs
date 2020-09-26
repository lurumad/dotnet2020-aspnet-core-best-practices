using Dotnet2020.Api.Features.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

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

        [HttpGet("badfireandforget")]
        public IActionResult BadFireAndForget([FromServices] ApiDbContext dbContext)
        {
            _ = Task.Run(async () =>
            {
                await Task.Delay(2000);

                var customer = await dbContext.Customers.SingleOrDefaultAsync(c => c.Id == 1);
                customer.Name = "Name modified";

                await dbContext.SaveChangesAsync();
            });

            return Ok();
        }

        [HttpGet("goodfireandforget")]
        public IActionResult BadFireAndForget([FromServices] IServiceScopeFactory serviceScopeFactory)
        {
            _ = Task.Run(async () =>
            {
                await Task.Delay(2000);

                using var scope = serviceScopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetService<ApiDbContext>();
                var customer = await dbContext.Customers.SingleOrDefaultAsync(c => c.Id == 1);
                customer.Name = "Name modified";

                await dbContext.SaveChangesAsync();
            });

            return Ok();
        }
    }
}
