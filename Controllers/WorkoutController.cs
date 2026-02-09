using Microsoft.AspNetCore.Mvc;

namespace ManvarFitness.Controllers
{
    public class WorkoutController : Controller
    {
        private readonly string uploadfolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
        
        public WorkoutController()
        {
            if(!Directory.Exists(uploadfolder))
                Directory.CreateDirectory(uploadfolder);
        }
        public IActionResult Videos()
        {
            var Videos = new List<dynamic>
            {
                //new { Title = "Full Body Workout",Description="Descrip1", Difficulty="Easy",Status="Active", FileName=""},
                //new { Title = "Yoga for Beginners", Description="Descrip2", Difficulty="Medium",Status="Active", FileName="" },
                //new { Title = "HIIT Session", Description="Descrip3", Difficulty="Hard",Status="Active", FileName="" }
            };
            var uploadedFiles = Directory.GetFiles(uploadfolder)
                .Select(f => Path.GetFileName(f))
                .Select(f => new
                {
                    Title = Path.GetFileNameWithoutExtension(f),
                    Description = "Uploaded Video",
                    Difficulty = "Custom",
                    Status = "Active",
                    FileName = f
                });
            Videos.AddRange(uploadedFiles);

            ViewBag.Videos = Videos;
            return View();
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upload(string Title, string Description, string Difficulty, IFormFile videofile)
        {
            if(string.IsNullOrEmpty(Title) || videofile==null)
            {
                ViewBag.Error = "Title and Video file are required";
                return View();
            }

            var filePath = Path.Combine(uploadfolder, videofile.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                videofile.CopyTo(stream);
            }

            TempData["Success"] = "Video uploaded successfully";
            return RedirectToAction("Videos");
        }
    }
}
