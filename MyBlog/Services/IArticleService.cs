using MyBlog.Models.Articles;
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

    }
}


