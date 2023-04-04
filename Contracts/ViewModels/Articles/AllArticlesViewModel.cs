using Contracts.Models.Articles;
using Contracts.Models.Users;

namespace Contracts.ViewModels.Articles;

public class AllArticlesViewModel
{
    public List<Article>? Articles { get; set; }

    public string? CurrentUser { get; set; }

    public AllArticlesViewModel()
    {        
    }

    public AllArticlesViewModel(List<Article> аrticles, User currentUser)
    {
        Articles = аrticles;
        CurrentUser = currentUser.Id;
    }
}

