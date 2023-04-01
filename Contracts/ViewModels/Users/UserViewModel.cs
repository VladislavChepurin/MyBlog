using Contracts.Models.Articles;
using Contracts.Models.Comments;
using Contracts.Models.Users;

namespace Contracts.ViewModels.Users;

public class UserViewModel
{
    public User User { get; set; }
    public List<Article>? AllArticles { get; set; }
    public List<Comment>? AllComments { get; set; }

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
