using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class LoginController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
