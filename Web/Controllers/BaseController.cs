using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (HttpContext.Session.TryGetValue("username", out _) && context.Controller.GetType() == typeof(LoginController))
            {
                context.Result = Redirect("Home");
            }
            else if (context.Controller.GetType() != typeof(LoginController))
            {
                context.Result = Redirect("Login");
            }

            base.OnActionExecuting(context);
        }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();

        return RedirectToAction("Index", "Home");
    }
}