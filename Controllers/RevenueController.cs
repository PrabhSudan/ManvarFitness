using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ManvarFitness.Controllers
{
    public class RevenueController : BaseController
    {
        // 🔒 Secure entire controller (Admin only)
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
            return View();
        }
    }
}