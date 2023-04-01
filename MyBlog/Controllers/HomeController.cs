using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MyBlog.Models;
using MyBlog.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using MyBlog.Models.Users;

namespace MyBlog.Controllers;

public class HomeController : Controller
{
    private readonly SignInManager<User> _signInManager;

    public HomeController(SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("")]
    [Route("[controller]/[action]")]
    public IActionResult Index()
    {
        if (_signInManager.IsSignedIn(User))
        {
            return RedirectToAction("Index", "Article");
        }
        return View(new LoginViewModel());
    }

    [ApiExplorerSettings(IgnoreApi = true)]
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