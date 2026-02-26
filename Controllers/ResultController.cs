using ManvarFitness.Database;
using ManvarFitness.Entity;
using ManvarFitness.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ManvarFitness.Controllers
{
    public class ResultController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ResultController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: ResultController
        public IActionResult Index()
        {
            var results = _context.Results
                .Include(s => s.User)
                .Include(s => s.ConcernCategory)
                .Include(s => s.SubConcern)
                .ToList();
            return View(results);
        }

        // GET: ResultController/Create
        public IActionResult Create()
        {
            ViewBag.SubConcerns = new SelectList(
                _context.SubConcerns
                    .Include(s => s.Concern)
                    .ToList(),
                "SubConcernId",
                "Name"
            );
            ViewBag.Users = _context.Users.Select(u => new
            {
                u.UserId,
                Display = u.CountryCode + " " + u.Mobile
            }).ToList();

            ViewBag.Users = new SelectList(ViewBag.Users, "UserId", "Display");
            return View();
        }

        // POST: ResultController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ResultModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/results");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var beforeList = SaveFiles(model.BeforeImageFile, uploadPath);
            var afterList = SaveFiles(model.AfterImageFile, uploadPath);
            var videoList = SaveFiles(model.Videos, uploadPath);

            //  Get SubConcern
            var subConcern = _context.SubConcerns.FirstOrDefault(x => x.SubConcernId == model.SubConcernId);
            if (subConcern == null)
                return NotFound();

            // Auto get Concern from SubConcern
            int concernId = subConcern.ConcernId;

            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                TempData["Error"] = "Session expired. Please login again.";
                return RedirectToAction("Login", "Auth"); 
            }

            var entity = new ResultEntity
            {
                UserId = model.UserId,
                ConcernCategoryId = concernId,
                SubConcernId = model.SubConcernId,
                Description = model.Description,
                IsActive = true,
                BeforeImage = JsonSerializer.Serialize(beforeList),
                AfterImage = JsonSerializer.Serialize(afterList),
                Video = JsonSerializer.Serialize(videoList)
            };

            _context.Results.Add(entity);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: ResultController/Edit/5
        public IActionResult Edit(int id)
        {
            var result = _context.Results
                   .Include(r => r.User)
                   .Include(r => r.SubConcern)
                   .Include(r => r.ConcernCategory)
                   .FirstOrDefault(r => r.ResultId == id);

            if (result == null) return NotFound();

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ResultModel model)
        {
            var result = _context.Results.Find(id);
            if (result == null)
                return NotFound();

            string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/results");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            // BEFORE IMAGES
            var existingBefore = string.IsNullOrEmpty(result.BeforeImage) ? new List<string>()
                : System.Text.Json.JsonSerializer.Deserialize<List<string>>(result.BeforeImage);
            if (model.BeforeImageFile != null && model.BeforeImageFile.Count > 0)
            {
                var newbefore = SaveFiles(model.BeforeImageFile, uploadPath);
                foreach (var img in newbefore)
                {
                    if (!existingBefore.Contains(img))
                        existingBefore.Add(img);
                }
                result.BeforeImage = System.Text.Json.JsonSerializer.Serialize(existingBefore);
            }

            // AFTER IMAGES
            var existingAfter = string.IsNullOrEmpty(result.AfterImage) ? new List<string>()
               : System.Text.Json.JsonSerializer.Deserialize<List<string>>(result.AfterImage);
            if (model.AfterImageFile != null && model.AfterImageFile.Count > 0)
            {
                var newAfter = SaveFiles(model.AfterImageFile, uploadPath);
                foreach (var img in newAfter)
                {
                    if (!existingAfter.Contains(img))
                        existingAfter.Add(img);
                }
                result.BeforeImage = System.Text.Json.JsonSerializer.Serialize(existingAfter);
            }

            _context.SaveChanges();

            TempData["Success"] = "Files updated successfully!";
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Toggle(int id)
        {
            var result = _context.Results.Find(id);
            if (result == null)
            {
                return NotFound();
            }

            result.IsActive = !result.IsActive;
            _context.SaveChanges();

            TempData["Success"] =
                $"Result {(result.IsActive ? "activated" : "deactivated")} successfully!";

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SoftDelete(int id)
        {
            var role = _context.Roles.Find(id);
            if (role == null)
            {
                return Json(new { success = false });
            }
            role.IsDeleted = true;

            _context.SaveChanges();
            return Json(new { success = true });
        }

        // Save File Method
        private List<string> SaveFiles(List<IFormFile>? files, string uploadPath)
        {
            var savedFiles = new List<string>();

            if (files == null || files.Count == 0)
                return savedFiles;

            foreach (var file in files)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(uploadPath, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                savedFiles.Add("/uploads/results/" + fileName);
            }

            return savedFiles;
        }
    }
}
