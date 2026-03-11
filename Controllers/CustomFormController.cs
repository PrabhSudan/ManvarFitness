using ManvarFitness.Database;
using ManvarFitness.Entity;
using ManvarFitness.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ManvarFitness.Controllers
{
    public class CustomFormController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public CustomFormController(ApplicationDbContext context): base(context)
        {
            _context = context;
        }

        public IActionResult Index(int? concernId)
        {
            ViewBag.Concerns = _context.Concerns.ToList();
            ViewBag.SelectedConcern = concernId;

            // Base query: default fields (IsDefault) + fields from forms for this concern
            var fieldsQuery = _context.CustomFields
                .Include(f => f.CustomForm)
                .AsQueryable();

            if (concernId.HasValue)
            {
                // Fields either default or attached to a form of this concern
                fieldsQuery = fieldsQuery.Where(f =>
                    f.IsDefault ||
                    (f.CustomForm != null && f.CustomForm.ConcernId == concernId.Value)
                );
            }
            else
            {
                // Just defaults if no concern selected
                fieldsQuery = fieldsQuery.Where(f => f.IsDefault);
            }

            var fields = fieldsQuery.OrderBy(f => f.CustomFieldId).ToList();

            return View(fields);
        }

        // CREATE GET
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.ConcernsList = _context.Concerns
                .Select(c => new { Value = c.ConcernId, Text = c.Name })
                .ToList();

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
            if (!ModelState.IsValid) return View(model);

            var form = new CustomForm
            {
                Name = model.Name,
                ConcernId = model.ConcernId,
                SubConcernId = model.SubConcernId,
                IsActive = true
            };

            _context.CustomForms.Add(form);
            _context.SaveChanges();

            // Save only the user-added fields
            if (model.Fields != null && model.Fields.Any())
            {
                var fields = model.Fields.Select(f => new CustomField
                {
                    Label = f.Label,
                    FieldType = f.FieldType,
                    MinValue = f.MinValue,
                    MaxValue = f.MaxValue,
                    MaxLength = f.MaxLength,
                    IsActive = true,
                    IsRequired = f.IsRequired,
                    Options = f.Options,
                    Date = f.Date,
                    StartTime = f.StartTime,
                    EndTime = f.EndTime,
                    MaxFileSize = f.MaxFileSize,
                    CustomFormId = form.CustomFormId,  // attach to this form
                }).ToList();

                _context.CustomFields.AddRange(fields);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index), new { concernId = model.ConcernId });
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

            field.IsDeleted = true;   // correct soft delete
            field.IsActive = false;

            _context.SaveChanges();

            return Json(new { success = true });
        }

        private void EnsureDefaultFields()
        {
            // Check if defaults already exist
            if (!_context.CustomFields.Any(f => f.IsDefault))
            {
                var defaultFields = new List<CustomField>
                {
                    new CustomField { Label = "Name", FieldType = "Text", MaxLength=100,IsDefault = true, IsActive = true },
                    new CustomField { Label = "Age", FieldType = "Number", MinValue = 1, MaxValue = 120, IsDefault = true, IsActive = true },
                    new CustomField { Label = "Gender", FieldType = "Select", Options = "Male,Female,Other", IsDefault = true, IsActive = true },
                    new CustomField { Label = "Height (Feet)", FieldType = "Number", MinValue = 1, MaxValue = 8, IsDefault = true, IsActive = true },
                    new CustomField { Label = "Height (Inches)", FieldType = "Number", MinValue = 0, MaxValue = 11, IsDefault = true, IsActive = true },
                    new CustomField { Label = "Weight (Kg)", FieldType = "Number", MinValue = 1, MaxValue = 300, IsDefault = true, IsActive = true }
                };

                _context.CustomFields.AddRange(defaultFields);
                _context.SaveChanges();
            }
        }
    }
}