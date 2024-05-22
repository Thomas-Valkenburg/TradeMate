using BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class InventoryController : Controller
{
    [HttpGet]
    public ActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Add(string name)
    {
        var customer = Customer.TryGetCustomer(int.Parse(HttpContext.Session.GetString("CustomerId")!), Program.ServiceType).Value;

        var result = customer?.AddInventory(name);

        return RedirectToAction("Index", "Home", routeValues: $"result={result?.ErrorMessage}");
    }
}