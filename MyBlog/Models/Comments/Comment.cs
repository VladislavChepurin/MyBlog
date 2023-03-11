using MyBlog.Models.Articles;
using MyBlog.Models.Users;

namespace MyBlog.Models.Comments;

public class Comment
{
    public Guid Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public string? Content { get; set; }
    public string? UserId { get; set; }
    public User? User { get; set; }
    public Guid ArticleId { get; set; }
    public Article? Article { get; set; }
}
