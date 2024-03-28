using System.Diagnostics;
<<<<<<<< HEAD:Web/Controllers/HomeController.cs
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers;
========
using Microsoft.AspNetCore.Mvc;
using TradeMate_App.Models;
using TradeMate_Business;

namespace TradeMate_App.Controllers;
>>>>>>>> 18a6114e3b9a14f2fd25ae9bde0ca0f20161e8ef:TradeMate App/Controllers/HomeController.cs

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