using Contracts.ViewModels;
using Contracts.ViewModels.Users;

namespace BissnesLibrary.ControllerServices.Interface;

public interface IAccountService
{
    Task<UserPageViewModel> GetUserPageModel();

    Task<LoginViewModel> GetLoginModel();

    Task Logout();

    Task<bool> IsLoggedIn(LoginViewModel model);
}
