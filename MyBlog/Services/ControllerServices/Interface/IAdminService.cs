using MyBlog.ViewModels;

namespace MyBlog.Services.ControllerServices.Interface;

public interface IAdminService
{
    Task EditRoleUser(string userId, List<string> roles);

    Task<UserPageViewModel> GetUserPageModel(string userId);

    Task DeleteUserAction(string userId);

    Task<ChangeRoleViewModel> GetEditModel(string userId);

    Task DeleteRole(string roleId);

}

