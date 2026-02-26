using ManvarFitness.Database;
using ManvarFitness.Entity;
using ManvarFitness.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace ManvarFitness.Controllers
{
    public class RoleMenuController : BaseController
    {
        private readonly ApplicationDbContext _context;
        public RoleMenuController(ApplicationDbContext context)
        {
            _context = context;
        }
        // Secure entire controller (Admin only)
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
        public IActionResult Index()
        {
            var roles = _context.Roles.Select(r => r.Name).ToList();
            ViewBag.Roles = roles;
            return View();
        }

        [HttpPost]
        public IActionResult GetPages(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
                return PartialView("_PagesPartialView", new List<PageModel>());
            var pages = _context.Pages.Where(p => p.ParentId == null).Select(p => new PageModel
            {
                Id = p.Id,
                Name = p.Name,
                SubPages = _context.Pages.Where(sp => sp.ParentId == p.Id).Select(sp => new PageModel
                {
                    Id = sp.Id,
                    Name = sp.Name
                }).ToList()
            }).ToList();

            List<int> assignedIds;

            if (roleName == "Admin")
            {
                assignedIds = _context.Pages.Select(p => p.Id).ToList();
            }
            else
            {
                assignedIds = _context.RolePages
                    .Where(rp => rp.RoleName == roleName && rp.IsActive)
                    .Select(rp => rp.PageId)
                    .ToList();
            }

            ViewBag.AssignedIds = assignedIds;
            ViewBag.SelectedRole = roleName;
            return PartialView("_PagesPartialView", pages);
        }

        [HttpPost]
        public IActionResult SaveRolePages(string roleName, List<int> selectedPageIds)
        {
            if (string.IsNullOrEmpty(roleName))
                return RedirectToAction("Index");

            if(roleName =="Admin")
            {
                return RedirectToAction("Index");
            }

            // Remove old
            var old = _context.RolePages
                              .Where(x => x.RoleName == roleName)
                              .ToList();

            _context.RolePages.RemoveRange(old);
            _context.SaveChanges();

            if (selectedPageIds != null && selectedPageIds.Any())
            {
                foreach (var id in selectedPageIds)
                {
                    _context.RolePages.Add(new RolePage
                    {
                        RoleName = roleName,
                        PageId = id,
                        IsActive = true   // 🔥 VERY IMPORTANT
                    });
                }

                _context.SaveChanges();
            }

            return RedirectToAction("Index", new { roleName = roleName });
        }

    }
}


