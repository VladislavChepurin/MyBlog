using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models.Users;
using MyBlog.ViewModels.Users;
using NLog;
using NLog.Web;

namespace MyBlog.Controllers.Account;

public class RegisterUserController : Controller
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly Logger _logger;

    public RegisterUserController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
    }

    [Route("Register")]
    [HttpGet]
    public IActionResult Register()
    {
        return View("RegisterUser");
    }

    [Route("Register")]
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = _mapper.Map<User>(model);
            var result = await _userManager.CreateAsync(user, model.PasswordReg!);
            if (result.Succeeded)
            {
                _logger.Info("Создан новый пользователь {Email}", user?.Email);
                if ((await _userManager.AddToRoleAsync(user!, "User")).Succeeded)
                {
                    _logger.Info("Пользователю {Email} присвоена роль User", user?.Email);
                }
                await _signInManager.SignInAsync(user!, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }
        return View("RegisterUser", model);
    }
}