namespace CreditConsult.Data.Seeding;

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using CreditConsult.Data.Data;
using CreditConsult.Data.Models;

public class RolesSeeder : ISeeder
{
    public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

        await SeedRoleAsync(roleManager, "Administrator");
        await SeedRoleAsync(roleManager, "Employee");
        await SeedRoleAsync(roleManager, "Client");
    }

    private static async Task SeedRoleAsync(RoleManager<ApplicationRole> roleManager, string roleName)
    {
        var role = await roleManager.FindByNameAsync(roleName);
        if (role == null)
        {
            var result = await roleManager.CreateAsync(new ApplicationRole(roleName));
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
            }
        }
    }
}
