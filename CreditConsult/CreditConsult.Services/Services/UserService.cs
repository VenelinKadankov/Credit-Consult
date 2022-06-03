namespace CreditConsult.Services.Services;

using CreditConsult.Data.Data;
using CreditConsult.Services.Interfaces;
using CreditConsult.Services.ViewModels;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<UserViewModel> GetEmployees()
    {
        var roleId = _context.Roles
            .FirstOrDefault(r => r.Name == "Employee").Id;

        var adminRoleId = _context.Roles
            .FirstOrDefault(r => r.Name == "Administrator").Id;

        return _context.Users
            .Where(u => u.IsEmployee)
            .Select(u => new UserViewModel
            {
                Id = u.Id,
                Name = u.UserName,
                Appointments = u.Appointments
                .Select(a => new AppointmentViewModel
                {
                    Date = a.Date.ToString(),
                    EmployeeId = a.EmployeeId,
                }),
                IsAdministrator = u.Roles.Any(r => r.RoleId == adminRoleId),
            });
    }

}
