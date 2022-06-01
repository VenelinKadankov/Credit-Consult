namespace CreditConsult.Web.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using CreditConsult.Services.Interfaces;
using CreditConsult.Services.Services;

public class AppointmentsController : Controller
{
    private readonly IAppointmentService _appointmentsService;

    public AppointmentsController(AppointmentService appointmentsService)
    {
        _appointmentsService = appointmentsService;
    }

    [Authorize]
    public IActionResult Index()
    {
        var appointments = _appointmentsService.CreateEmptyAppointment();

        return this.View(appointments);
    }

    //[HttpPost]
    //[Authorize]
    //public async Task<IActionResult> Index(AppointmentInputFormModel appointment)
    //{
    //    if (!this.ModelState.IsValid)
    //    {
    //        return this.View(appointment);
    //    }

    //    var isCreated = await this.appointmentsService.CreateAppointment(
    //        appointment.Doctor,
    //        appointment.Date,
    //        appointment.Time,
    //        appointment.PatientName,
    //        appointment.DepartmentName,
    //        appointment.Message,
    //        appointment.PatientEmail,
    //        appointment.PatientPhone,
    //        appointment.TestName,
    //        this.User.Identity.Name,
    //        this.User.GetId());

    //    if (!isCreated)
    //    {
    //        this.TempData[InvalidCredentials] = "Invalid patient details";

    //        return this.RedirectToAction(nameof(this.Index));
    //    }

    //    var testModel = this.testsService.GetTest(appointment.TestName);

    //    if (testModel == null)
    //    {
    //        return this.BadRequest();
    //    }

    //    var price = testModel.Price;

    //    this.TempData[TempAppointmentMessage] = "You have successfully booked an appointment!";

    //    return this.RedirectToAction(nameof(this.Pay), new { price });
    //}

    //[Authorize]
    //public IActionResult MyAppointments()
    //{
    //    var appointments = this.appointmentsService.MyAppointments(this.User.Identity.Name);

    //    return this.View(appointments);
    //}

    //[HttpPost]
    //[Authorize]
    //public async Task<IActionResult> Remove(int id)
    //{
    //    var isRemoved = await this.appointmentsService.RemoveAppointment(id);

    //    if (!isRemoved)
    //    {
    //        return this.BadRequest();
    //    }

    //    return this.RedirectToAction(nameof(this.MyAppointments));
    //}
}
