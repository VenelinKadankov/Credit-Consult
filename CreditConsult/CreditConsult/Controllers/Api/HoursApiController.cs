namespace CreditConsult.Web.Controllers.Api;

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using CreditConsult.Services.Interfaces;
using CreditConsult.Services.ViewModels.ApiModels;

[Route("api/hours")]
[ApiController]
public class HoursApiController : ControllerBase
{
    private readonly IAppointmentService _appointmentsService;

    public HoursApiController(IAppointmentService appointmentsService)
        => _appointmentsService = appointmentsService;

    [HttpGet]
    public IEnumerable<FreeHourModel> Get(
        string employeeName,
        string date)
    {
        var hours = _appointmentsService.AllFreeHours(employeeName, date);

        if (hours == null)
        {
            return null;
        }

        return hours;
    }
}
