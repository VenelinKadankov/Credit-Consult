namespace CreditConsult.Data.Seeding;

using CreditConsult.Data.Data;
using CreditConsult.Data.Models;

public class OfferedServicesSeeder : ISeeder
{
    public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        if (dbContext.OfferedServices.Any())
        {
            return;
        }

        await dbContext.OfferedServices.AddAsync(
            new OfferedService
            {
                Title = "Housing Loan",
                Fee = 150,
                ImageUrl = "https://img.iproperty.com.my/angel-legacy/1110x624-crop/static/2019/07/Property_Finance_Condo_Loan_sts_1372683893.jpg",
                Description = "Our specialst will help you choose the best option for your new home.",
            });
        await dbContext.OfferedServices.AddAsync(
            new OfferedService
            {
                Title = "Personal expenses Loan",
                Fee = 50,
                ImageUrl = "https://www.idfcfirstbank.com/content/dam/idfcfirstbank/images/blog/benefits-personal-loan.jpg",
                Description = "Our specialst will be more tha happy to check if you qualify for that kind of credit.",
            });
        await dbContext.OfferedServices.AddAsync(
            new OfferedService
            {
                Title = "Mortage",
                Fee = 100,
                ImageUrl = "https://www.badcredit.org/wp-content/uploads/mortgage.png",
                Description = "Our specialst will calculate the best mortgage you can get for your house.",
            });
        await dbContext.OfferedServices.AddAsync(
            new OfferedService
            {
                Title = "Loan new car",
                Fee = 75,
                ImageUrl = "https://www.debt.org/wp-content/uploads/2012/04/Auto-Loans.gif",
                Description = "You dream for a new car. Our consultants will make your dreams come true!",
            });

        await dbContext.SaveChangesAsync();
    }
}

