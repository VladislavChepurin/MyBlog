using Microsoft.AspNetCore.Identity;
using MyBlog.Data;
using MyBlog.Models.Users;
using MyBlog.Services.ContextServices.Interface;
using MyBlog.ViewModels.Users;
using NLog;
using NLog.Web;

namespace MyBlog.Services.ContextServices;

public class SingInResolverService : ISingInResolverService
{
    private readonly ApplicationDbContext _context;
    private readonly SignInManager<User> _signInManager;
    private readonly Logger _logger;

    public SingInResolverService(ApplicationDbContext context, SignInManager<User> signInManager)
    {
        _context = context;
        _signInManager = signInManager;
        _logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
    }

    public async Task<bool> IsLoggedInAction(LoginViewModel model)
    {
        var userData = _context.Users.FirstOrDefault(p => p.Email == model.Email);
        var result = await _signInManager.PasswordSignInAsync(userData?.UserName ?? String.Empty, model.Password!, model.RememberMe, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            _logger.Info("Пользователь {Email} вошел на сайт", model?.Email);
        }
        else
        {
            _logger.Info("Неудачная попытка входа на сайт, пользователя {Email}", model?.Email);
        }
        return result.Succeeded;
    }

    public async Task LogoutAction() =>
        await _signInManager.SignOutAsync();

}
