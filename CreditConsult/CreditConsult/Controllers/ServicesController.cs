namespace CreditConsult.Web.Controllers;

using Microsoft.AspNetCore.Mvc;

using CreditConsult.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

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

    public IActionResult Service(int id, string info)
    {
        var model = _offeredService.GetService(id);

        if (model == null || model.Title != info.Replace("-", " "))
        {
            return this.BadRequest();
        }

        return this.View(model);
    }

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Remove(int id)
    {
        var isRemoved = await _offeredService.Remove(id);

        if (!isRemoved)
        {
            return BadRequest();
        }

        return RedirectToAction(nameof(this.Index));
    }
}
