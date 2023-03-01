namespace MyBlog.Models.Articles
{
    public class Teg
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public List<Article>? Articles { get; set; }
    }
}
