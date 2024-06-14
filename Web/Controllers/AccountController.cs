using System.Security.Claims;
using BLL.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class AccountController(ILogger<AccountController> logger) : BaseController(logger)
{
	[AllowAnonymous]
	[HttpGet]
    public ActionResult Index()
    {
	    return RedirectToAction("Login");
    }

	[AllowAnonymous]
	[HttpGet]
    public ActionResult Login(string errorMessage)
    {
	    ViewData["LoginError"] = errorMessage;

	    return View();
	}

	[AllowAnonymous]
	[HttpPost]
	public async Task<ActionResult> Login(string username, string password)
    {
	    var result = Account.TryLogin(username, password);

		if (!result.Success || result.Value is null)
		{
			return RedirectToAction("Login", "Account", new RouteValueDictionary
			{
				{ "errorMessage", string.IsNullOrWhiteSpace(result.ErrorMessage) ? "Username or password incorrect" : result.ErrorMessage }
			});
		}

		await Authenticate(result.Value.Username);

		return RedirectToAction("Index", "Home");
    }

	[AllowAnonymous]
	[HttpGet]
    public ActionResult Signup()
    {
	    return View();
    }

	[AllowAnonymous]
    [HttpPost]
    public ActionResult Signup(string username, string email, string password)
    {
	    Account.CreateAccount(username, password, email);

	    return RedirectToAction("Index");
    }

	[Authorize]
    public ActionResult Logout()
    {
	    HttpContext.Session.Clear();
		HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

	    return RedirectToAction("Login", "Account");
    }

	private async Task Authenticate(string username)
	{
		var claims = new List<Claim>
		{
			new(ClaimsIdentity.DefaultNameClaimType, username),
			new(ClaimsIdentity.DefaultRoleClaimType, "user")
		};

		var identity = new ClaimsIdentity(claims, "ApplicationCookie");

		await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
	}
}