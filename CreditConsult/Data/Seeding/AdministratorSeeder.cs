namespace CreditConsult.Data.Seeding;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using CreditConsult.Data.Data;
using CreditConsult.Data.Models;

public class AdministratorSeeder : ISeeder
{
    public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        if (dbContext.UserRoles.Any())
        {
            return;
        }

        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        await SeedUserAsync(userManager, "Administrator");
    }

    private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, string userRoleName)
    {
        var user = new ApplicationUser
        {
            UserName = "admin@creditConsult.bg",
            Email = "admin@creditConsult.bg",
            EmailConfirmed = true,
            IsEmployee = false,
            PhoneNumber = "123456789",
            PhoneNumberConfirmed = true,
        };

        await userManager.CreateAsync(user, "Pass@123");
        var result = await userManager.AddToRoleAsync(user, userRoleName);

        if (!result.Succeeded)
        {
            throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
        }
    }
}
