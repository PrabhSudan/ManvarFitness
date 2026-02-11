using ManvarFitness.Database;
using ManvarFitness.Entity;
using ManvarFitness.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManvarFitness.Controllers
{

    public class WorkoutController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly string uploadfolder;

        public WorkoutController(ApplicationDbContext context)
        {
            _context = context;
            uploadfolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

            if (!Directory.Exists(uploadfolder))
                Directory.CreateDirectory(uploadfolder);
        }
        public IActionResult Videos()
        {
            var videos = _context.WorkoutVideos
               .Where(v => !v.IsDeleted)
               .OrderByDescending(v => v.Title)
               .ToList();

            return View(videos);
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upload(WorkoutVideoModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.VideoFile == null && string.IsNullOrWhiteSpace(model.VideoUrl))
            {
                ModelState.AddModelError("", "Please upload a video or provide a video URL");
                return View(model);
            }

            if (model.VideoFile != null)
            {
                ;
                var fileName = Guid.NewGuid() + Path.GetExtension(model.VideoFile.FileName);
                var filePath = Path.Combine(uploadfolder, fileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                model.VideoFile.CopyTo(stream);

                model.VideoUrl = "/uploads/" + fileName;
            }

            var entity = new WorkoutVideo
            {
                Title = model.Title,
                Description = model.Description,
                Difficulty = model.Difficulty,
                IsActive = model.IsActive,
                VideoUrl = model.VideoUrl,
                CreatedOn = DateTime.UtcNow
            };
            _context.WorkoutVideos.Add(entity);
            _context.SaveChanges();
            TempData["Success"] = "Video uploaded successfully";
            return RedirectToAction("Videos", "Workout");
        }

        [HttpPost]
        public IActionResult ToggleStatus(int id, [FromBody] WorkoutVideoModel model)
        {
            var video = _context.WorkoutVideos.Find(id);
            if (video == null)
                return Json(new { success = false });

            video.IsActive = model.IsActive;
            _context.SaveChanges();

            return Json(new { success = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SoftDelete(int id)
        {
            var video = _context.WorkoutVideos.Find(id);
            if(video == null)
            {
                return Json(new { success = false });
            }
            video.IsDeleted = true;
            _context.SaveChanges();
            return Json(new { success = true });
        }
    }
}
