namespace CreditConsult.Data.Seeding;

using System;
using System.Threading.Tasks;

using CreditConsult.Data.Data;

public interface ISeeder
{
    Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider);
}
