using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MyBlog.Data.Repository;
using MyBlog.Data.UoW;
using MyBlog.Models.Articles;
using MyBlog.Models.Comments;
using MyBlog.Models.Tegs;
using MyBlog.Models.Users;
using MyBlog.Services.Interface;
using MyBlog.ViewModels.Articles;
using MyBlog.ViewModels.Comments;
using NLog;
using NLog.Web;

namespace MyBlog.Services;

public class CommentService: ICommentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ArticleRepository? ArticleRepository;
    private readonly TegRepository? TegRepository;
    private readonly CommentRepository? CommentRepository;
    private readonly Logger logger;
    private readonly IUserResolverService _userResolverService;



    public CommentService(IUnitOfWork unitOfWork, IUserResolverService userResolverService)
    {
        _unitOfWork = unitOfWork;
        ArticleRepository = _unitOfWork.GetRepository<Article>() as ArticleRepository;
        TegRepository = _unitOfWork.GetRepository<Teg>() as TegRepository;
        CommentRepository = _unitOfWork.GetRepository<Comment>() as CommentRepository;
        logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
        _userResolverService = userResolverService;
    }

    public async Task<CommentViewModel> GetModelIndex()
    {
        var model = new CommentViewModel(CommentRepository!.GetAllComment());
        var currentUser = await _userResolverService.GetUser();
        model.CurrentUser = currentUser?.Id;
        logger.Info("Пользователь {Email} открыл страницу со списком комментариев", currentUser?.Email);
        return model;
    }

    public async Task CreateComment(ArticleViewModel model)
    {
        var currentUser = await _userResolverService.GetUser();
        var comment = new Comment(model, currentUser);
        CommentRepository?.CreateComment(comment);
        _unitOfWork.SaveChanges();
    }

    public CommentUpdateViewModel UpdateComment(Guid id)
    {
        var comment = CommentRepository?.GetCommentById(id);
        return new CommentUpdateViewModel(comment!);
    }

    public void UpdateComment(CommentUpdateViewModel model)
    {
        var comment = CommentRepository?.GetCommentById(model.Id);
        comment!.Content = model.Content;
        CommentRepository?.UpdateComment(comment);
        _unitOfWork.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var comment = CommentRepository?.GetCommentById(id);
        CommentRepository?.DeleteComment(comment);
        _unitOfWork.SaveChanges();
    }
}
