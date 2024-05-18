using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Web.Models;

namespace Web.Controllers;

public abstract class BaseController : Controller
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (HttpContext.Session.TryGetValue("CustomerId", out _))
        {
            if (context.Controller.GetType() == typeof(AccountController))
            {
                context.Result = RedirectToAction("Index", "Home");
            }
        }
        else if (context.Controller.GetType() != typeof(AccountController))
        {
            context.Result = RedirectToAction("Index", "Account");
        }

        base.OnActionExecuting(context);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public ActionResult Error()
    {
	    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}