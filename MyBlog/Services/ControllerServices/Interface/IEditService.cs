using MyBlog.ViewModels;
using MyBlog.ViewModels.Users;

namespace MyBlog.Services.ControllerServices.Interface;

public interface IEditService
{
    Task<UserEditViewModel> GetEditUserModel(string userId);

    Task<UserPageViewModel> GetUserPageModel(string userId);

    Task<bool> UpdateUser(UserEditViewModel userEdit);
}
