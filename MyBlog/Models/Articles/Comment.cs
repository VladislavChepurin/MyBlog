using MyBlog.Models.Users;

namespace MyBlog.Models.Articles
{
    public class Comment
    {
        public int Id { get; set; }
     
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string? Content { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
        public string? ArticleId { get; set; }
        public Article? Article { get; set; }
    }
}
