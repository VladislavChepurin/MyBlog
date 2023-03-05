using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MyBlog.Models;
using MyBlog.ViewModels.Users;

namespace MyBlog.Controllers;

public class HomeController : Controller
{
    public HomeController()
    {
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("")]
    [Route("[controller]/[action]")]
    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }

    [HttpGet]
    [Route("[action]")]
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