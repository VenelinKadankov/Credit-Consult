namespace CreditConsult.Services.Services;

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using CreditConsult.Data.Data;
using CreditConsult.Data.Models;
using CreditConsult.Services.Interfaces;
using CreditConsult.Services.ViewModels;

public class OfferedServicesService : IOfferedServicesService
{
    private readonly ApplicationDbContext _context;

    public OfferedServicesService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Add(string title, string description, decimal price, string imageUrl)
    {
        if (_context.OfferedServices.Any(s => s.Title == title))
        {
            return false;
        }

        var currentTest = new OfferedService
        {
            Title = title,
            Description = description,
            Fee = price,
            ImageUrl = imageUrl,
        };

        await _context.OfferedServices.AddAsync(currentTest);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task AddEmployeeToService(string employeeId, int serviceId)
    {
        if (_context.Users.Any(s => s.Id == employeeId) && _context.OfferedServices.Any(s => s.Id == serviceId))
        {
            return;
        }

        var service = _context.OfferedServices.FirstOrDefault(s => s.Id == serviceId);
        var employee = _context.Users.FirstOrDefault(u => u.Id == employeeId);
        service.Employees.Add(employee);

        await _context.SaveChangesAsync();
    }

    public int AllServicess()
        => _context.OfferedServices.AsNoTracking().IgnoreQueryFilters().Where(x => !x.IsDeleted).Count();

    public IEnumerable<ServiceViewModel> GetAllServices()
        => _context.OfferedServices.AsNoTracking().IgnoreQueryFilters().Where(x => !x.IsDeleted)
                .Select(s => new ServiceViewModel
                {
                    Description = s.Description,
                    Id = s.Id,
                    Title = s.Title,
                    Fee = s.Fee,
                    ImageUrl = s.ImageUrl
                });

    public ServiceViewModel GetService(int id)
    {

        if (!_context.OfferedServices.Any(t => t.Id == id))
        {
            return null;
        }

        return _context.OfferedServices.AsNoTracking().IgnoreQueryFilters().Where(x => !x.IsDeleted)
            .Select(x => new ServiceViewModel
            {
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                Description = x.Description,
                Title = x.Title,
                Fee = x.Fee,
            })
            .FirstOrDefault(x => x.Id == id);
    }

    public ServiceViewModel GetService(string title)
    {
        if (!_context.OfferedServices.Any(t => t.Title == title))
        {
            return null;
        }

        return _context.OfferedServices.AsNoTracking().IgnoreQueryFilters().Where(x => !x.IsDeleted)
            .Select(x => new ServiceViewModel
            {
                Id = x.Id,
                ImageUrl = x.ImageUrl,
                Description = x.Description,
                Title = x.Title,
                Fee = x.Fee,
            })
            .FirstOrDefault(x => x.Title == title);
    }

    public async Task<bool> Remove(int id)
    {
        if (!_context.OfferedServices.Any(t => t.Id == id))
        {
            return false;
        }

        _context.OfferedServices
            .FirstOrDefault(n => n.Id == id)
            .IsDeleted = true;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> Update(int id, string title, string description, decimal price, string imageUrl)
    {
        if (title == null || description == null || imageUrl == null)
        {
            return false;
        }

        var service = _context.OfferedServices.FirstOrDefault(s => s.Id == id);

        if (service == null)
        {
            return false;
        }

        service.Fee = price;
        service.Title = title;
        service.Description = description;
        service.ImageUrl = imageUrl;

        await _context.SaveChangesAsync();

        return true;
    }
}
