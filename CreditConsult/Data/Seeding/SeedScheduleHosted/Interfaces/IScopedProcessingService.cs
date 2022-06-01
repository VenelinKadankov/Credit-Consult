namespace CreditConsult.Data.Seeding.SeedScheduleHosted.Interfaces;

public interface IScopedProcessingService
{
    Task SeedNextMonth(CancellationToken cancellationToken);
}
