using Microsoft.AspNetCore.Identity;
using MyBlog.Data.Repository;
using MyBlog.Data.UoW;
using MyBlog.Models.Articles;
using MyBlog.Models.Comments;
using MyBlog.Models.Users;
using MyBlog.Services.ContextServices.Interface;
using MyBlog.Services.ControllerServices.Interface;
using MyBlog.ViewModels;
using MyBlog.ViewModels.Users;
using NLog;
using NLog.Web;

namespace MyBlog.Services.ControllerServices;

public class AdminService : IAdminService
{
    private readonly Logger _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserResolverService _userResolverService;
    private readonly RoleManager<IdentityRole> _roleManager;


    public AdminService(IUserResolverService userResolverService, IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager)
    {
        _logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
        _unitOfWork = unitOfWork;
        _userResolverService = userResolverService;
        _roleManager = roleManager;
    }

    public async Task EditRoleUser(string userId, List<string> roles)
    {
        var user = await _userResolverService.GetUserById(userId);
        if (user != null)
        {
            // получем список ролей пользователя
            var userRoles = await _userResolverService.GetUserRoles(user);
            // получаем список ролей, которые были добавлены
            var addedRoles = roles.Except(userRoles);
            // получаем роли, которые были удалены
            var removedRoles = userRoles.Except(roles);
            await _userResolverService.AddRolesUser(user, addedRoles);
            await _userResolverService.RemoveRolesUser(user, removedRoles);
            _logger.Info("Изменены права доступа пользователя {Name} ** {Email}", user.UserName, user.Email);
        }
    }

    public async Task<UserPageViewModel> GetUserPageModel(string userId)
    {
        var user = await _userResolverService.GetUserById(userId);
        _logger.Info("Открыта страница пользователя {Email}", user?.Email);
        var model = new UserPageViewModel
        {
            UserViewModel = new UserViewModel(user!)
        };
        model.UserViewModel.AllArticles = GetUserArticles(user!);
        model.UserViewModel.AllComments = GetUserComments(user!);
        return model;
    }

    private List<Comment> GetUserComments(User user)
    {
        if (_unitOfWork.GetRepository<Comment>() is CommentRepository repository)
        {
            return repository.GetCommentByUser(user);
        }
        return new List<Comment>();
    }

    private List<Article> GetUserArticles(User user)
    {
        if (_unitOfWork.GetRepository<Article>() is ArticleRepository repository)
        {
            return repository.GetArticleByUser(user);
        }
        return new List<Article>();
    }

    public async Task DeleteUserAction(string userId)
    {
        var user = await _userResolverService.GetUserById(userId);
        if (user != null)
        {
            await _userResolverService.DeleteUser(user);
            _logger.Info("Удален пользователь {Name} ** {Email}", user.UserName, user.Email);
        }
    }

    public async Task<ChangeRoleViewModel> GetEditModel(string userId)
    {
        var user = await _userResolverService.GetUserById(userId);
        if (user != null)
        {
            // получем список ролей пользователя
            var userRoles = await  _userResolverService.GetUserRoles(user);
            var allRoles = _roleManager.Roles.ToList();
            ChangeRoleViewModel model = new()
            {
                UserId = user.Id,
                UserEmail = user.Email,
                UserRoles = userRoles,
                AllRoles = allRoles
            };   
            return model;
        }
        return new ChangeRoleViewModel();
    }

    public async Task DeleteRole(string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role != null)
        {
            await _roleManager.DeleteAsync(role);
        }
    }
}