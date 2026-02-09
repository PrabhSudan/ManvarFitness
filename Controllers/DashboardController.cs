using Microsoft.AspNetCore.Mvc;

namespace ManvarFitness.Controllers;

public class DashboardController : Controller
{
  public IActionResult Index() => View();
}
