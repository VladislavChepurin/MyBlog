using MyBlog.Models.Articles;

namespace MyBlog.ViewModels.Articles;

public class AllArticlesViewModel
{
    public List<Article>? Articles { get; set; }

    public string? CurrentUser { get; set; }

    public AllArticlesViewModel()
    {        
    }

    public AllArticlesViewModel(List<Article> аrticles)
    {
        Articles = аrticles;
    }
}

