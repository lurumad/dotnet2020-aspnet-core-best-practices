using Dotnet2020.Api.Features.EFCore;
using Dotnet2020.Api.Features.HttpContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet2020.Api
{
    public static class ApiConfiguration
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services
                .AddHttpClient()
                .AddHttpContextAccessor()
                .AddScoped<BadAuthorizationService>();

            return services;
        }

        public static void Configure(IApplicationBuilder app)
        {
            
        }
    }
}
