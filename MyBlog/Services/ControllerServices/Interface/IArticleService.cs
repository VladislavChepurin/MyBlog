using MyBlog.Models.Articles;
using MyBlog.Models.Users;
using MyBlog.ViewModels.Articles;

namespace MyBlog.Services.ControllerServices.Interface
{
    public interface IArticleService
    {
        Task<AllArticlesViewModel> GetModelIndex();

        Task<ArticleViewModel> GetArticleView(Guid id);

        Task<AddArticleViewModel> GetAddArticleView(AddArticleViewModel model);

        Task<AddArticleViewModel> GetAddArticleView();

        Task CreateArticle(AddArticleViewModel article, List<Guid> tegsCurrent);

        Task DeleteArticle(Guid id);

        Task<ArticleUpdateViewModel> UpdateArticle(Guid id);

        Task UpdateArticle(ArticleUpdateViewModel model, List<Guid> tegsCurrent);

        List<Article> GetArticleByUser(User user);

        Article GetArticleById(Guid id);
    }
}


