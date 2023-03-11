using MyBlog.Models.Articles;

namespace MyBlog.Models.Tegs;

public class Teg
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public List<Article>? Articles { get; set; }
}
