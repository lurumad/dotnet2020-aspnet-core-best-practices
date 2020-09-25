using Dotnet2020.Api.Features.EFCore;
using DotNet2020.Host.Infrastructure.Seeders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace DotNet2020.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .MigrateDbContext<ApiDbContext>(dbContext => CustomersSeeder.Run(dbContext).Wait())
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
