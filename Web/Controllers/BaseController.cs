using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Controllers;

public abstract class BaseController : Controller
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (HttpContext.Session.TryGetValue("username", out _))
        {
            if (context.Controller.GetType() == typeof(LoginController))
            {
                context.Result = RedirectToAction("Index", "Home");
            }
        }
        else if (context.Controller.GetType() != typeof(LoginController))
        {
            context.Result = RedirectToAction("Index", "Login");
        }

        base.OnActionExecuting(context);
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();

        return RedirectToAction("Index", "Home");
    }
}