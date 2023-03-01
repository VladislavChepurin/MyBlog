using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Data.Repositiry;
using MyBlog.Data.Repositiry.Repository;
using MyBlog.Data.UoW;
using MyBlog.Extentions;
using MyBlog.Models;
using MyBlog.Models.Users;
using MyBlog.ViewModels.Users;

namespace MyBlog.Controllers.Account;

public class RegisterUserController : Controller
{
    private readonly ILogger<RegisterUserController> _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserController(ILogger<RegisterUserController> logger, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, IRoleRepository roleRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
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
                await _roleRepository.CreateInitRoles();

                // идет на заглушку----------------------------------------------------------
                await _roleRepository.AssignRoles(user, model.CodeRegister ?? String.Empty);
                // идет на заглушку----------------------------------------------------------

                var repository = _unitOfWork.GetRepository<Invate>() as InviteRepository;
                var invite = repository?.GetInvite(model.CodeRegister.ConvertMD5());
                if (invite != null)
                {
                    if ((await _userManager.AddToRoleAsync(user, invite.Roles)).Succeeded)
                    {
                        _logger.LogInformation($"Пользователю {user.UserName} присвоена роль {invite.Roles}");
                    }
                    repository?.ChangeStatusInvite(invite);
                    _unitOfWork.SaveChanges();
                }
                //----------------------------------Раскоментировать--------------------
                //else
                //{
                //    if ((await _userManager.AddToRoleAsync(user, "User")).Succeeded)
                //    {
                //        _logger.LogInformation($"Пользователю {user.UserName} присвоена роль User");
                //    }
                //}
                //----------------------------------------------------------------------
                await _signInManager.SignInAsync(user, false);
                _logger.LogInformation($"Зарегистрирован новый пользователь {user.UserName} ** {user.Email}");
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