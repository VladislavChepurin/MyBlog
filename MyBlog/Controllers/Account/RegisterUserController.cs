using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models.Users;
using MyBlog.ViewModels.Users;

namespace MyBlog.Controllers.Account;

public class RegisterUserController : Controller
{
    private readonly ILogger<RegisterUserController> _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public RegisterUserController(ILogger<RegisterUserController> logger, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
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
            var result = await _userManager.CreateAsync(user, model.PasswordReg);
            if (result.Succeeded)
            {
                if ((await _userManager.AddToRoleAsync(user, "User")).Succeeded)
                {
                    _logger.LogError(0, "Пользователю {Name} присвоена роль User", user.UserName);
                }
                await _signInManager.SignInAsync(user, false);
                _logger.LogError(0, "Зарегистрирован новый пользователь {Name} ** {Email}", user.UserName, user.Email);
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