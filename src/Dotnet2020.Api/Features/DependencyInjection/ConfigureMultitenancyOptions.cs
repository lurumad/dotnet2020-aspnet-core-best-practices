using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Dotnet2020.Api.Features.DependencyInjection
{
    public class ConfigureMultitenancyOptions : IConfigureOptions<MultitenancyOptions>
    {
        private readonly IServiceProvider serviceProvider;

        public ConfigureMultitenancyOptions(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public void Configure(MultitenancyOptions options)
        {
            var multitenantService = serviceProvider.GetService<MultitenantService>();
            options.TenantIds = multitenantService.GetTenantIds();
        }
    }
}
