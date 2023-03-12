using MyBlog.Models.Articles;
using MyBlog.Models.Comments;

namespace MyBlog.ViewModels.Articles
{
    public class ArticleViewModel
    {
        public Article? Article { get; set; }       
        public List<Comment>? Comments { get; set; }

        public ArticleViewModel(Article? article, List<Comment>? comments)
        {
            Article = article;           
            Comments = comments;
        }
    }
}
