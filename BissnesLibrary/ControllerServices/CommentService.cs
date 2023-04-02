using Contracts.Models.Articles;
using Contracts.Models.Comments;
using BissnesLibrary.ContextServices.Interface;
using BissnesLibrary.ControllerServices.Interface;
using Contracts.ViewModels.Articles;
using Contracts.ViewModels.Comments;
using NLog;
using DataLibrary.Data.UoW;
using DataLibrary.Data.Repository;

namespace BissnesLibrary.ControllerServices;

public class CommentService : ICommentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ArticleRepository? ArticleRepository;
    private readonly CommentRepository? CommentRepository;
    private readonly Logger _logger;
    private readonly IUserResolverService _userResolverService;

    public CommentService(IUnitOfWork unitOfWork, IUserResolverService userResolverService)
    {
        _unitOfWork = unitOfWork;
        ArticleRepository = _unitOfWork.GetRepository<Article>() as ArticleRepository;
        CommentRepository = _unitOfWork.GetRepository<Comment>() as CommentRepository;
        _logger = LogManager.Setup().GetCurrentClassLogger();
        _userResolverService = userResolverService;
    }

    public async Task<CommentViewModel> GetModelIndex()
    {
        var model = new CommentViewModel(CommentRepository!.GetAllComment());
        var currentUser = await _userResolverService.GetUser();
        model.CurrentUser = currentUser?.Id;
        _logger.Info("Пользователь {Email} открыл страницу со списком комментариев", currentUser?.Email);
        return model;
    }

    public async Task CreateComment(Guid articleId, string content)
    {
        var currentUser = await _userResolverService.GetUser();
        var comment = new Comment(articleId, content, currentUser);
        CommentRepository?.CreateComment(comment);
        _unitOfWork.SaveChanges();
        var article = ArticleRepository?.GetArticleById(articleId);
        _logger.Info("Пользователь {Email} создал комментарий к статье с индификатором {Id} и названием {Title}", currentUser?.Email, article?.Id, article?.Title);
    }

    public async Task<CommentUpdateViewModel> UpdateComment(Guid id)
    {
        var comment = CommentRepository?.GetCommentById(id);
        var article = ArticleRepository?.GetArticleById(comment!.ArticleId);
        var currentUser = await _userResolverService.GetUser();
        _logger.Info("Пользователь {Email} открыл окно редактирования комментария с индификатором {IdComment} из статьи с индификатором {IdArticle} названием {Title}", currentUser?.Email, id, article?.Id, article?.Title);
        return new CommentUpdateViewModel(comment!);
    }

    public async Task<Guid> UpdateComment(CommentUpdateViewModel model)
    {
        var comment = CommentRepository?.GetCommentById(model.Id);
        comment!.Content = model.Content;
        CommentRepository?.UpdateComment(comment);
        _unitOfWork.SaveChanges();
        var article = ArticleRepository?.GetArticleById(comment!.ArticleId);
        var currentUser = await _userResolverService.GetUser();
        _logger.Info("Пользователь {Email} изменил комментарий с индификатором {IdComment} из статьи с индификатором {IdArticle} названием {Title}", currentUser?.Email, model.Id, article?.Id, article?.Title);
        return comment!.ArticleId;
    }

    public async Task Delete(Guid id)
    {
        var comment = CommentRepository?.GetCommentById(id);
        CommentRepository?.DeleteComment(comment);
        _unitOfWork.SaveChanges();
        var article = ArticleRepository?.GetArticleById(comment!.ArticleId);
        var currentUser = await _userResolverService.GetUser();
        _logger.Info("Пользователь {Email} удалил комментарий с индификатором {IdComment} из статьи с индификатором {IdArticle} названием {Title}", currentUser?.Email, id, article?.Id, article?.Title);
    }

    public async Task<ArticleViewModel> GetArticleView(Guid id)
    {
        var article = ArticleRepository?.GetArticleById(id);
        var currentUser = await _userResolverService.GetUser();
        var model = new ArticleViewModel(article, currentUser!);
        model.CurrentUser = currentUser?.Id;        
        return model;
    }

    public  List<Comment> GetAllCommentApi()
    {   
        return CommentRepository!.GetAllCommentApi();
    }

    public Comment GetCommentByIdApi(Guid id)
    {
        return CommentRepository!.GetCommentByIdApi(id);
    }

    public Task<Comment> GetCommentByArticle(Article article)
    {
        throw new NotImplementedException();
    }
}
