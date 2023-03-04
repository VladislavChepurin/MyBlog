using MyBlog.Models.Articles;

namespace MyBlog.ViewModels.Articles;

public class TegViewModel
{
    public Teg Teg { get; set; }
    public TegViewModel(Teg teg)
    {
        Teg = teg;
    }
}
