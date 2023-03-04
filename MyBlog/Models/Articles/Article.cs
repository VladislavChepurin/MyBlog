using MyBlog.Models.Users;

namespace MyBlog.Models.Articles;

public class Article
{
    public Guid Id { get; set; } 
    public string? Title { get; set; }
    public string? Content { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public bool Private { get; set; }
    public string? UserId { get; set; }
    public User? User { get; set; }
    public List<Teg>? Tegs { get; set; }
}
