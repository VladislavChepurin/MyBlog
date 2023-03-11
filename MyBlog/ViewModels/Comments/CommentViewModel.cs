using MyBlog.Models.Comments;

namespace MyBlog.ViewModels.Comments;

public class CommentViewModel
{
    public Comment Comment { get; set; }
    public CommentViewModel(Comment comment)
    {
        Comment = comment;
    }
}
