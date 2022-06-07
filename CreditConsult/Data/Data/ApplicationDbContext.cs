namespace CreditConsult.Data.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

using CreditConsult.Data.Models;
using CreditConsult.Data.Common.Interfaces;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Appointment> Appointments { get; init; }

    public DbSet<AppointmentsForDay> AppointmentsForDays { get; init; }

    public DbSet<HourForAppontment> HourForAppontments { get; init; }

    public DbSet<OfferedService> OfferedServices { get; init; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            options.UseSqlServer("Server=.;Database=CreditConsult;Trusted_Connection=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        //builder.Entity<AppointmentsForDay>()
        //    .HasOne(u => u.ApplicationUser)
        //    .WithMany(a => a.DailyAppointments)
        //    .HasForeignKey(d => d.ApplicationUserId);

        base.OnModelCreating(builder);

        ConfigureUserIdentityRelations(builder);

        EntityIndexesConfiguration.Configure(builder);

        var entityTypes = builder.Model.GetEntityTypes().ToList();

        // Set global query filter for not deleted entities only
        var deletableEntityTypes = entityTypes
            .Where(et => et.ClrType != null && typeof(IBaseModel).IsAssignableFrom(et.ClrType));
        foreach (var deletableEntityType in deletableEntityTypes)
        {
            var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
            method.Invoke(null, new object[] { builder });
        }

        // Disable cascade delete
        var foreignKeys = entityTypes
            .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
        foreach (var foreignKey in foreignKeys)
        {
            foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }

    private void ConfigureUserIdentityRelations(ModelBuilder builder)
     => builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

    private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
    typeof(ApplicationDbContext).GetMethod(
        nameof(SetIsDeletedQueryFilter),
        BindingFlags.NonPublic | BindingFlags.Static);

    private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
    where T : class, IBaseModel
    {
        builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
    }
}