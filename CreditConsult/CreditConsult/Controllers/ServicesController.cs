namespace CreditConsult.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using CreditConsult.Services.Interfaces;
using CreditConsult.Services.InputModels;

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
            return BadRequest();
        }

        return View(model);
    }

    [Authorize(Roles = "Administrator")]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Add(ServiceInputModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _offeredService.Add(
            model.Title,
            model.Description,
            model.Fee,
            model.ImageUrl);

        if (!result)
        {
            return BadRequest();
        }

        return RedirectToAction(nameof(this.Index));
    }

    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Edit(int id)
    {
        var service = _offeredService.GetService(id);

        if (service == null)
        {
            return BadRequest();
        }

        return View(service);
    }

    [HttpPut]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Edit(int id, ServiceInputModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _offeredService.Update(
            id,
            model.Title,
            model.Description,
            model.Fee,
            model.ImageUrl);

        if (!result)
        {
            return BadRequest();
        }

        return RedirectToAction(nameof(Index));
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

        return RedirectToAction(nameof(Index));
    }
}
