using MyBlog.Models.Articles;

namespace MyBlog.ViewModels.Articles
{
    public class ArticleViewModel
    {
        public Article? Article { get; set; }       

        public ArticleViewModel(Article? article)
        {
            Article = article;
            if (article != null)
            {
                article.Comments = Article?.Comments?.OrderBy(d => d.Created).ToList();
            }            
        }
    }
}
