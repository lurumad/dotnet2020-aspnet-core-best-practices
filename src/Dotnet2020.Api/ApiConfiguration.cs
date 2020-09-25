using Dotnet2020.Api.Features.DependencyInjection;
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
                .AddSingleton<MultitenantService>()
                // Warning ASP0000 Calling 'BuildServiceProvider' from application code results in an additional copy of singleton services 
                // being created. Consider alternatives such as dependency injecting services as parameters to 'Configure'
                .Configure<MultitenancyOptions>(options =>
                {
                    var serviceProvider = services.BuildServiceProvider();
                    var multitenantService = serviceProvider.GetService<MultitenantService>();
                    serviceProvider = services.BuildServiceProvider();
                    multitenantService = serviceProvider.GetService<MultitenantService>();
                    options.TenantIds = multitenantService.GetTenantIds();
                })
                //.AddSingleton<IConfigureOptions<MultitenancyOptions>, ConfigureMultitenancyOptions>()
                .AddHostedService<GoodScopeHostedService>()
                .AddHashids(setup =>
                {
                    setup.Salt = "lzDeEA)xmaEnEF((JzX9xZH6d2Ui5NNEjCtiYa8*KD6FyVB@Eckvrw5x2gf+";
                    setup.MinHashLength = 8;
                })
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
