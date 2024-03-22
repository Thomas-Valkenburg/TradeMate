using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TradeMate_App.Models;
using TradeMate_Business;

namespace TradeMate_App.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
    public ILogger<HomeController> Logger { get; } = logger;

    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}