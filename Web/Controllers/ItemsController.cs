using System.Globalization;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class ItemsController : BaseController
{
	public ActionResult Index(int itemId)
	{
		return View();
	}

	public ActionResult Add(int inventoryId)
	{
		var inventory = Inventory.TryGetInventory(inventoryId, Program.ServiceType);

		if (!inventory.Success || inventory.Value is null) return Error();

		(string name, int id) data = (inventory.Value.Name, inventory.Value.Id);

		return View(data);
	}

	[HttpPost]
	public ActionResult Add(int inventoryId, string name, string barcode, int amount, string price)
	{
		var inventory = Inventory.TryGetInventory(inventoryId, Program.ServiceType).Value;

		inventory?.AddStockItem(name, barcode, amount, decimal.Parse(price, CultureInfo.InvariantCulture));

		return RedirectToAction("Index", "Home", new { inventory = inventoryId });
	}

	[HttpPost]
	public ActionResult Delete(int itemId)
	{
		return RedirectToAction("Index", "Home");
	}
}