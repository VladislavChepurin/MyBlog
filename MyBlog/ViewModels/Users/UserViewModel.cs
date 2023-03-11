using MyBlog.Models.Articles;
using MyBlog.Models.Users;

namespace MyBlog.ViewModels.Users;

public class UserViewModel
{
    public User User { get; set; }
    public List<Article>? AllArticles { get; set; }

    private int number = default;
    public int Number
    {
        get
        {
            return ++number;    // возвращаем значение свойства
        }           
    }

    public UserViewModel(User user)
    {
        User = user;
    }    

}
