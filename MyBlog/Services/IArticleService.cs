using MyBlog.Models.Articles;
using MyBlog.Models.Users;
using MyBlog.ViewModels.Articles;

namespace MyBlog.Services
{
    public interface IArticleService
    {
        Task<AllArticlesViewModel> GetModelIndex();

        Task<ArticleViewModel> GetArticleView(Guid id);

        AddArticleViewModel GetAddArticleView(AddArticleViewModel model);

        AddArticleViewModel GetAddArticleView();

        Task CreateArticle(AddArticleViewModel article, List<Guid> tegsCurrent);

        Task DeleteArticle(Guid id);
        ArticleUpdateViewModel UpdateArticle(Guid id);

        void UpdateArticle(ArticleUpdateViewModel model, List<Guid> tegsCurrent);

        List<Article> GetArticleByUser(User user);

        Article GetArticleById(Guid id);
    }
}


