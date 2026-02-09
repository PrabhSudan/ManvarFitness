using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ManvarFitness.Controllers
{
    public class ResultController : Controller
    {
        private static List<dynamic> Results = new()
        {
            new { Id = 1, Name = "John Doe", Category = "Category1", Date = "2024-01-01", Result="null" },
            new { Id = 2, Name = "Jane Smith", Category = "Category2", Date = "2024-01-02", Result="null" },
            new { Id = 3, Name = "Jamie", Category = "Category3", Date = "2024-01-03", Result="null" }
        };

        // GET: ResultController
        public ActionResult Index()
        {
            return View(Results);
        }

        // GET: ResultController/Details/5
        public ActionResult Details(int id)
        {
            var result = Results.FirstOrDefault(u => u.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

        // GET: ResultController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ResultController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string Name, string Category, string Date, IFormFile? Result)
        {
            string?filePath = null;
            if (Result != null && Result.Length > 0)
            {
                // Folder to save files
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\results");
                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                // Save file
                var fileName = Path.GetFileName(Result.FileName);
                filePath = Path.Combine(uploads, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Result.CopyTo(fileStream);
                }
                // Store relative path for use in views
                filePath = "/results/" + fileName;
            }

            // Add new result to the static list
            int newId = Results.Max(u => u.Id) + 1;
            Results.Add(new { Id = newId, Name = Name, Category = Category, Date = Date, Result = filePath });

            return RedirectToAction("Index");
        }

        // GET: ResultController/Edit/5
        public ActionResult Edit(int id)
        {
            var result = Results.FirstOrDefault(u => u.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

        // POST: ResultController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string Date, IFormFile? Result)
        {
            // Find the specific result entry by Id
            var existing = Results.Find(u => u.Id == id);
            if (existing == null)
                return NotFound();

            string? filePath = existing.Result; // Keep old file if no new upload

            // If a new file is uploaded, save it
            if (Result != null && Result.Length > 0)
            {
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\results");
                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                var fileName = Path.GetFileName(Result.FileName);
                filePath = Path.Combine(uploads, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Result.CopyTo(fileStream);
                }

                // Store relative path for views
                filePath = "/results/" + fileName;
            }

            // Remove old entry and add updated one 
            Results.Remove(existing);
            Results.Add(new
            {
                Id = existing.Id,
                Name = existing.Name,           
                Category = existing.Category,   
                Date = Date,                    
                Result = filePath               
            });

            return RedirectToAction("Index");
        }


        // GET: ResultController/Delete/5
        public ActionResult Delete(int id)
        {
            var user = Results.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            return View(user);
        }

        // POST: ResultController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var user = Results.FirstOrDefault(u => u.Id == id);
            if (user != null) Results.Remove(user);
            return RedirectToAction("Index");
        }
    }
}
