using ManvarFitness.Database;
using ManvarFitness.Entity;
using ManvarFitness.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace ManvarFitness.Controllers
{
    public class AdminUserController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public AdminUserController(ApplicationDbContext context)
        {
            _context = context;
        }

        //  Automatically secure all actions
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var loginCheck = RequireLogin();
            if (loginCheck != null)
            {
                context.Result = loginCheck;
                return;
            }
            var roleCheck = AuthorizeRole("Admin");
            if (roleCheck != null)
            {
                context.Result = roleCheck;
                return;
            }
            base.OnActionExecuting(context);
        }
        // GET: AdminUserController
        public async Task<IActionResult> Index()
        {
            var users = await _context.AdminUsers
                .Where(u => !u.IsDeleted)
                .ToListAsync();
            return View(users);
        }

        // GET: UserController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var user = await _context.AdminUsers.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminUser model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var exists = await _context.AdminUsers.AnyAsync(u => u.EmailUsername == model.EmailUsername);
            if (exists)
            {
                ModelState.AddModelError("EmailUsername", "Email or username already exists.");
                return View(model);
            }
            // On create
            model.CreatedOn = DateTime.UtcNow;
            model.CreatedBy = CurrentUserId;
            model.IsDeleted = false;
            _context.AdminUsers.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: UserController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _context.AdminUsers.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AdminUser model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _context.AdminUsers.FindAsync(id);
            if (user == null)
            {
                return BadRequest();
            }
            user.EmailUsername = model.EmailUsername;
            user.Password = model.Password;
            user.CountryCode = model.CountryCode;
            user.Mobile = model.Mobile;
            // On edit 
            user.UpdatedOn = DateTime.UtcNow;
            user.UpdatedBy = CurrentUserId;

            _context.AdminUsers.Update(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: UserController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _context.AdminUsers.FindAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            var user = await _context.AdminUsers.FindAsync(id);
            if (user == null) return NotFound();

            // Soft delete
            user.IsDeleted = true;
            user.UpdatedOn = DateTime.UtcNow;
            user.UpdatedBy = 1;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "AdminUser");
        }

        [HttpPost]
        public async Task<IActionResult> ToogleActive(int id)
        {
            var user = await _context.AdminUsers.FindAsync(id);
            if (user == null) return NotFound();

            user.IsActive = !user.IsActive;
            user.UpdatedOn = DateTime.UtcNow;
            user.UpdatedBy = 1;

            await _context.SaveChangesAsync();

            return Json(new { success = true, isActive = user.IsActive });
        }

        [HttpPost]
        public IActionResult UpdateRole(int id, [FromBody] RoleUpdateModel model)
        {
            var user = _context.AdminUsers.Find(id);

            if (user == null)
                return Json(new { success = false });

            user.Role = model.Role;
            _context.SaveChanges();

            return Json(new { success = true });
        }
    }
}

