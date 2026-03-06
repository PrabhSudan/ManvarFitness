using ManvarFitness.Database;
using ManvarFitness.Entity;
using ManvarFitness.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ManvarFitness.Controllers
{
    public class UserRolesController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public UserRolesController(ApplicationDbContext context): base(context)
        {
            _context = context;
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
                return View(model);
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

            AssignDefaultPages(model.Name);

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

        private void AssignDefaultPages(string roleName)
        {
            if (string.IsNullOrEmpty(roleName) || roleName.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                return;

            var defaultPageNames = new List<string> 
            {
                "Dashboard", "Users", "Customers", "Results", "Result Overview", "Diet Plans", "Diet Plan", "Workouts", "Workout Videos", "Meditation Music", "Yoga & Exercises", "Herbs", "Herb Overview", "Herb Category", "Concerns", "Main Concerns", "SubConcerns", "Custom Forms" 
            };

            // Get main pages first
            var mainPages = _context.Pages
                .Where(p => p.IsActive && defaultPageNames.Contains(p.Name) && p.ParentId == null)
                .ToList();

            var allPageIds = new List<int>();

            foreach (var page in mainPages)
            {
                allPageIds.Add(page.Id);

                // Add sub-pages automatically
                var subPageIds = _context.Pages
                    .Where(sp => sp.IsActive && sp.ParentId == page.Id)
                    .Select(sp => sp.Id)
                    .ToList();

                allPageIds.AddRange(subPageIds);
            }

            // Assign all pages to the role
            foreach (var id in allPageIds.Distinct()) // avoid duplicates
            {
                _context.RolePages.Add(new RolePage
                {
                    RoleName = roleName,
                    PageId = id,
                    IsActive = true
                });
            }

            _context.SaveChanges();
        }
    }
}
