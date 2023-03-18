using MyBlog.Models.Comments;

namespace MyBlog.ViewModels.Comments;

public class CommentUpdateViewModel
{
    public Guid Id { get; set; }
    public string? Content { get; set; }

    public CommentUpdateViewModel(Comment comment)
    {
        Id = comment.Id;
        Content = comment.Content;
    }
    public CommentUpdateViewModel()
    {
    }
}
