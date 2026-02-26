using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ManvarFitness.Controllers
{
    public class BaseController : Controller
    {
        protected string CurrentUserRole => HttpContext.Session.GetString("UserRole") ?? "";
        protected string CurrentUserName => HttpContext.Session.GetString("UserName") ?? "User";
        protected int CurrentUserId => HttpContext.Session.GetInt32("UserId") ?? 0;
        // Ensure user is logged in
        protected IActionResult? RequireLogin()
        {
            if (string.IsNullOrEmpty(CurrentUserRole))
                return RedirectToAction("Login", "Auth");

            return null;
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
            // Make role and username available in all views
            ViewBag.CurrentUserRole = CurrentUserRole;
            ViewBag.CurrentUserName = CurrentUserName;
            base.OnActionExecuting(context);
        }
    }
}
