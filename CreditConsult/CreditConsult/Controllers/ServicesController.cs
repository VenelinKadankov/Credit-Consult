namespace CreditConsult.Web.Controllers;

using Microsoft.AspNetCore.Mvc;

using CreditConsult.Services.Interfaces;

public class ServicesController : Controller
{
    private readonly IOfferedServicesService _offeredService;

    public ServicesController(IOfferedServicesService offeredService)
    {
        _offeredService = offeredService;
    }

    public IActionResult Index()
    {
        var services = _offeredService.GetAllServices();

        return View(services);
    }
}
