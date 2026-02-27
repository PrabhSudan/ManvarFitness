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
        public DietPlanController(ApplicationDbContext context)
        {
            _context = context;
        }
        // Secure entire controller (Admin only)

        public IActionResult Index(string name, int? concernId = null)
        {
            ViewBag.ConcernsList = _context.Concerns
                .Where(c => c.IsActive)
                .OrderBy(c => c.Name)
                .ToList();

            var model = new DietPlanModel();
            if (concernId.HasValue)
            {
                model.ConcernId = concernId.Value;

                var concern = _context.Concerns.FirstOrDefault(c => c.ConcernId == concernId.Value);
                model.Concern = concern;

                var plans = _context.DietPlans
                    .Where(p => !p.IsDeleted && p.ConcernId == concernId.Value)
                    .OrderBy(p => p.DayName)
                    .ToList();

                if (plans.Any())
                {
                    model.Name = plans.First().Name;
                    model.DayName = plans.Select(p => p.DayName).ToList();
                    model.EmptyStomach = plans.SelectMany(p => JsonSerializer.Deserialize<List<string>>(p.EmptyStomach ?? "[]")).ToList();
                    model.EarlyMorningSnack = plans.SelectMany(p => JsonSerializer.Deserialize<List<string>>(p.EarlyMorningSnack ?? "[]")).ToList();
                    model.Exercise = plans.SelectMany(p => JsonSerializer.Deserialize<List<string>>(p.Exercise ?? "[]")).ToList();
                    model.Breakfast = plans.SelectMany(p => JsonSerializer.Deserialize<List<string>>(p.Breakfast ?? "[]")).ToList();
                    model.MidMorningSnack = plans.SelectMany(p => JsonSerializer.Deserialize<List<string>>(p.MidMorningSnack ?? "[]")).ToList();
                    model.Lunch = plans.SelectMany(p => JsonSerializer.Deserialize<List<string>>(p.Lunch ?? "[]")).ToList();
                    model.EveningSnack = plans.SelectMany(p => JsonSerializer.Deserialize<List<string>>(p.EveningSnack ?? "[]")).ToList();
                    model.Dinner = plans.SelectMany(p => JsonSerializer.Deserialize<List<string>>(p.Dinner ?? "[]")).ToList();
                    model.Bedtime = plans.SelectMany(p => JsonSerializer.Deserialize<List<string>>(p.Bedtime ?? "[]")).ToList();
                }
                else
                {
                    model.DayName = new List<string> { "" };
                }

            }
            else
            {
                model.DayName = new List<string> { "" };
            }
            _context.SaveChanges();
            return View(model);
        }

        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(DietPlanModel model)
        //{
        //    if (string.IsNullOrWhiteSpace(model.Name))
        //    {
        //        ModelState.AddModelError("Name", "Category name is required.");
        //        return View();
        //    }
        //    // Get existing rows of this plan
        //    var existingPlans = _context.DietPlans
        //        .Where(p => p.Name == model.Name && !p.IsDeleted)
        //        .ToList();
        //    foreach (var day in model.DayName)
        //    {
        //        bool alreadyExists = existingPlans.Any(p => p.DayName == day);
        //        if(!alreadyExists)
        //        {
        //            var newPlan = new DietPlan
        //            {
        //                Name = model.Name,
        //                DayName = day,
        //                EmptyStomach = JsonSerializer.Serialize(model.EmptyStomach),
        //                EarlyMorningSnack = JsonSerializer.Serialize(model.EarlyMorningSnack),
        //                Exercise = JsonSerializer.Serialize(model.Exercise),
        //                Breakfast = JsonSerializer.Serialize(model.Breakfast),
        //                MidMorningSnack = JsonSerializer.Serialize(model.MidMorningSnack),
        //                Lunch = JsonSerializer.Serialize(model.Lunch),
        //                EveningSnack = JsonSerializer.Serialize(model.EveningSnack),
        //                Dinner = JsonSerializer.Serialize(model.Dinner),
        //                Bedtime = JsonSerializer.Serialize(model.Bedtime),
        //                Description = model.Description,
        //                IsActive = model.IsActive
        //            };
        //            _context.DietPlans.Add(newPlan);
        //        }
        //    }

        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //// GET: DietPlan/Details/5
        //public IActionResult Details(int id)
        //{
        //    var plan = _context.DietPlans.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
        //    if (plan == null)
        //        return NotFound();

        //    var alldays = _context.DietPlans
        //        .Where(x => x.Name == plan.Name && !x.IsDeleted);

        //    var viewModel = new DietPlanModel
        //    {
        //        Name = plan.Name,
        //        DayName = alldays.Select(x => x.DayName).ToList(),
        //        EmptyStomach = JsonSerializer.Deserialize<List<string>>(plan.EmptyStomach ?? "[]"),
        //        EarlyMorningSnack = JsonSerializer.Deserialize<List<string>>(plan.EarlyMorningSnack ?? "[]"),
        //        Exercise = JsonSerializer.Deserialize<List<string>>(plan.Exercise ?? "[]"),
        //        Breakfast = JsonSerializer.Deserialize<List<string>>(plan.Breakfast ?? "[]"),
        //        MidMorningSnack = JsonSerializer.Deserialize<List<string>>(plan.MidMorningSnack ?? "[]"),
        //        Lunch = JsonSerializer.Deserialize<List<string>>(plan.Lunch ?? "[]"),
        //        EveningSnack = JsonSerializer.Deserialize<List<string>>(plan.EveningSnack ?? "[]"),
        //        Dinner = JsonSerializer.Deserialize<List<string>>(plan.Dinner ?? "[]"),
        //        Bedtime = JsonSerializer.Deserialize<List<string>>(plan.Bedtime ?? "[]"),
        //        Description = plan.Description,
        //        IsActive = plan.IsActive
        //    };

        //    return View(viewModel);
        //}

        [HttpPost]
        public IActionResult SaveDietPlan([FromBody] List<DietPlanModel> models)
        {
            if (models == null || !models.Any()) return BadRequest();

            var concernId = models.First().ConcernId;

            var existingPlans = _context.DietPlans
                .Where(x => x.ConcernId == concernId && !x.IsDeleted)
                .ToList();

            foreach (var model in models)
            {
                var dayname = model.DayName.FirstOrDefault();
                if (string.IsNullOrWhiteSpace(dayname)) continue;

                var existing = existingPlans.FirstOrDefault(x => x.DayName == dayname);
                if (existing != null)
                {
                    existing.EmptyStomach = JsonSerializer.Serialize(model.EmptyStomach ?? new List<string>());
                    existing.EarlyMorningSnack = JsonSerializer.Serialize(model.EarlyMorningSnack ?? new List<string>());
                    existing.Exercise = JsonSerializer.Serialize(model.Exercise ?? new List<string>());
                    existing.Breakfast = JsonSerializer.Serialize(model.Breakfast ?? new List<string>());
                    existing.MidMorningSnack = JsonSerializer.Serialize(model.MidMorningSnack ?? new List<string>());
                    existing.Lunch = JsonSerializer.Serialize(model.Lunch ?? new List<string>());
                    existing.EveningSnack = JsonSerializer.Serialize(model.EveningSnack ?? new List<string>());
                    existing.Dinner = JsonSerializer.Serialize(model.Dinner ?? new List<string>());
                    existing.Bedtime = JsonSerializer.Serialize(model.Bedtime ?? new List<string>());
                }
                else
                {
                    _context.DietPlans.Add(new DietPlan
                    {
                        Name = model.Name,
                        DayName = dayname, // store one day per row
                        EmptyStomach = JsonSerializer.Serialize(model.EmptyStomach ?? new List<string>()),
                        EarlyMorningSnack = JsonSerializer.Serialize(model.EarlyMorningSnack ?? new List<string>()),
                        Exercise = JsonSerializer.Serialize(model.Exercise ?? new List<string>()),
                        Breakfast = JsonSerializer.Serialize(model.Breakfast ?? new List<string>()),
                        MidMorningSnack = JsonSerializer.Serialize(model.MidMorningSnack ?? new List<string>()),
                        Lunch = JsonSerializer.Serialize(model.Lunch ?? new List<string>()),
                        EveningSnack = JsonSerializer.Serialize(model.EveningSnack ?? new List<string>()),
                        Dinner = JsonSerializer.Serialize(model.Dinner ?? new List<string>()),
                        Bedtime = JsonSerializer.Serialize(model.Bedtime ?? new List<string>()),
                        IsActive = true,
                        IsDeleted = false
                    });
                }
            }

            _context.SaveChanges();
            return Json(new { success = true });
        }

        public IActionResult PlanSchedule()
        {
            var plans = _context.DietPlans
                                .Where(d => !d.IsDeleted && d.IsActive)
                                .OrderBy(d => d.Name)
                                .ToList();

            return View(plans);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Toggle(int id)
        {
            var plans = _context.DietPlans.Find(id);
            if (plans == null)
            {
                return NotFound();
            }
            plans.IsActive = !plans.IsActive;
            _context.SaveChanges();
            TempData["Success"] = $"Category '{plans.Name}' {(plans.IsActive ? "deactivated" : "activated")} successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var plan = _context.DietPlans.Find(id);
            if (plan == null)
                return RedirectToAction("Index");

            _context.DietPlans.Remove(plan);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
