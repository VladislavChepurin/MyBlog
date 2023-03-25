using MyBlog.ViewModels.Users;

namespace MyBlog.Services.ContextServices.Interface;

public interface ISingInResolverService
{
    Task LogoutAction();

    Task<bool> IsLoggedInAction(LoginViewModel model);

}
