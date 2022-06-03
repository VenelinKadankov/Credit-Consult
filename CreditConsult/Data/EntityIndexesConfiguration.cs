namespace CreditConsult.Data;

using Microsoft.EntityFrameworkCore;

using CreditConsult.Data.Common.Interfaces;

internal static class EntityIndexesConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        var deletableEntityTypes = modelBuilder.Model
            .GetEntityTypes()
            .Where(et => et.ClrType != null && typeof(IBaseModel).IsAssignableFrom(et.ClrType));
        foreach (var deletableEntityType in deletableEntityTypes)
        {
            modelBuilder.Entity(deletableEntityType.ClrType).HasIndex(nameof(IBaseModel.IsDeleted));
        }
    }
}