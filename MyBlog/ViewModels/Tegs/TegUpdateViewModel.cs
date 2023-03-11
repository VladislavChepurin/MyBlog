using MyBlog.Models.Tegs;

namespace MyBlog.ViewModels.Tegs;

public class TegUpdateViewModel
{
    public Guid Id { get; set; }
    public string? Content { get; set; }

    public TegUpdateViewModel(Teg teg)
    {
        Id = teg.Id;
        Content = teg.Content;
    }
    public TegUpdateViewModel()
    {

    }    
}
