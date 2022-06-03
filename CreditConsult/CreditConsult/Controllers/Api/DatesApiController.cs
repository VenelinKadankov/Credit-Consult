namespace CreditConsult.Web.Controllers.Api;

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using CreditConsult.Services.Interfaces;

[Route("api/dates")]
[ApiController]
public class DatesApiController : ControllerBase
{
    private readonly IAppointmentService _appointmentsService;

    public DatesApiController(IAppointmentService appointmentsService)
        => _appointmentsService = appointmentsService;

    [HttpGet]
    public IEnumerable<string> Get(string name)
    {
        var dates = _appointmentsService.AllDates(name);

        return dates;
    }
}
