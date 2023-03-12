using MyBlog.ViewModels.Articles;
using MyBlog.ViewModels.Users;


namespace MyBlog.ViewModels;

public class UserPageViewModel
{
    public UserViewModel? UserViewModel { get; set; }

    public AllArticlesViewModel? RegisterViewsModel { get; set; }
}
