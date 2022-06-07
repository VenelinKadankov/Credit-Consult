namespace CreditConsult.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using CreditConsult.Data.Data;
    using CreditConsult.Data.Models;
    using CreditConsult.Data.Models.Enums;

    public class EmployeesSeeder : ISeeder
    {
        public async Task SeedAsync(
            ApplicationDbContext dbContext,
            IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any(u => u.IsEmployee))
            {
                return;
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedUserAsync(dbContext, userManager, "Employee");
        }

        private static async Task SeedUserAsync(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, string userRoleName)
        {
            var names = new List<string> { "PeshoIvanov", "GoshoIvanov", "ToshoIvanov", "IvanIvanov", "AnaIvanova", "MariaIvanova", "TaniaIvanova", "PetkanaIvanova" };
            var users = new List<ApplicationUser>();
            var phone = 11111111111;
            var pass = 1;
            var rand = new Random();

            foreach (var item in names)
            {
                var user = new ApplicationUser
                {
                    UserName = $"{item}@abv.bg",
                    Title = UserTitle.Consultant,
                    IsEmployee = true,
                    Email = $"{item}@abv.bg",
                    EmailConfirmed = true,
                    PhoneNumber = $"{phone++}",
                    PhoneNumberConfirmed = true,
                    ModifiedOn = DateTime.UtcNow,
                    Appointments = new HashSet<Appointment>(),
                };

                users.Add(user);

                await userManager.CreateAsync(user, $"123@aA{pass++}");

                var result = await userManager.AddToRoleAsync(user, userRoleName);

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }

            }

           // await dbContext.AddRangeAsync(users);
        }
    }
}
