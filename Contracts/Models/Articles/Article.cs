using Contracts.Models.Comments;
using Contracts.Models.Tegs;
using Contracts.Models.Users;

namespace Contracts.Models.Articles;

public class Article
{
    public Guid Id { get; set; } 
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? UserId { get; set; }
    public User? User { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public int CountView { get; set; }
    public List<Comment>? Comments { get; set; }
    public List<Teg>? Tegs { get; set; }

}
