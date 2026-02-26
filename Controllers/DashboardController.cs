using Microsoft.AspNetCore.Mvc;

namespace ManvarFitness.Controllers;

public class DashboardController : BaseController
{
  public IActionResult Index()
    {
        var loginCheck = RequireLogin();
        if (loginCheck != null) return loginCheck;

        var roleCheck = AuthorizeRole("Admin", "Coach");
        if (roleCheck != null) return roleCheck;
        return View();
    }
}
