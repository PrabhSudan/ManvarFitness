using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManvarFitness.Filters
{
    public class SessionAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.RouteData.Values["controller"]?.ToString();
            // Allow all actions inside AuthController
            if (controller == "Auth")
            {
                base.OnActionExecuting(context);
                return;
            }
            var userId = context.HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}
