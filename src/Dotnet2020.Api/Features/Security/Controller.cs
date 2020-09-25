using Dotnet2020.Api.Features.EFCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Dotnet2020.Api.Features.Security
{
    [Route("api/security/customers")]
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
        public async Task<ActionResult<CustomerDto>> GetBy(int id)
        {
            var customer = await CompiledQueries.CustomerBy(dbContext, id);
            
            return Ok(new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name
            });
        }
    }
}
