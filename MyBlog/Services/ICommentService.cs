using MyBlog.ViewModels.Articles;
using MyBlog.ViewModels.Comments;

namespace MyBlog.Services
{
    public interface ICommentService
    {
        Task<CommentViewModel> GetModelIndex();

        Task CreateComment(ArticleViewModel model);

        CommentUpdateViewModel UpdateComment(Guid id);

        void UpdateComment(CommentUpdateViewModel model);

        void Delete(Guid id);

    }
}
