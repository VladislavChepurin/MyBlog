using Contracts.ViewModels.Articles;
using Contracts.ViewModels.Comments;

namespace BissnesLibrary.ControllerServices.Interface
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

