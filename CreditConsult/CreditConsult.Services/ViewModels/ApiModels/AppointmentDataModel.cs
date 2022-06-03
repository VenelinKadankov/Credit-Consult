namespace CreditConsult.Services.ViewModels.ApiModels;

public class AppointmentDataModel
{
    public string Date { get; init; }

    public IEnumerable<FreeHourModel> Hours { get; init; }
}
