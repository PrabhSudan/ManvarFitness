using ManvarFitness.Database;
using ManvarFitness.Entity;
using ManvarFitness.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManvarFitness.Controllers
{
    public class HerbsCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HerbsCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        // Secure entire controller (Admin only)

        public IActionResult Index()
        {
            var categories = _context.HerbCategories.ToList();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(HerbCategoryModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                ModelState.AddModelError("Name", "HerbsCategory name is required.");
                return View();
            }

            if (_context.HerbCategories.Any(r => r.Name == model.Name))
            {
                ModelState.AddModelError("Name", "HerbCategory already exists.");
                return View(model);
            }
            var categoryEntity = new HerbCategory
            {
                Name = model.Name,
                IsActive = true
            };

            _context.HerbCategories.Add(categoryEntity);
            _context.SaveChanges();
            TempData["Success"] = $"HerbsCategory '{model.Name}' created successfully!";
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Toggle(int id)
        {
            var categories = _context.HerbCategories.Find(id);
            if (categories == null)
            {
                return NotFound();
            }
            categories.IsActive = !categories.IsActive;
            _context.SaveChanges();
            TempData["Success"] = $"HerbsCategory '{categories.Name}' {(categories.IsActive ? "deactivated" : "activated")} successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SoftDelete(int id)
        {
            var categories = _context.HerbCategories.Find(id);
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
