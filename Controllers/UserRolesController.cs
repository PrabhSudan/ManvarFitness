using ManvarFitness.Database;
using ManvarFitness.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ManvarFitness.Controllers
{
    public class UserRolesController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public UserRolesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // Secure entire controller (Admin only)
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            var loginCheck = RequireLogin();
            if (loginCheck != null)
            {
                context.Result = loginCheck;
                return;
            }
            var roleCheck = AuthorizeRole("Admin");
            if (roleCheck != null)
            {
                context.Result = roleCheck;
                return;
            }
            base.OnActionExecuting(context);
        }
        public IActionResult Index()
        {
            var roles = _context.Roles
                .Where(r => !r.IsDeleted)
                .ToList();

            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RoleModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                ModelState.AddModelError("Name", "Role name is required.");
                return View();
            }

            if (_context.Roles.Any(r => r.Name == model.Name))
            {
                ModelState.AddModelError("Name", "Role already exists.");
                return View(model);
            }
            var roleEntity = new Entity.Role
            {
                Name = model.Name,
            };
            _context.Roles.Add(roleEntity);
            _context.SaveChanges();
            TempData["Success"] = $"Role '{model.Name}' created successfully!";
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Toggle(int id)
        {
            var role = _context.Roles.Find(id);
            if (role == null)
            {
                return NotFound();
            }
            role.IsActive = !role.IsActive;
            _context.SaveChanges();
            TempData["Success"] = $"Role '{role.Name}' {(role.IsActive ? "deactivated" : "activated")} successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SoftDelete(int id)
        {
            var role = _context.Roles.Find(id);
            if (role == null)
            {
                return Json(new { success = false });
            }
            role.IsDeleted = true;
            
            _context.SaveChanges();
            return Json(new { success = true });
        }
    }
}
