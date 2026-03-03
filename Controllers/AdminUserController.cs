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

            var roles = await _context.Roles
                .Select(r => r.Name) 
                .ToListAsync();

            ViewBag.Roles = roles;

            return View(users);
        }

        // GET: UserController/Details/5
        public async Task<IActionResult> Details(Guid id)
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
            var exists = await _context.AdminUsers.AnyAsync(u => u.Name == model.Name);
            if (exists)
            {
                ModelState.AddModelError("Name", "Name already exists.");
                return View(model);
            }
            // On create
            model.CreatedOn = DateTime.UtcNow;
            model.CreatedBy = null;
            model.IsDeleted = false;
            _context.AdminUsers.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: UserController/Edit/5
        public async Task<IActionResult> Edit(Guid id)
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
        public async Task<IActionResult> Edit(Guid id, AdminUser model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }
            else if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _context.AdminUsers.FindAsync(id);
            if (user == null)
            {
                return BadRequest();
            }
            user.Name = model.Name;
            user.Email = model.Email;
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
        public async Task<ActionResult> Delete(Guid id)
        {
            var user = await _context.AdminUsers.FindAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id, IFormCollection collection)
        {
            var user = await _context.AdminUsers.FindAsync(id);
            if (user == null) return NotFound();

            // Soft delete
            user.IsDeleted = true;
            user.UpdatedOn = DateTime.UtcNow;
            user.UpdatedBy = CurrentUserId;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "AdminUser");
        }

        [HttpPost]
        public async Task<IActionResult> ToogleActive(Guid id)
        {
            var user = await _context.AdminUsers.FindAsync(id);
            if (user == null) return NotFound();

            user.IsActive = !user.IsActive;
            user.UpdatedOn = DateTime.UtcNow;
            user.UpdatedBy = CurrentUserId;

            await _context.SaveChangesAsync();

            return Json(new { success = true, isActive = user.IsActive });
        }

        [HttpPost]
        public IActionResult UpdateRole([FromBody] RoleUpdateModel model)
        {
            var user = _context.AdminUsers.Find(model.Id);

            if (user == null)
                return Json(new { success = false });

            user.Role = model.Role;
            user.UpdatedOn = DateTime.UtcNow;
            user.UpdatedBy = CurrentUserId;

            _context.SaveChanges();

            return Json(new { success = true });
        }
    }
}

