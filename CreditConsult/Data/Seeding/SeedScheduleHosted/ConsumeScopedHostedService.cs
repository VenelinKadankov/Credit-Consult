namespace CreditConsult.Data.Seeding.SeedScheduleHosted;

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using CreditConsult.Data.Seeding.SeedScheduleHosted.Interfaces;

public class ConsumeScopedHostedService : BackgroundService
{
    public ConsumeScopedHostedService(IServiceProvider services)
    {
        this.Services = services;
    }

    public IServiceProvider Services { get; }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        await base.StopAsync(stoppingToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await this.SeedNextMonth(stoppingToken);
    }

    private async Task SeedNextMonth(CancellationToken stoppingToken)
    {
        using var scope = this.Services.CreateScope();

        var scopedProcessingService =
            scope.ServiceProvider
                .GetRequiredService<IScopedProcessingService>();

        await scopedProcessingService.SeedNextMonth(stoppingToken);
    }
}
