namespace CreditConsult.Services.ViewModels.ApiModels;

public class ConsultDataModel
{
    public IEnumerable<EmployeeDataModel> Employees { get; init; }

    public IEnumerable<ServiceDataModel> Services { get; init; }
}
