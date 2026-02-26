using ManvarFitness.Database;
using ManvarFitness.Entity;
using ManvarFitness.Models; // your SubConcernsModel
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ManvarFitness.Controllers
{
    public class SubConcernController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubConcernController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ===== List =====
        public IActionResult Index()
        {
            var subConcerns = _context.SubConcerns
                .Include(s => s.Concern)
                .ToList();
            return View(subConcerns);
        }

        // ===== Create GET =====
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.ConcernsList = new SelectList(
                _context.Concerns.Where(c => c.IsActive),
                "ConcernId",
                "Name"
            );
            return View();
        }

        // ===== Create POST =====
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SubConcernsModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = new SubConcerns
                {
                    Name = model.Name,
                    ConcernId = model.ConcernId,
                    IsActive = model.IsActive
                };

                _context.SubConcerns.Add(entity);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // If validation fails, repopulate dropdown and return View
            ViewBag.ConcernsList = new SelectList(
                _context.Concerns.Where(c => c.IsActive),
                "ConcernId",
                "Name",
                model.ConcernId
            );

            return View(model);
        }

        // ===== Toggle Active/Inactive =====
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Toggle(int id)
        {
            var sub = _context.SubConcerns.Find(id);
            if (sub == null) return NotFound();

            sub.IsActive = !sub.IsActive;
            _context.SaveChanges();

            TempData["Success"] = $"Sub Concern '{sub.Name}' has been {(sub.IsActive ? "activated" : "deactivated")}.";
            return RedirectToAction("Index");
        }

        // ===== Soft Delete =====
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SoftDelete(int id)
        {
            var sub = _context.SubConcerns.Find(id);
            if (sub == null)
            {
                return Json(new { success = false });
            }
            _context.SubConcerns.Remove(sub);

            _context.SaveChanges();
            return Json(new { success = true });
        }
    }
}