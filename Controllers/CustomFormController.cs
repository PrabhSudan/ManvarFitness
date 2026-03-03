using ManvarFitness.Database;
using ManvarFitness.Entity;
using ManvarFitness.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ManvarFitness.Controllers
{
    public class CustomFormController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CustomFormController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? concernId)
        {
            ViewBag.Concerns = _context.Concerns.ToList();
            ViewBag.SelectedConcern = concernId;

            var fields = _context.CustomFields
                .Include(f => f.CustomForm)
                .ThenInclude(cf => cf.Concern)
                .AsQueryable();

            if (concernId.HasValue)
            {
                fields = fields.Where(f => f.CustomFormId == null || f.CustomForm.ConcernId == concernId);
            }

            return View(fields.ToList());
        }

        // CREATE GET
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.ConcernsList = new SelectList(
                _context.Concerns.ToList(),
                "ConcernId",
                "Name"
            );

            ViewBag.SubConcernsList = new SelectList(
                _context.SubConcerns.ToList(),
                "SubConcernId",
                "Name"
            );
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CustomFormModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ConcernsList = new SelectList(_context.Concerns.ToList(), "ConcernId", "Name", model.ConcernId);
                ViewBag.SubConcernsList = new SelectList(
                    _context.SubConcerns.Where(s => s.ConcernId == model.ConcernId).ToList(),
                    "SubConcernId", "Name", model.SubConcernId
                );
                return View(model);
            }

            // Create CustomForm first
            var form = new CustomForm
            {
                Name = model.Name,
                ConcernId = model.ConcernId,
                SubConcernId = model.SubConcernId,
                IsActive = true
            };

            _context.CustomForms.Add(form);
            _context.SaveChanges();

            if (model.Fields != null && model.Fields.Any())
            {
                var fields = model.Fields.Select(f => new CustomField
                {
                    Label = f.Label,
                    FieldType = f.FieldType,
                    MinValue = f.MinValue,
                    MaxValue = f.MaxValue,
                    MaxLength = f.MaxLength,
                    IsActive = f.IsActive,
                    IsRequired = f.IsRequired,
                    CustomFormId = form.CustomFormId
                }).ToList();

                _context.CustomFields.AddRange(fields);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        // Fill FORM
        public IActionResult Fill(int id)
        {
            var form = _context.CustomForms
                .Include(f => f.Concern)
                .Include(f => f.CustomFields)
                .FirstOrDefault(f => f.CustomFormId == id && f.IsActive);
            if (form == null)
                return NotFound();

            return View(form);
        }

        public IActionResult GetSubConcerns(int? concernId)
        {
            var query = _context.SubConcerns.AsQueryable();

            if (concernId.HasValue)
            {
                query = query.Where(s => s.ConcernId == concernId.Value);
            }

            var subConcerns = query
                .Select(s => new
                {
                    subConcernId = s.SubConcernId,
                    name = s.Name
                })
                .ToList();

            return Json(subConcerns);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleActive(int id)
        {
            var field = _context.CustomFields.Find(id);
            if (field == null) return NotFound();

            field.IsActive = !field.IsActive;
            _context.SaveChanges();

            return Json(new { success = true, isActive = field.IsActive });
        }

        // TOGGLE IS REQUIRED
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleRequired(int id)
        {
            var field = _context.CustomFields.Find(id);
            if (field == null)
                return NotFound();

            field.IsRequired = !field.IsRequired;
            _context.SaveChanges();

            return Json(new { success = true, isRequired = field.IsRequired });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteField(int id)
        {
            var field = _context.CustomFields.Find(id);
            if (field == null)
            {
                return Json(new { success = false });
            }

            field.IsActive = false; // soft delete
            _context.SaveChanges();

            return Json(new { success = true });
        }
    }
}