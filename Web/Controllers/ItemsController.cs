using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class ItemsController : BaseController
{
	public ActionResult Index(int itemId)
	{
		return View();
	}

	[HttpPost]
	public ActionResult Delete(int itemId)
	{
		return RedirectToAction("Index", "Home");
	}
}