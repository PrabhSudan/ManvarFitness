using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManvarFitness.Controllers
{
    public class DietController : Controller
    {
        private static List<dynamic> plans = new List<dynamic>
        {
            new { Name ="Plan A", Breakfast = "Oats", Lunch = "Lentils", Snacks = "Fruits", Dinner = "Salad" },
            new { Name = "Plan B", Breakfast = "Eggs", Lunch = "Vegetables", Snacks = "Handful of nuts", Dinner = "Salad" },
            new { Name = "Plan C", Breakfast = "Greek yogurt", Lunch = "Lentils", Snacks = "Roasted chickpeas", Dinner = "Tofu" }
        };

        public IActionResult Index()
        {
            ViewBag.Diet = plans;
            return View();
        }

        [HttpGet]
        public IActionResult Search(string planName)
        {
            var results = plans
               .Where(h => h.Name.Contains(planName ?? "", System.StringComparison.OrdinalIgnoreCase))
               .ToList();

            ViewBag.SearchResults = results;
            return View("Index");
        }

        [HttpGet]
        public IActionResult AddPlan()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPlan(string Name, string Breakfast, string Lunch, string Snacks, string Dinner)
        {
            if (string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(Breakfast) ||
                string.IsNullOrWhiteSpace(Lunch) ||
                string.IsNullOrWhiteSpace(Snacks) ||
                string.IsNullOrWhiteSpace(Dinner))
            {
                TempData["Error"] = "All fields are required.";
                return RedirectToAction("Index");
            }
            plans.Add(new { Name, Breakfast, Lunch, Snacks , Dinner });
            TempData["Success"] = $"Plan '{Name}' added successfully!";
            return RedirectToAction("Index");
        }
    }
}
