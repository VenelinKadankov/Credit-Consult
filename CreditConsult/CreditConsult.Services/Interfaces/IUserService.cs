namespace CreditConsult.Services.Interfaces;

using CreditConsult.Services.ViewModels;

public interface IUserService
{
    IEnumerable<UserViewModel> GetEmployees();
}
