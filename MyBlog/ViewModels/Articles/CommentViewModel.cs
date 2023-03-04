using MyBlog.Models.Articles;

namespace MyBlog.ViewModels.Articles;

public class CommentViewModel
{
    public Comment Comment { get; set; }
    public CommentViewModel(Comment comment)
    {
        Comment = comment;
    }
}
