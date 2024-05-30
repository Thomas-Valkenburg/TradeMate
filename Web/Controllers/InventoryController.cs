using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Authorize]
public class InventoryController(ILogger<InventoryController> logger) : BaseController(logger)
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