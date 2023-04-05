using Contracts.Models.Articles;
using Contracts.Models.Users;
using Contracts.ViewModels.Articles;

namespace BissnesLibrary.ControllerServices.Interface
{
    public interface IArticleService
    {
        Task<AllArticlesViewModel> GetModelIndex();

        Task<ArticleViewModel> GetArticleView(Guid id);

        Task<AddArticleViewModel> GetAddArticleView(AddArticleViewModel model);

        Task<AddArticleViewModel> GetAddArticleView();      

        Task CreateArticle(Article article, List<Guid>? tegsCurrent);

        Task DeleteArticle(Guid id);

        Task<ArticleUpdateViewModel> UpdateArticle(Guid id);

        Task UpdateArticle(Guid id, string? title, string? content, List<Guid>? tegsCurrent);

        List<Article> GetArticleByUser(User user);

        Article GetArticleById(Guid id);
    }
}


