using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Dotnet2020.Api.Features.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Dotnet2020.Api.Features.DependencyInjection
{
    public class GoodScopeHostedService : BackgroundService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;

        public GoodScopeHostedService(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await BackgroundProcessing(stoppingToken);
        }

        private async Task BackgroundProcessing(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using var scope = serviceScopeFactory.CreateScope();

                var dbContext = scope.ServiceProvider.GetService<ApiDbContext>();

                await dbContext.Customers.ToListAsync();

                await Task.Delay(5000);
            }
        }
    }
}
