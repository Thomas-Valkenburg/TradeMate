using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers;

[Authorize]
public class HomeController(ILogger<HomeController> logger) : BaseController(logger)
{
    [HttpGet]
    public ActionResult Index(int? inventory = null)
    {
        var customer = Customer.TryGetCustomer(int.Parse(HttpContext.Session.GetString("CustomerId")!), Program.ServiceType).Value!;

        ViewData["Inventory"] = inventory;

        return View(new InventoriesViewModel(customer.GetInventories()));
    }

    [HttpGet]
    public ActionResult ChangeTheme(string redirectPage, Theme.Value theme)
    {
        Theme.SetActive(theme, HttpContext);

        return Redirect(redirectPage);
    }
}