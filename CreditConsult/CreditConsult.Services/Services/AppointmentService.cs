namespace CreditConsult.Services.Services;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CreditConsult.Data.Data;
using CreditConsult.Data.Models;
using CreditConsult.Services.InputModels;
using CreditConsult.Services.Interfaces;
using CreditConsult.Services.ViewModels;
using CreditConsult.Services.ViewModels.ApiModels;
using Microsoft.EntityFrameworkCore;

public class AppointmentService : IAppointmentService
{
    private readonly ApplicationDbContext _context;

    public AppointmentService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<string> AllDates(string name)
    {
        var employee = _context.Users.FirstOrDefault(u => u.UserName == name && u.IsEmployee);

        if (employee == null)
        {
            return null;
        }

        employee.DailyAppointments = _context.AppointmentsForDays
            .Where(d => d.ApplicationUserId == employee.Id)
            .ToList();

        char[] delimiterChars = { '/', ' ' };

        var listOfDates = employee.DailyAppointments
            .Select(u => u.Date)
            .Where(d => (int.Parse(d.Split('/')[0]) > DateTime.UtcNow.Day &&
                int.Parse(d.Split(delimiterChars)[1]) == DateTime.UtcNow.Month) ||
                (int.Parse(d.Split(delimiterChars)[1]) > DateTime.UtcNow.Month))
            .OrderBy(d => int.Parse(d.Split(delimiterChars)[1]))
            .ThenBy(d => int.Parse(d.Split(delimiterChars)[0]));

        return listOfDates;
    }

    public IEnumerable<FreeHourModel> AllFreeHours(string employeeName, string date)
    {
        var employee = _context.Users
            .Where(d => d.UserName == employeeName)
            .FirstOrDefault(d => d.IsEmployee);

        var dailyAppointments = _context.AppointmentsForDays.Where(x => x.ApplicationUserId == employee.Id);

        var appointmentsForDay = _context.AppointmentsForDays.FirstOrDefault(a => a.ApplicationUserId == employee.Id && a.Date.StartsWith(date));

        if (appointmentsForDay == null)
        {
            return null;
        }

        var appointmentsForDayId = appointmentsForDay.Id;

        var employeeAppointmentsHours = _context.HourForAppontments
            .Where(h => h.DailyAppointmentsId == appointmentsForDayId);

        var hours = employeeAppointmentsHours
            .Where(x => !x.IsDeleted)
            .Select(h => h.Time)
            .OrderBy(h => h)
            .Select(h => new FreeHourModel
            {
                Time = h,
                Id = appointmentsForDayId,
            })
            .ToList();

        return hours;
    }

    public async Task<bool> CreateAppointment(string employeeToBook, string date, string time, string message, string clientName, string clientEmail, string clientPhone, string serviceName, string currentUserName, string currentUserId)
    {
        var employee = _context.Users
               .FirstOrDefault(x => x.UserName == employeeToBook);

        if (employee == null)
        {
            return false;
        }

        var patient = _context.Users
            .FirstOrDefault(x => x.UserName == clientName && x.Email == clientEmail);

        var user = _context.Users
            .FirstOrDefault(u => u.Id == currentUserId);

        if (patient == null ||
            user == null ||
            user != patient)
        {
            return false;
        }

        var dailyApps = _context.AppointmentsForDays
            .FirstOrDefault(x => x.ApplicationUserId == employee.Id && x.Date.StartsWith(date));

        if (dailyApps == null)
        {
            return false;
        }

        var dailyAppsId = dailyApps.Id;

        _context.HourForAppontments
            .FirstOrDefault(h => h.Id == dailyAppsId && h.Time == time).IsDeleted = true;

        await _context.SaveChangesAsync();

        var appointment = new Appointment
        {
            EmployeeId = employee.Id,
            Date = date,
            ClientEmail = clientEmail,
            ClientName = clientName,
            ClientPhone = clientPhone,
            Time = time,
            Message = message,
            ServiceName = serviceName,
        };

        await _context.AddAsync(appointment);

        await _context.SaveChangesAsync();

        return true;
    }

    public AppointmentInputModel CreateEmptyAppointment()
        => new AppointmentInputModel();

    public AppointmentViewModel GetAppointment(int id)
    {
        var appointment = _context.Appointments
               .FirstOrDefault(a => a.Id == id);

        if (appointment == null)
        {
            return null;
        }

        var client = _context.Users
            .FirstOrDefault(p => p.UserName == appointment.ClientName);

        if (client == null)
        {
            return null;
        }

        return new AppointmentViewModel
        {
            Id = id,
            EmployeeName = appointment.Employee.UserName,
            ClientName = appointment.ClientName,
            EmployeeId = appointment.EmployeeId,
            ClientId = client.Id,
            Date = appointment.Date,
            Time = appointment.Time,
            ClientEmail=client.Email,
            ClientPhone = client.PhoneNumber,
            Message = appointment.Message,
            ServiceName = appointment.ServiceName
        };
    }

    public IEnumerable<AppointmentViewModel> MyAppointments(string userName)
    {
        var allAppsOfUser = _context.Appointments
            .Where(a => a.ClientName == userName);

        var userAppoitments = _context.Appointments
            .Where(a => a.ClientName == userName)
            .Select(a => new AppointmentViewModel
            {
                Id = a.Id,
                ClientName = a.ClientName,
                EmployeeName = a.Employee.UserName,
                Date = a.Date + "/" + DateTime.UtcNow.Year,
                Time = a.Time,
                ClientPhone = a.ClientPhone,
                ServiceName = a.ServiceName,
                ClientEmail = a.ClientEmail,
                ClientId = a.ClientId,
                EmployeeId = a.EmployeeId,
                Message = a.Message
            })
            .ToList();

        if (userAppoitments == null)
        {
            return null;
        }

        return userAppoitments;
    }

    public async Task<bool> RemoveAppointment(int id)
    {
        var appointment = _context.Appointments
    .FirstOrDefault(a => a.Id == id);

        if (appointment == null)
        {
            return false;
        }

        var time = appointment.Time;
        var date = appointment.Date;
        var employeeId = appointment.EmployeeId;

        var dailyAppsAll = _context.AppointmentsForDays
            .Where(a => a.ApplicationUserId == employeeId);

        var dailyApps = dailyAppsAll
            .FirstOrDefault(d => d.Date.StartsWith(date));

        if (dailyApps == null)
        {
            return false;
        }

        _context.HourForAppontments.IgnoreQueryFilters()
            .FirstOrDefault(h => h.Id == dailyApps.Id && h.Time == time)
            .IsDeleted = false;

        await _context.SaveChangesAsync();

        appointment.IsDeleted = true;

        await _context.SaveChangesAsync();

        return true;
    }
}
