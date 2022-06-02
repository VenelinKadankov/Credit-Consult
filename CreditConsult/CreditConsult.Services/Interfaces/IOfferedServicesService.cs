namespace CreditConsult.Services.Interfaces;

using CreditConsult.Services.ViewModels;

public interface IOfferedServicesService
{
    Task Add(
       string title,
       string description,
       decimal price,
       string imageUrl);

    Task AddEmployeeToService(string employeeId, int serviceId);

    int AllServicess();

    Task<bool> Remove(int id);

    ServiceViewModel GetService(int id);

    ServiceViewModel GetService(string name);

    IEnumerable<ServiceViewModel> GetAllTest();
}
