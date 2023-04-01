using Contracts.Models.Articles;

namespace Contracts.ViewModels.Articles;

public class UserArticlesViewModel
{
    public List<Article> Articles { get; set; }

    public UserArticlesViewModel(List<Article> аrticles)
    {
        Articles = аrticles;
    }
}
