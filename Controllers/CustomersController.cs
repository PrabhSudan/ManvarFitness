using ManvarFitness.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManvarFitness.Controllers
{
    public class CustomersController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public CustomersController(ApplicationDbContext context): base(context)
        {
            _context = context;
        }
        // GET: UserController
        public ActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleActive(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
                return Json(new { success = false });

            user.IsActive = !user.IsActive;
            _context.SaveChanges();

            return Json(new { success = true, isActive = user.IsActive });
        }
    }
}
