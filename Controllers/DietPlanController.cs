using ManvarFitness.Database;
using ManvarFitness.Entity;
using ManvarFitness.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ManvarFitness.Controllers
{
    public class DietPlanController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly List<string> dayOrder = new()
        {
            "Monday","Tuesday","Wednesday","Thursday","Friday","Saturday","Sunday"
        };
        public DietPlanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Helper: Safely deserialize JSON stored in DB
        private List<string> SafeDeserialize(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return new List<string>();
            try
            {
                return JsonSerializer.Deserialize<List<string>>(value) ?? new List<string>();
            }
            catch
            {
                return new List<string> { value };
            }
        }

        // GET: DietPlan
        public IActionResult Index(int? concernId = null)
        {
            ViewBag.ConcernsList = _context.Concerns
                .Where(c => c.IsActive)
                .OrderBy(c => c.Name)
                .ToList();

            ViewBag.SubConcernsList = concernId.HasValue 
                ? _context.SubConcerns
               .Where(sc => sc.IsActive && sc.ConcernId == concernId.Value)
               .OrderBy(sc => sc.Name)
               .ToList()
               : new List<SubConcerns>();

            var viewModel = new DietPlanModel();

            if (concernId.HasValue)
            {
                var concern = _context.Concerns.FirstOrDefault(c => c.ConcernId == concernId.Value);
                viewModel.ConcernId = concernId.Value;
                viewModel.Concern = concern;

                // Get all plans for this concern
                var plans = _context.DietPlans
                    .Where(p => !p.IsDeleted && p.ConcernId == concernId.Value)
                    .ToList();

                if (plans.Any())
                {
                    viewModel.Name = plans.First().Name;
                    viewModel.Days = plans.Select(p => new DietDayModel
                    {
                        DayName = p.DayName,
                        EmptyStomach = SafeDeserialize(p.EmptyStomach),
                        EarlyMorningSnack = SafeDeserialize(p.EarlyMorningSnack),
                        Exercise = SafeDeserialize(p.Exercise),
                        Breakfast = SafeDeserialize(p.Breakfast),
                        MidMorningSnack = SafeDeserialize(p.MidMorningSnack),
                        Lunch = SafeDeserialize(p.Lunch),
                        EveningSnack = SafeDeserialize(p.EveningSnack),
                        Dinner = SafeDeserialize(p.Dinner),
                        Bedtime = SafeDeserialize(p.Bedtime)
                    })
                     .OrderBy(d => dayOrder.IndexOf(d.DayName))
                     .ToList();

                    ViewBag.AllPlans = plans;
                }
                else
                {
                    // Create default empty day
                    viewModel.Days.Add(new DietDayModel { DayName = "" });
                }
            }
            else
            {
                viewModel.Days.Add(new DietDayModel { DayName = "" });
            }

            return View(viewModel);
        }

        // POST: Save Diet Plan (AJAX)
        [HttpPost]
        public IActionResult SaveDietPlan([FromBody] DietPlanModel model)
        {
            if (model == null || model.Days == null || !model.Days.Any())
                return BadRequest();

            var existingPlans = _context.DietPlans
                .Where(p => p.ConcernId == model.ConcernId && !p.IsDeleted)
                .ToList();

            foreach (var day in model.Days)
            {
                if (string.IsNullOrWhiteSpace(day.DayName)) continue;

                var existing = existingPlans.FirstOrDefault(x => x.DayName == day.DayName);
                if (existing != null)
                {
                    // Update existing
                    existing.EmptyStomach = JsonSerializer.Serialize(day.EmptyStomach ?? new List<string>());
                    existing.EarlyMorningSnack = JsonSerializer.Serialize(day.EarlyMorningSnack ?? new List<string>());
                    existing.Exercise = JsonSerializer.Serialize(day.Exercise ?? new List<string>());
                    existing.Breakfast = JsonSerializer.Serialize(day.Breakfast ?? new List<string>());
                    existing.MidMorningSnack = JsonSerializer.Serialize(day.MidMorningSnack ?? new List<string>());
                    existing.Lunch = JsonSerializer.Serialize(day.Lunch ?? new List<string>());
                    existing.EveningSnack = JsonSerializer.Serialize(day.EveningSnack ?? new List<string>());
                    existing.Dinner = JsonSerializer.Serialize(day.Dinner ?? new List<string>());
                    existing.Bedtime = JsonSerializer.Serialize(day.Bedtime ?? new List<string>());
                    existing.Name = model.Name;
                }
                else
                {
                    // Add new
                    _context.DietPlans.Add(new DietPlan
                    {
                        Name = model.Name,
                        ConcernId = model.ConcernId,
                        DayName = day.DayName,
                        EmptyStomach = JsonSerializer.Serialize(day.EmptyStomach ?? new List<string>()),
                        EarlyMorningSnack = JsonSerializer.Serialize(day.EarlyMorningSnack ?? new List<string>()),
                        Exercise = JsonSerializer.Serialize(day.Exercise ?? new List<string>()),
                        Breakfast = JsonSerializer.Serialize(day.Breakfast ?? new List<string>()),
                        MidMorningSnack = JsonSerializer.Serialize(day.MidMorningSnack ?? new List<string>()),
                        Lunch = JsonSerializer.Serialize(day.Lunch ?? new List<string>()),
                        EveningSnack = JsonSerializer.Serialize(day.EveningSnack ?? new List<string>()),
                        Dinner = JsonSerializer.Serialize(day.Dinner ?? new List<string>()),
                        Bedtime = JsonSerializer.Serialize(day.Bedtime ?? new List<string>()),
                        IsActive = true,
                        IsDeleted = false
                    });
                }
            }

            _context.SaveChanges();
            return Json(new { success = true });
        }

        // GET: Plan Schedule
        public IActionResult PlanSchedule()
        {
            var plans = _context.DietPlans
                                .Where(d => !d.IsDeleted && d.IsActive)
                                .OrderBy(d => d.Name)
                                .ToList();
            return View(plans);
        }

        // POST: Toggle active/inactive
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Toggle(int id)
        {
            var plan = _context.DietPlans.Find(id);
            if (plan == null) return NotFound();

            plan.IsActive = !plan.IsActive;
            _context.SaveChanges();

            TempData["Success"] = $"Diet Plan '{plan.Name}' {(plan.IsActive ? "activated" : "deactivated")} successfully!";
            return RedirectToAction("Index");
        }

        // POST: Delete plan
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var plan = _context.DietPlans.Find(id);
            if (plan != null)
            {
                plan.IsDeleted = true;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}