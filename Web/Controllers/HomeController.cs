using System.Diagnostics;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
    public ILogger<HomeController> Logger { get; } = logger;

    public IActionResult Index()
    {
        _ = Inventory.GetAllInventories();
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}