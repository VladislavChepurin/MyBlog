using Contracts.ViewModels.Articles;
using Contracts.ViewModels.Users;


namespace Contracts.ViewModels;

public class UserPageViewModel
{
    public UserViewModel? UserViewModel { get; set; }

    public UserArticlesViewModel? UserArticlesViewModel { get; set; }
}
