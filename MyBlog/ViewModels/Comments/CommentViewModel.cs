using MyBlog.Models.Comments;

namespace MyBlog.ViewModels.Comments;

public class CommentViewModel
{
    public List<Comment>? Comment { get; set; }
    public CommentViewModel(List<Comment>? comment)
    {
        Comment = comment;
    }
}
