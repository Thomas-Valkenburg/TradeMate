using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Authorize]
public class InventoryController(ILogger<InventoryController> logger) : BaseController(logger)
{
	[HttpGet]
	public ActionResult Index(int inventoryId)
	{
		return View(Inventory.TryGetInventory(inventoryId, Program.ServiceType).Value);
	}

	[HttpGet]
    public ActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Add(string name)
    {
        var customer = Customer.TryGetCustomer(int.Parse(HttpContext.Session.GetString("CustomerId")!), Program.ServiceType).Value;
		if (customer is null) return RedirectToAction("Index", "Home");

        var result = customer.AddInventory(name);

        if (result.Success) return RedirectToAction("Index", "Home");

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public ActionResult Update(int inventoryId, string name)
	{
		var inventory = Inventory.TryGetInventory(inventoryId, Program.ServiceType).Value;
		if (inventory is null) return RedirectToAction("Index", "Home");

		var result = inventory.ChangeName(name);

		if (result.Success) return RedirectToAction("Index", "Home", routeValues: $"inventory: {inventory.Id}");

		return RedirectToAction("Index", "Inventory");
	}

    [HttpPost]
    public ActionResult Delete(int inventoryId)
	{
		var inventory = Inventory.TryGetInventory(inventoryId, Program.ServiceType).Value;

		var result = inventory?.Delete();

		return RedirectToAction("Index", "Home");
	}
}