using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WP.Models;

namespace WP.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        List<string> subMenu = new List<string>();
        subMenu.Add("SubMenu1");
        subMenu.Add("SubMenu2");
        subMenu.Add("SubMenu3");
        return View(subMenu);
    }

    public IActionResult Index2()
    {

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

