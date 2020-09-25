using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dotnet2020.Api.Features.EFCore
{
    [Route("api/efcore/customers")]
    [ApiController]
    public class Controller : ControllerBase
    {
        private readonly ApiDbContext dbContext;

        public Controller(ApiDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new System.ArgumentNullException(nameof(dbContext));
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Customer>> GetBy(int id)
        {
            return Ok(await CompiledQueries.CustomerBy(dbContext, id));
        }
    }
}
