using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace eShopSolution.AdminApp.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session.GetString("Token");
            var controller = context.RouteData.Values["controller"]?.ToString();
            var action = context.RouteData.Values["action"]?.ToString();

            // allow access login or register without session
            if (controller == "User" && (action == "Login"))
            {
                base.OnActionExecuting(context);
                return;
            }

            // If don't have session -> redirect to Login
            if (string.IsNullOrEmpty(session))
            {
                context.Result = new RedirectToActionResult("Login", "User", null);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}