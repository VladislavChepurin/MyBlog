using Contracts.Models.Articles;
using Contracts.Models.Users;
using Contracts.ViewModels.Articles;

namespace Contracts.Models.Comments;

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

    public Comment()
    {        
    }

    public Comment(ArticleViewModel model, User user)
    {
        Content = model.CommentContent;
        ArticleId = model.Article!.Id;
        UserId = user?.Id;
    }
}
