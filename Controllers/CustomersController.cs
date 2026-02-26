using ManvarFitness.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManvarFitness.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CustomersController(ApplicationDbContext context)
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
        public IActionResult Toggle(int id)
        {
            var users = _context.Users.Find(id);
            if (users == null)
            {
                return NotFound();
            }
            users.IsActive = !users.IsActive;
            _context.SaveChanges();
            TempData["Success"] = $"Role '{users.Name}' {(users.IsActive ? "deactivated" : "activated")} successfully!";
            return RedirectToAction("Index");
        }
    }
}
