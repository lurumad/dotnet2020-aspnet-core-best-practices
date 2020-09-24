using Microsoft.EntityFrameworkCore;

namespace Dotnet2020.Api.Features.EFCore
{
    public class ApiDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
    }
}
