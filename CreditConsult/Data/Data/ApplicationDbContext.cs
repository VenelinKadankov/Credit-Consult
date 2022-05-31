namespace CreditConsult.Data.Data;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CreditConsult.Data.Models;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public ApplicationDbContext() { }

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
        //builder.Entity<ApplicationUser>()
        //    .HasMany(u => u.OfferedServices)
        //    .WithMany(s => s.Employees);

        base.OnModelCreating(builder);

        ConfigureUserIdentityRelations(builder);
    }

    private void ConfigureUserIdentityRelations(ModelBuilder builder)
     => builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
}