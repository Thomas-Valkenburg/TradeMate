using BLL.Models;
using DAL_Factory;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers;

public class HomeController : BaseController
{
    public ActionResult Index(int? inventory = null)
    {
        var customer = Customer.TryGetCustomer(int.Parse(HttpContext.Session.GetString("CustomerId")!), Program.ServiceType).Value!;

        ViewData["Inventory"] = inventory;

        return View(new InventoriesViewModel(customer.GetInventories()));
    }
}