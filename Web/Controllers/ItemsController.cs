using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
	public class ItemsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
