using ManvarFitness.Database;
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
        return View();
    }
}
