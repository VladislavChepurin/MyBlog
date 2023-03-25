using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBlog.Data;
using MyBlog.Models.Users;
using MyBlog.Services.ContextServices.Interface;
using MyBlog.ViewModels.Users;

namespace MyBlog.Services.ContextServices;

public class SingInResolverService : ISingInResolverService
{
    private readonly ApplicationDbContext _context;
    private readonly SignInManager<User> _signInManager;


    public SingInResolverService(ApplicationDbContext context, SignInManager<User> signInManager)
    {
        _context = context;
        _signInManager = signInManager;
    }

    public async Task<bool> IsLoggedInAction(LoginViewModel model)
    {
        var userData = _context.Users.FirstOrDefault(p => p.Email == model.Email);
        var result = await _signInManager.PasswordSignInAsync(userData?.UserName ?? String.Empty, model.Password!, model.RememberMe, lockoutOnFailure: false);
        return result.Succeeded;
    }

    public async Task LogoutAction() =>
        await _signInManager.SignOutAsync();

}
