using MyBlog.ViewModels.Articles;
using MyBlog.ViewModels.Comments;

namespace MyBlog.Services.ControllerServices.Interface
{
    public interface ICommentService
    {
        Task<CommentViewModel> GetModelIndex();

        Task CreateComment(ArticleViewModel model);

        Task<CommentUpdateViewModel> UpdateComment(Guid id);

        Task UpdateComment(CommentUpdateViewModel model);

        Task Delete(Guid id);

        Task<ArticleViewModel> GetArticleView(Guid id);

    }
}

