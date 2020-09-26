using Dotnet2020.Api.Features.DependencyInjection;
using Dotnet2020.Api.Features.HttpContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;

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
                .AddHttpClient()
                .AddHttpContextAccessor()
                .AddScoped<BadAuthorizationService>();

            return services;
        }

        public static IApplicationBuilder Configure(
            IApplicationBuilder app,
            Func<IApplicationBuilder, IApplicationBuilder> preRouting,
            Func<IApplicationBuilder, IApplicationBuilder> postRouting,
            Func<IEndpointRouteBuilder, IEndpointRouteBuilder> configureEndpoints)
        {
            preRouting(app);
            app.UseRouting();
            postRouting(app);
            app.UseEndpoints(endpoints =>
            {
                configureEndpoints(endpoints);
                endpoints.MapDefaultControllerRoute();
            });

            return app;
        }
    }
}
