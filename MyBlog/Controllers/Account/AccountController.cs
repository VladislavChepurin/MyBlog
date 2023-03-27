using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models.Users;
using MyBlog.Services.ControllerServices.Interface;
using MyBlog.ViewModels.Users;

namespace MyBlog.Controllers.Account;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IAccountService _accountService;

    public AccountController(UserManager<User> userManager, IAccountService accountService)
    {
        _userManager = userManager;
        _accountService = accountService;
    }

    public IActionResult AllUsers() => View(_userManager.Users.ToList());
    public IActionResult AccessDenied() => View();
    public IActionResult Index() => View();

    [Authorize]
    [HttpGet]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> UserPage()
    {
        var view = await _accountService.GetUserPageModel();
        return View(view);
    }

    [HttpPost]
    [Route("Login")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LoginAsync(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var succeededLogin = await _accountService.IsLoggedIn(model);

            if (succeededLogin)
            {
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Article");
                }
            }
            else
            {
                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            }
        }
        return View("~/Views/Home/Index.cshtml", new LoginViewModel());
    }

    [Route("Logout")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _accountService.Logout();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Route("Login")]
    public IActionResult Login() => View("Login");

}
