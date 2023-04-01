using Contracts.ViewModels.Users;

namespace BissnesLibrary.ContextServices.Interface;

public interface ISingInResolverService
{
    Task LogoutAction();

    Task<bool> IsLoggedInAction(LoginViewModel model);

}
