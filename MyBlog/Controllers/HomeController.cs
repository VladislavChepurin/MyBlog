﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Contracts.Models;
using Contracts.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Contracts.Models.Users;

namespace MyBlog.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class HomeController : Controller
{
    private readonly SignInManager<User> _signInManager;

    public HomeController(SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
    }

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