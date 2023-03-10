using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Data;
using MyBlog.Data.Repository;
using MyBlog.Data.UoW;
using MyBlog.Models.Articles;
using MyBlog.Models.Users;
using MyBlog.ViewModels;
using MyBlog.ViewModels.Users;

namespace MyBlog.Controllers.Account;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public AccountController(ApplicationDbContext context, UserManager<User> userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public IActionResult AllUsers() => View(_userManager.Users.ToList());
    public IActionResult AccessDenied() => View();
    public IActionResult Index() => View();

    [Authorize]
    [HttpPost]
    [Route("UserPage")]
    public async Task<IActionResult> UserPage()
    {
        var userClaims = User;
        var user = await _userManager.GetUserAsync(userClaims);
        var model = new UserPageViewModel
        {
            UserViewModel = new UserViewModel(user)
        };      
        return View("User", model);
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> LoginAsync(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var userData = _context.Users.FirstOrDefault(p => p.Email == model.Email);
            var result = await _signInManager.PasswordSignInAsync(userData?.UserName ?? String.Empty, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
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
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Route("Login")]
    public IActionResult Login()
    {
        return View("Login");
    }

    public List<Article> GetAllArticles(User user)
    {
        var repository = _unitOfWork.GetRepository<Article>() as ArticleRepository;
        return repository.GetArticleByUser(user);
    }

}
