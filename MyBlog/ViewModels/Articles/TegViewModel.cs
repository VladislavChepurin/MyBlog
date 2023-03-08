using MyBlog.Models.Articles;

namespace MyBlog.ViewModels.Articles;

public class TegViewModel
{
    public List<Teg> Tegs { get; set; } 

    public TegViewModel(List<Teg> tegs)
    {
        Tegs = tegs;
    }
}
