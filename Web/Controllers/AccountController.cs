using BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class AccountController(ILogger<AccountController> logger) : BaseController(logger)
{
    public ActionResult Index()
    {
	    return RedirectToAction("Login");
    }

	
    public ActionResult Login(string? errorMessage = null)
    {
	    ViewData["LoginError"] = errorMessage;

	    return View();
	}

    [HttpPost]
    public ActionResult LoginPost(string username, string password)
    {
	    var result = Account.TryLogin(username, password);

		if (!result.Success || result.Value is null)
	    {
		    return RedirectToAction("Login", routeValues: new
		    {
                errorMessage = result.ErrorMessage
		    });
	    }

		HttpContext.Session.SetString("AccountId", result.Value.AccountId.ToString());
		HttpContext.Session.SetString("AccountUsername", result.Value.Username);
	    HttpContext.Session.SetString("AccountEmail", result.Value.Email);
		HttpContext.Session.SetString("CustomerId", result.Value.CustomerId.ToString());

		return RedirectToAction("Index", "Home");
    }

    public ActionResult Signup()
    {
	    return View();
    }

    [HttpPost]
    public ActionResult SignupPost(string username, string email, string password)
    {
	    Account.CreateAccount(username, password, email);

	    return RedirectToAction("Index");
    }

    public ActionResult Logout()
    {
	    HttpContext.Session.Clear();

	    return RedirectToAction("Index", "Home");
    }
}