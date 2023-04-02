using Contracts.Models.Articles;
using Contracts.Models.Comments;
using Contracts.ViewModels.Articles;
using Contracts.ViewModels.Comments;

namespace BissnesLibrary.ControllerServices.Interface
{
    public interface ICommentService
    {
        Task<CommentViewModel> GetModelIndex();

        Task CreateComment(Guid articleId, string content);

        Task<CommentUpdateViewModel> UpdateComment(Guid id);

        Task<Guid> UpdateComment(CommentUpdateViewModel model);

        Task Delete(Guid id);

        Task<ArticleViewModel> GetArticleView(Guid id);

        List<Comment> GetAllCommentApi();

        Comment GetCommentByIdApi(Guid id);

        Task<Comment> GetCommentByArticle(Article article);

    }
}

