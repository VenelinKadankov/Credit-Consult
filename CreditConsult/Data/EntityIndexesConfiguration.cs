namespace CreditConsult.Data;

using CreditConsult.Data.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

internal static class EntityIndexesConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        // IDeletableEntity.IsDeleted index
        var deletableEntityTypes = modelBuilder.Model
            .GetEntityTypes()
            .Where(et => et.ClrType != null && typeof(IBaseModel).IsAssignableFrom(et.ClrType));
        foreach (var deletableEntityType in deletableEntityTypes)
        {
            modelBuilder.Entity(deletableEntityType.ClrType).HasIndex(nameof(IBaseModel.IsDeleted));
        }
    }
}