namespace CreditConsult.Web.Controllers.Api;

using Microsoft.AspNetCore.Mvc;

using CreditConsult.Services.Interfaces;
using CreditConsult.Services.ViewModels.ApiModels;

[Route("api/services")]
[ApiController]
public class ServiceApiController : ControllerBase
{
    private readonly IOfferedServicesService _servicesService;

    public ServiceApiController(IOfferedServicesService servicesService)
        => _servicesService = servicesService;

    [HttpGet]
    public IEnumerable<ServiceDataModel> Get()
    {
        var services = _servicesService.GetAllServices();

        if (services == null)
        {
            return null;
        }

        var result = services.Select(s => new ServiceDataModel
        {
            Fee = s.Fee,
            Title = s.Title,
        });

        return result;
    }
}
