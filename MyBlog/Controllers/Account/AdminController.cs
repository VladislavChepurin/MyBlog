using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Data.Repository;
using MyBlog.Data.UoW;
using MyBlog.Models.Articles;
using MyBlog.Models.Comments;
using MyBlog.Models.Users;
using MyBlog.ViewModels;
using MyBlog.ViewModels.Users;

namespace MyBlog.Controllers.Account;

[Authorize(Roles = "Administrator")]
public class AdminController : Controller
{
    private readonly ILogger<AdminController> _logger;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public AdminController(ILogger<AdminController> logger, RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _roleManager = roleManager;
        _userManager = userManager;
        _unitOfWork=unitOfWork;        
    }
        
    public IActionResult Index() => View(_userManager.Users.ToList());

    public IActionResult Create() => View();

    public IActionResult RoleList() => View(_roleManager.Roles.ToList());

    [Route("Delete")]
    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        IdentityRole? role = await _roleManager.FindByIdAsync(id);
        if (role != null)
        {
            await _roleManager.DeleteAsync(role);
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Create(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }
        return View(name);
    }

    [Route("Edit")]
    [HttpGet]
    public async Task<IActionResult> Edit(string userId)
    {
        // получаем пользователя
        User? user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            // получем список ролей пользователя
            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.ToList();
            ChangeRoleViewModel model = new()
            {
                UserId = user.Id,
                UserEmail = user.Email,
                UserRoles = userRoles,
                AllRoles = allRoles
            };
            return View(model);
        }
        return NotFound();
    }

    [Route("Edit")]
    [HttpPost]
    public async Task<IActionResult> Edit(string userId, List<string> roles)
    {
        // получаем пользователя
        User? user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            // получем список ролей пользователя
            var userRoles = await _userManager.GetRolesAsync(user);
            // получаем все роли
            //var allRoles = _roleManager.Roles.ToList();
            // получаем список ролей, которые были добавлены
            var addedRoles = roles.Except(userRoles);
            // получаем роли, которые были удалены
            var removedRoles = userRoles.Except(roles);
            await _userManager.AddToRolesAsync(user, addedRoles);
            await _userManager.RemoveFromRolesAsync(user, removedRoles);
            _logger.LogError(0, "Изменены права доступа пользователя {Name} ** {Email}", user.UserName, user.Email);
            return RedirectToAction("Index");
        }
        return NotFound();
    }

    [Route("DeleteUser")]
    [HttpGet]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        // получаем пользователя
        User? user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            await _userManager.DeleteAsync(user);
            _logger.LogError(0, "Удален пользователь {Name} ** {Email}", user.UserName, user.Email);
            return RedirectToAction("Index");
        }
        return NotFound();
    }

    [HttpGet]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> UserPage(string userId)
    {
        User? user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var model = new UserPageViewModel
            {
                UserViewModel = new UserViewModel(user)
            };
            model.UserViewModel.AllArticles = GetUserArticles(user);
            model.UserViewModel.AllComments = GetUserComments(user);
            return View(model);
        }
        return NotFound();
    }

    public List<Comment> GetUserComments(User user)
    {
        if (_unitOfWork.GetRepository<Comment>() is CommentRepository repository)
        {
            return repository.GetCommentByUser(user);
        }
        return new List<Comment>();
    }

    public List<Article> GetUserArticles(User user)
    {
        if (_unitOfWork.GetRepository<Article>() is ArticleRepository repository)
        {
            return repository.GetArticleByUser(user);
        }
        return new List<Article>();
    }
}