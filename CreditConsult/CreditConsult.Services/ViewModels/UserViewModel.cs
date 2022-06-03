namespace CreditConsult.Services.ViewModels;

public class UserViewModel
{
    public string Id { get; init; }

    public string Name { get; set; }

    public IEnumerable<AppointmentViewModel> Appointments { get; set; }

    public bool IsAdministrator { get; set; }
}
