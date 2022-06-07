namespace CreditConsult.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using CreditConsult.Services.Interfaces;
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

        return View(appointments);
    }

    [HttpPost]
    [Authorize(Roles = "Administrator, Client")]
    public async Task<IActionResult> Index(AppointmentInputModel appointment)
    {
        if (!ModelState.IsValid)
        {
            return View(appointment);
        }

        var isCreated = await _appointmentsService.CreateAppointment(
            appointment.Employee,
            appointment.Date,
            appointment.Time,
            appointment.Message,
            appointment.ClientName,
            appointment.ClientEmail,
            appointment.ClientPhone,
            appointment.ServiceName,
            User.Identity.Name,
            User.GetId());

        if (!isCreated)
        {
            TempData["InvalidData"] = "Invalid client details";

            return RedirectToAction(nameof(Index));
        }

        var serviceModel = _offeredServicesService.GetService(appointment.ServiceName);

        if (serviceModel == null)
        {
            return BadRequest();
        }

        var price = serviceModel.Fee;

        TempData["AppointmentMessage"] = "You have successfully booked an appointment!";

        return RedirectToAction(nameof(MyAppointments));
    }

    [Authorize(Roles = "Administrator, Client")]
    public IActionResult MyAppointments()
    {
        var appointments = _appointmentsService.MyAppointments(User.Identity.Name);

        return View(appointments);
    }

    [HttpPost]
    [Authorize(Roles = "Administrator, Client")]
    public async Task<IActionResult> Remove(int id)
    {
        var isRemoved = await _appointmentsService.RemoveAppointment(id);

        if (!isRemoved)
        {
            return BadRequest();
        }

        return RedirectToAction(nameof(MyAppointments));
    }
}
