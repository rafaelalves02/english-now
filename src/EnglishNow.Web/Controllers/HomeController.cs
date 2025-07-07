using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EnglishNow.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace EnglishNow.Web.Controllers;

[Authorize]

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}