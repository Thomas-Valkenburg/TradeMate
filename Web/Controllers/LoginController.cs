using BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class LoginController : BaseController
{
    public ActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public ActionResult LoginPost(string username, string password)
    {
	    var result = Account.TryLogin(username, password);

	    if (!result.Success) ViewData["LoginError"] = result.ErrorMessage;
        
        

		return RedirectToAction("Index");
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
}