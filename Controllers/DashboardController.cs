using ManvarFitness.Database;
using ManvarFitness.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManvarFitness.Controllers;

public class DashboardController : BaseController
{
    private readonly ApplicationDbContext _context;
    public DashboardController(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var model = new DashboardModel
        {
            Customers = _context.Users.Count(x => !x.IsDeleted),

            DietPlans = _context.DietPlans.Where(x => !x.IsDeleted)
                        .Select(x => x.ConcernId)   
                        .Distinct()
                        .Count(),

            WorkoutVideos = _context.WorkoutVideos.Count(x => !x.IsDeleted),

            Results = _context.Results.Count(x => !x.IsDeleted),
            AllCustomers = _context.Users.Where(x => !x.IsDeleted)
            .OrderByDescending(x => x.CreatedOn)
            .Take(5)
            .ToList()
        };
        return View(model);
    }
}
