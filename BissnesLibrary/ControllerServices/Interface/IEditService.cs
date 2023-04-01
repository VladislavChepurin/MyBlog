using Contracts.ViewModels;
using Contracts.ViewModels.Users;

namespace BissnesLibrary.ControllerServices.Interface;

public interface IEditService
{
    Task<UserEditViewModel> GetEditUserModel(string userId);

    Task<UserPageViewModel> GetUserPageModel(string userId);

    Task<bool> UpdateUser(UserEditViewModel userEdit);
}
