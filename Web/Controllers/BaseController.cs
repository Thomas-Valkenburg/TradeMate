using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using Web.Models;

namespace Web.Controllers;

public abstract class BaseController : Controller
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var customerId = HttpContext.Session.GetString("CustomerId");

        if (string.IsNullOrWhiteSpace(customerId) && context.Controller.GetType() != typeof(AccountController))
        {
            context.Result = RedirectToAction("Index", "Account");
        }

        if (Theme.GetActiveTheme(HttpContext) is null)
            HttpContext.Session.SetString("data-bs-theme", nameof(Theme.Value.auto));

        base.OnActionExecuting(context);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public ActionResult Error()
    {
	    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}