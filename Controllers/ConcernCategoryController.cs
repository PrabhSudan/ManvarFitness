using ManvarFitness.Database;
using ManvarFitness.Entity;
using ManvarFitness.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ManvarFitness.Controllers
{
    public class ConcernCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ConcernCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        // Secure entire controller (Admin only)
        
        public IActionResult Index()
        {
            var categories = _context.Concerns
                .Where(c => !c.IsDeleted)
                .ToList();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ConcernModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                ModelState.AddModelError("Name", "Category name is required.");
                return View();
            }

            if (_context.Concerns.Any(r => r.Name == model.Name))
            {
                ModelState.AddModelError("Name", "Category already exists.");
                return View(model);
            }
            var categoryEntity = new Concerns
            {
                Name = model.Name,
                IsActive = true
            };

            _context.Concerns.Add(categoryEntity);
            _context.SaveChanges();
            TempData["Success"] = $"Category '{model.Name}' created successfully!";
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Toggle(int id)
        {
            var category = _context.Concerns.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            category.IsActive = !category.IsActive;
            _context.SaveChanges();

            TempData["Success"] = $"Category '{category.Name}' has been {((bool)category.IsActive ? "activated" : "deactivated")} successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SoftDelete(int id)
        {
            var categories = _context.Concerns.Find(id);
            if (categories == null)
            {
                return Json(new { success = false });
            }
            categories.IsDeleted = true;
            _context.SaveChanges();
            return Json(new { success = true });
        }
    }
}
