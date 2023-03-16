using MyBlog.Models.Comments;

namespace MyBlog.ViewModels.Comments;

public class CommentViewModel
{
    public List<Comment>? Comment { get; set; }

    public string? CurrentUser { get; set; }

    public CommentViewModel()
    {        
    }

    public CommentViewModel(List<Comment>? comment)
    {
        Comment = comment;
    }
}
