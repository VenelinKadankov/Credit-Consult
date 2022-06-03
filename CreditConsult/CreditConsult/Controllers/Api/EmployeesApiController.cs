namespace CreditConsult.Web.Controllers.Api;

using Microsoft.AspNetCore.Mvc;

using CreditConsult.Services.Interfaces;
using CreditConsult.Services.ViewModels.ApiModels;

[Route("api/employees")]
[ApiController]
public class EmployeesApiController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IOfferedServicesService _servicesService;

    public EmployeesApiController(IUserService userService, IOfferedServicesService offeredServicesService)
    {
        _servicesService = offeredServicesService;
        _userService = userService;
    }

    [HttpGet]
    public ConsultDataModel Get()
    {
        var employees = _userService
            .GetEmployees()
            .Select(e => new EmployeeDataModel
            {
                Name = e.Name,
                Dates = e.Appointments.Select(x => new AppointmentDataModel
                {
                    Date = x.Date.ToString(),
                }),
            });

        var services = _servicesService
            .GetAllServices()
            .Select(t => new ServiceDataModel
            {
                Title = t.Title,
                Fee = t.Fee,
            });

        var consultData = new ConsultDataModel
        {
            Employees = employees,
            Services = services,
        };

        return consultData;
    }
}
