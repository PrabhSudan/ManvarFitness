using Microsoft.AspNetCore.Mvc;

namespace ManvarFitness.Controllers
{
    public class HerbsController : Controller
    {
        private static List<dynamic> HerbsList = new List<dynamic>
        {
            new { Name = "Ashwagandha", Benefits = "Reduces stress, improves concentration", Usage = "Capsules, Powder" },
            new { Name = "Turmeric", Benefits = "Anti-inflammatory, boosts immunity", Usage = "Capsules, Tea" },
            new { Name = "Tulsi", Benefits = "Respiratory health, reduces anxiety", Usage = "Tea, Capsules" }
        };
        public IActionResult Index()
        {
            ViewBag.Herbs = HerbsList;
            return View();
        }

        [HttpGet]
        public IActionResult Search(string herbName)
        {
            var results = HerbsList
               .Where(h => h.Name.Contains(herbName ?? "", System.StringComparison.OrdinalIgnoreCase))
               .ToList();

            ViewBag.SearchResults = results;
            return View("Index");
        }

        [HttpGet]
        public IActionResult AddHerb()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddHerb(string Name, string Benefits, string Usage)
        {
            if (string.IsNullOrWhiteSpace(Name) ||
                string.IsNullOrWhiteSpace(Benefits) ||
                string.IsNullOrWhiteSpace(Usage))
            {
                TempData["Error"] = "All fields are required.";
                return RedirectToAction("Index");
            }
            HerbsList.Add(new { Name, Benefits, Usage });
            TempData["Success"] = $"Herb '{Name}' added successfully!";
            return RedirectToAction("Index");
        }
    }
}
