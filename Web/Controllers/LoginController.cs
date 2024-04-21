using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class LoginController : BaseController
{
    public ActionResult Index()
    {
        return View();
    }
}