namespace CreditConsult.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using CreditConsult.Services.Interfaces;
using CreditConsult.Services.Services;
using CreditConsult.Services.InputModels;
using CreditConsult.Web.Common.Extensions;

public class AppointmentsController : Controller
{
    private readonly IAppointmentService _appointmentsService;
    private readonly IOfferedServicesService _offeredServicesService;

    public AppointmentsController(
        IAppointmentService appointmentsService,
        IOfferedServicesService offeredServicesService)
    {
        _appointmentsService = appointmentsService;
        _offeredServicesService = offeredServicesService;
    }

    [Authorize]
    public IActionResult Index()
    {
        var appointments = _appointmentsService.CreateEmptyAppointment();

        return this.View(appointments);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Index(AppointmentInputModel appointment)
    {
        if (!this.ModelState.IsValid)
        {
            return this.View(appointment);
        }

        var isCreated = await _appointmentsService.CreateAppointment(
            appointment.Employee,
            appointment.Date,
            appointment.Time,
            appointment.ClientName,
            appointment.Message,
            appointment.ClientEmail,
            appointment.ClientPhone,
            appointment.ServiceName,
            this.User.Identity.Name,
            this.User.GetId());

        if (!isCreated)
        {
            this.TempData["InvalidData"] = "Invalid client details";

            return this.RedirectToAction(nameof(this.Index));
        }

        var serviceModel = _offeredServicesService.GetService(appointment.ServiceName);

        if (serviceModel == null)
        {
            return this.BadRequest();
        }

        var price = serviceModel.Fee;

        this.TempData["AppointmentMessage"] = "You have successfully booked an appointment!";

        return this.RedirectToAction(nameof(this.MyAppointments));
    }

    [Authorize]
    public IActionResult MyAppointments()
    {
        var appointments = _appointmentsService.MyAppointments(this.User.Identity.Name);

        return this.View(appointments);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Remove(int id)
    {
        var isRemoved = await _appointmentsService.RemoveAppointment(id);

        if (!isRemoved)
        {
            return this.BadRequest();
        }

        return this.RedirectToAction(nameof(this.MyAppointments));
    }
}
