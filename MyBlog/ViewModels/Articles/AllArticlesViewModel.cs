using MyBlog.Models.Articles;

namespace MyBlog.ViewModels.Articles;

public class AllArticlesViewModel
{
    public List<Article> Articles { get; set; }

    public AllArticlesViewModel(List<Article> аrticles)
    {
        Articles = аrticles;
    }
}
