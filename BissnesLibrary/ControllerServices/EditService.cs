using Contracts.Models.Articles;
using Contracts.Models.Comments;
using Contracts.Models.Users;
using BissnesLibrary.ContextServices.Interface;
using BissnesLibrary.ControllerServices.Interface;
using Contracts.ViewModels;
using Contracts.ViewModels.Users;
using NLog;
using DataLibrary.Data.UoW;
using DataLibrary.Data.Repository;
using BissnesLibrary.Extentions;

namespace BissnesLibrary.ControllerServices;

public class EditService : IEditService
{
    private readonly IUserResolverService _userResolverService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly Logger _logger;

    public EditService(IUserResolverService userResolverService, IUnitOfWork unitOfWork)
    {
        _userResolverService = userResolverService;
        _unitOfWork = unitOfWork;
        _logger = LogManager.Setup()/*.LoadConfigurationFromAppSettings()*/.GetCurrentClassLogger();
    }

    public async Task<UserEditViewModel> GetEditUserModel(string userId)
    {
        var user = await _userResolverService.GetUserById(userId);
        if (user != null)
        {
            var currentUser = await _userResolverService.GetUser();            
            if (userId == currentUser.Id)
            {
                _logger.Info("Пользователь {Email} открыл страницу изменения профиля", user?.Email);
            }
            else
            {
                var userRoles = await _userResolverService.GetUserRoles(currentUser);
                if (userRoles.Contains("Administrator"))
                {
                    _logger.Info("Администратор {EmailAdmin} открыл страницу изменения профиля {EmailUser}", currentUser?.Email, user.Email);
                }
                else
                {
                    _logger.Warn("Этого не должно произоити, без прав администратора нельзя изменить другого пользователя, это сделал пользователь {Email}", currentUser.Email);
                }
            }
            var view = new UserEditViewModel(user!);
            return view;
        }
        return null!;
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

    public async Task<bool> UpdateUser(UserEditViewModel userEdit)
    {
        var user = await _userResolverService.GetUserById(userEdit.UserId!);
        user?.Convert(userEdit);
        var result = await _userResolverService.UpdateUserAction(user!);
        if (result.Succeeded)
        {
            _logger.Info("Учетная запись {Email} успешно изменена", user?.Email);
        }
        return result.Succeeded;
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
}
