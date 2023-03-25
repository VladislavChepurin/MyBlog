using MyBlog.ViewModels;
using MyBlog.ViewModels.Users;

namespace MyBlog.Services.ControllerServices.Interface;

public interface IAccountService
{
    Task<UserPageViewModel> GetUserPageModel();

    Task<LoginViewModel> GetLoginModel();

    Task Logout();

    Task<bool> IsLoggedIn(LoginViewModel model);
}
