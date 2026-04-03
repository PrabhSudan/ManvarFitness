using ManvarFitness.Database;
using ManvarFitness.Entity;
using ManvarFitness.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

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

            var fieldsQuery = _context.CustomFields
                .Include(f => f.CustomForm)
                .Where(f => !f.IsDeleted)
                .AsQueryable();

            if (concernId.HasValue)
            {
                fieldsQuery = fieldsQuery.Where(f =>
                   f.CustomForm != null && f.CustomForm.ConcernId == concernId.Value
                );
            }
            else
            {
                fieldsQuery = fieldsQuery.Where(f => false); 
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
            ViewBag.ConcernsList = _context.Concerns
                .Select(c => new { Value = c.ConcernId, Text = c.Name })
                .ToList();

            ViewBag.SubConcernsList = new SelectList(
                _context.SubConcerns.ToList(),
                "SubConcernId",
                "Name"
            );

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var form = new CustomForm
            {
                ConcernId = model.ConcernId,
                SubConcernId = model.SubConcernId,
                CustomFieldData = model.CustomFieldData,
                IsDeleted = false
            };

            _context.CustomForms.Add(form);
            _context.SaveChanges();

            if (!string.IsNullOrEmpty(model.CustomFieldData))
            {
                var fields = JsonSerializer.Deserialize<List<CustomFieldModel>>(model.CustomFieldData);

                if (fields != null)
                {
                    foreach (var fieldData in fields)
                    {
                        if (string.IsNullOrEmpty(fieldData.FieldName) || string.IsNullOrEmpty(fieldData.FieldType))
                            continue;

                        string? options = "";

                        // ✅ OPTIONS (simple)
                        if (fieldData.FieldType == "Select" || fieldData.FieldType == "Radio" ||fieldData.FieldType == "Checkbox")
                        {
                            if (fieldData.Options != null && fieldData.Options.Any())
                            {
                                options = string.Join(", ", fieldData.Options
                                    .Where(o => !string.IsNullOrWhiteSpace(o))
                                    .Select(o => o.Trim()));
                            }
                        }

                        var validationList = new Dictionary<string, string>();

                        if (fieldData.FieldType == "Text" || fieldData.FieldType == "Textarea")
                        {
                            if (fieldData.MaxLength != null)
                                validationList.Add("MaxLength", $"{fieldData.MaxLength}");
                        }
                        else if (fieldData.FieldType == "Number")
                        {
                            validationList.Add("Min", $"{fieldData.Min}");
                            validationList.Add("Max", $"{fieldData.Max}");
                        }
                        else if (fieldData.FieldType == "Time")
                        {
                            validationList.Add("Start", $"{fieldData.StartTime}");
                            validationList.Add("End", $"{fieldData.EndTime}");
                        }
                        else if (fieldData.FieldType == "File")
                        {
                            if (fieldData.MaxFileSize != null)
                                validationList.Add("MaxFileSize", $"{fieldData.MaxFileSize}");          
                        }

                        _context.CustomFields.Add(new CustomField
                        {
                            CustomFormId = form.CustomFormId,
                            FieldName = fieldData.FieldName,
                            FieldType = fieldData.FieldType,
                            Options = JsonSerializer.Serialize(options),              
                            ValidationData = JsonSerializer.Serialize(validationList),
                            IsIncluded = fieldData.IsIncluded,
                            IsRequired = fieldData.IsRequired,
                            IsDeleted = false
                        });
                    }

                    _context.SaveChanges();
                }
            }

            return RedirectToAction(nameof(Index), new { concernId = model.ConcernId });
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
        public IActionResult DeleteField(int id)
        {
            var field = _context.CustomFields.Find(id);

            if (field == null)
            {
                return Json(new { success = false });
            }

            field.IsDeleted = true; 

            _context.SaveChanges();

            return Json(new { success = true });
        }
        [HttpPost]
        public async Task<IActionResult> ToggleReqyuired(Guid id)
        {
            var user = await _context.AdminUsers.FindAsync(id);
            if (user == null) return NotFound();

            user.IsActive = !user.IsActive;
            user.UpdatedOn = DateTime.UtcNow;
            user.UpdatedBy = null;

            await _context.SaveChangesAsync();

            return Json(new { success = true, isActive = user.IsActive });
        }

        [HttpPost]
        public async Task<IActionResult> ToggleIsIncluded(Guid id)
        {
            var user = await _context.AdminUsers.FindAsync(id);
            if (user == null) return NotFound();

            user.IsActive = !user.IsActive;
            user.UpdatedOn = DateTime.UtcNow;
            user.UpdatedBy = null;

            await _context.SaveChangesAsync();

            return Json(new { success = true, isActive = user.IsActive });
        }

    }
}