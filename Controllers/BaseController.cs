using ManvarFitness.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ManvarFitness.Controllers
{
    public class BaseController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BaseController (ApplicationDbContext context)
        {
            _context = context;
        }
        protected string CurrentUserRole => HttpContext.Session.GetString("UserRole") ?? "";
        protected string CurrentName => HttpContext.Session.GetString("Name") ?? "User";
        protected Guid CurrentUserId
        {
            get
            {
                var value = HttpContext.Session.GetString("UserId");
                return Guid.TryParse(value, out var id) ? id : Guid.Empty;
            }
        }

        // Allow only specific roles
        protected IActionResult? AuthorizeRole(params string[] allowedRoles)
        {
            if (string.IsNullOrEmpty(CurrentUserRole) || !allowedRoles.Contains(CurrentUserRole))
                return RedirectToAction("AccessDenied", "Home");

            return null;
        }

        public override void OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context)
        {
            // Check Login
            if(string.IsNullOrEmpty(CurrentUserRole))
            {
                context.Result = RedirectToAction("Login", "Auth");
                return;
            }

            // Load allowed pages for this role
            var allowedPageUrls = _context.RolePages
                .Where(rp => rp.RoleName == CurrentUserRole && rp.IsActive)
                .Join(_context.Pages,
                    rp => rp.PageId,
                    p => p.Id,
                    (rp, p) => p.Url)
                .ToList();

            // Make role and username available in all views
            ViewBag.AllowedPageUrls = allowedPageUrls;
            ViewBag.CurrentUserRole = CurrentUserRole;
            ViewBag.CurrentName = CurrentName;

            // Protect direct URL access
            var currentPath = HttpContext.Request.Path.ToString();
            bool hasAccess = allowedPageUrls.Any(url => currentPath.StartsWith(url, StringComparison.OrdinalIgnoreCase))
                 || CurrentUserRole.Equals("Admin", StringComparison.OrdinalIgnoreCase);

            if (!hasAccess)
            {
                context.Result = Content("You do not have permission to access this page.");
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}
