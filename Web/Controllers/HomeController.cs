using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web.Core;
using Web.Models;

namespace Web.Controllers;

public class HomeController : Controller
{
    private readonly Service _service;
    private readonly ILogger<HomeController> _logger;

    public HomeController(Service service,ILogger<HomeController> logger)
    {
        _service = service;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> List()
    {
        var models = await _service.List();
        return View(models);
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