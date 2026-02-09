using Microsoft.AspNetCore.Mvc;

namespace ManvarFitness.Controllers
{
    public class UserActivityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
