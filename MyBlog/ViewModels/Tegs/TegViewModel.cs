using MyBlog.Models.Tegs;

namespace MyBlog.ViewModels.Tegs;

public class TegViewModel
{
    public List<Teg>? Tegs { get; set; }

    public TegViewModel(List<Teg>? tegs)
    {
        Tegs = tegs;
    }
}
