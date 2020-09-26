using Dotnet2020.Api;
using Dotnet2020.Api.Features.EFCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNet2020.Host
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ApiConfiguration.ConfigureServices(services)
                .AddDbContextPool<ApiDbContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("SqlServer"), sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(Startup).Assembly.GetName().Name);
                    });
                })
                .AddControllers();
        }

        public void Configure(IApplicationBuilder app)
        {
            ApiConfiguration.Configure(app, _ => _, _ => _, _ => _);
        }
    }
}
