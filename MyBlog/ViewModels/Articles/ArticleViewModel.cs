using MyBlog.Models.Articles;

namespace MyBlog.ViewModels.Articles;

public class ArticleViewModel
{
    public List<Article> Articles { get; set; }

    public ArticleViewModel(List<Article> аrticles)
    {
        Articles = аrticles;
    }
}
