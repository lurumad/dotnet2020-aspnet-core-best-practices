using Dotnet2020.Api.Features.EFCore;
using System.Linq;
using System.Threading.Tasks;

namespace DotNet2020.Host.Infrastructure.Seeders
{
    public static class CustomersSeeder
    {
        public static async Task Run(ApiDbContext dbContext)
        {
            if (!dbContext.Customers.Any())
            {
                dbContext.Customers.AddRange(new Customer[]
                {
                    new Customer
                    {
                        Name = "John Doe"
                    },
                    new Customer
                    {
                        Name = "Mary Jean"
                    }
                });

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
