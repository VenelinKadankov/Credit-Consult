namespace CreditConsult.Services.ViewModels.ApiModels;

public class EmployeeDataModel
{
    public string Name { get; init; }

    public IEnumerable<AppointmentDataModel> Dates { get; init; }
}
