using MyBlog.ViewModels.Articles;
using MyBlog.ViewModels.Users;


namespace MyBlog.ViewModels;

public class UserPageViewModel
{
    public UserViewModel? UserViewModel { get; set; }

    public UserArticlesViewModel? UserArticlesViewModel { get; set; }
}
