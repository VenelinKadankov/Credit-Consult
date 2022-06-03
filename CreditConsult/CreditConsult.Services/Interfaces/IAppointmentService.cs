namespace CreditConsult.Services.Interfaces;

using CreditConsult.Services.InputModels;
using CreditConsult.Services.ViewModels;
using CreditConsult.Services.ViewModels.ApiModels;

public interface IAppointmentService
{
    IEnumerable<string> AllDates(string name);

    IEnumerable<FreeHourModel> AllFreeHours(
        string employeeName,
        string date);

    Task<bool> CreateAppointment(
        string employee,
        string date,
        string time,
        string message,
        string clientName,
        string clientEmail,
        string clientPhone,
        string serviceName,
        string currentUserName,
        string currentUserId);

    Task<bool> RemoveAppointment(int id);

    AppointmentViewModel GetAppointment(int id);

    AppointmentInputModel CreateEmptyAppointment();

    IEnumerable<AppointmentViewModel> MyAppointments(string userName);
}
