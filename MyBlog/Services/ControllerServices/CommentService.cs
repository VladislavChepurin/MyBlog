using MyBlog.Data.Repository;
using MyBlog.Data.UoW;
using MyBlog.Models.Articles;
using MyBlog.Models.Comments;
using MyBlog.Services.ContextServices.Interface;
using MyBlog.Services.ControllerServices.Interface;
using MyBlog.ViewModels.Articles;
using MyBlog.ViewModels.Comments;
using NLog;
using NLog.Web;

namespace MyBlog.Services.ControllerServices;

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
        _logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
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

    public async Task CreateComment(ArticleViewModel model)
    {
        var currentUser = await _userResolverService.GetUser();
        var comment = new Comment(model, currentUser);
        CommentRepository?.CreateComment(comment);
        _unitOfWork.SaveChanges();  
        
        var article = ArticleRepository?.GetArticleById(model.Article!.Id);
        _logger.Info("Пользователь {Email} создал комментарий к статье с названием {Title}", currentUser?.Email, article?.Title);
    }

    public async Task<CommentUpdateViewModel> UpdateComment(Guid id)
    {
        var comment = CommentRepository?.GetCommentById(id);

        var article = ArticleRepository?.GetArticleById(comment!.ArticleId);
        var currentUser = await _userResolverService.GetUser();
        _logger.Info("Пользователь {Email} открыл окно редактирования комментария с индификатором {Id} из статьи с названием {Title}", currentUser?.Email, id, article?.Title);

        return new CommentUpdateViewModel(comment!);
    }

    public async Task UpdateComment(CommentUpdateViewModel model)
    {
        var comment = CommentRepository?.GetCommentById(model.Id);
        comment!.Content = model.Content;
        CommentRepository?.UpdateComment(comment);
        _unitOfWork.SaveChanges();

        var article = ArticleRepository?.GetArticleById(comment!.ArticleId);
        var currentUser = await _userResolverService.GetUser();
        _logger.Info("Пользователь {Email} изменил комментарий с индификатором {Id} из статьи с названием {Title}", currentUser?.Email, model.Id, article?.Title);
    }

    public async Task Delete(Guid id)
    {
        var comment = CommentRepository?.GetCommentById(id);
        CommentRepository?.DeleteComment(comment);
        _unitOfWork.SaveChanges();

        var article = ArticleRepository?.GetArticleById(comment!.ArticleId);
        var currentUser = await _userResolverService.GetUser();
        _logger.Info("Пользователь {Email} удалил комментарий с индификатором {Id} из статьи с названием {Title}", currentUser?.Email, id, article?.Title);
    }
}
