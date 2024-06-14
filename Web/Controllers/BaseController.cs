using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;
using BLL.Models;
using Web.Models;

namespace Web.Controllers;

public abstract class BaseController(ILogger<BaseController> logger) : Controller
{
    public ILogger<BaseController> Logger { get; } = logger;

    public override void OnActionExecuting(ActionExecutingContext context)
    {
	    if (User.Identity is { Name: not null, IsAuthenticated: true })
	    {
		    var result = Account.TryGetAccount(User.Identity.Name);

            if (result is { Success: true, Value: not null })
            {
	            HttpContext.Session.SetString("AccountId", result.Value.AccountId.ToString());
	            HttpContext.Session.SetString("AccountUsername", result.Value.Username);
	            HttpContext.Session.SetString("AccountEmail", result.Value.Email);
	            HttpContext.Session.SetString("CustomerId", result.Value.CustomerId.ToString());
            }
		}

        if (Theme.GetActiveTheme(HttpContext) is null)
            HttpContext.Session.SetString("data-bs-theme", nameof(Theme.Value.Auto));

        base.OnActionExecuting(context);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public ActionResult Error()
    {
	    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}