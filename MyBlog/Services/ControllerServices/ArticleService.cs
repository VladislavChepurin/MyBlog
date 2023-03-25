using AutoMapper;
using MyBlog.Data.Repository;
using MyBlog.Data.UoW;
using MyBlog.Models.Articles;
using MyBlog.Models.Tegs;
using MyBlog.Models.Users;
using MyBlog.Services.ContextServices.Interface;
using MyBlog.Services.ControllerServices.Interface;
using MyBlog.ViewModels.Articles;
using NLog;
using NLog.Web;

namespace MyBlog.Services.ControllerServices;

public class ArticleService : IArticleService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ArticleRepository? ArticleRepository;
    private readonly TegRepository? TegRepository;
    private readonly Logger _logger;
    private readonly IUserResolverService _userResolverService;

    public ArticleService(IMapper mapper, IUnitOfWork unitOfWork, IUserResolverService userResolverService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        ArticleRepository = _unitOfWork.GetRepository<Article>() as ArticleRepository;
        TegRepository = _unitOfWork.GetRepository<Teg>() as TegRepository;
        _logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
        _userResolverService = userResolverService;
    }

    public async Task<AllArticlesViewModel> GetModelIndex()
    {
        var model = new AllArticlesViewModel(ArticleRepository!.GetAllArticle());
        var currentUser = await _userResolverService.GetUser();
        model.CurrentUser = currentUser?.Id;
        _logger.Info("Пользователь {Email} открыл страницу со списком статей", currentUser?.Email);
        return model;
    }

    public async Task<ArticleViewModel> GetArticleView(Guid id)
    {
        var article = ArticleRepository?.GetArticleById(id);
        ++article!.CountView;
        ArticleRepository?.Update(article);
        _unitOfWork.SaveChanges();
        var currentUser = await _userResolverService.GetUser();
        var model = new ArticleViewModel(article, currentUser!);
        model.CurrentUser = currentUser?.Id;
        _logger.Info("Пользователь {Email} открыл статью с заголовком {Title}", currentUser?.Email, article.Title);
        return model;
    }

    public async Task<AddArticleViewModel> GetAddArticleView(AddArticleViewModel model)
    {
        var article = _mapper.Map<Article>(model);
        var tegs = TegRepository?.GetAllTeg();
        var currentUser = await _userResolverService.GetUser();
        _logger.Info("Пользователь {Email} ошибочно ввел данные на странице создания статей", currentUser?.Email);
        return new AddArticleViewModel(tegs, article);
    }

    public async Task< AddArticleViewModel> GetAddArticleView()
    {      
        var currentUser = await _userResolverService.GetUser();
        _logger.Info("Пользователь {Email} открыл страницу создания статей", currentUser?.Email);
        var tegs = TegRepository?.GetAllTeg();
        return new AddArticleViewModel(tegs);
    }

    public async Task CreateArticle(AddArticleViewModel model, List<Guid> tegsCurrent)
    {
        var article = _mapper.Map<Article>(model);
        var currentUser = await _userResolverService.GetUser();
        article.UserId = currentUser?.Id;
        ArticleRepository?.CreateArticle(article);
        TegRepository?.AddTegInArticles(article, tegsCurrent);
        _unitOfWork.SaveChanges();
        _logger.Info("Пользователь {Email} создал статью с заголовком {Title}", currentUser?.Email, article.Title);
    }

    public async Task DeleteArticle(Guid id)
    {
        var currentUser = await _userResolverService.GetUser();
        var article = ArticleRepository?.GetArticleById(id);
        ArticleRepository?.DeleteArticle(article);
        _unitOfWork.SaveChanges();
        _logger.Info("Пользователь {Email} удалил статью с заголовком {Title}", currentUser?.Email, article?.Title);
    }

    public async Task<ArticleUpdateViewModel> UpdateArticle(Guid id)
    {
        var article = ArticleRepository?.GetArticleById(id);
        var currentUser = await _userResolverService.GetUser();
        _logger.Info("Пользователь {Email} открыл сраницу редектирования статьи с заголовком {Title}", currentUser?.Email, article?.Title);
        return new ArticleUpdateViewModel(article!, TegRepository);
    }

    public async Task UpdateArticle(ArticleUpdateViewModel model, List<Guid> tegsCurrent)
    {
        var article = ArticleRepository?.GetArticleById(model.Id);
        article!.Content = model.Content;
        article.Title = model.Title;
        ArticleRepository?.UpdateArticle(article);
        TegRepository?.UpdateTegsInArticles(article, tegsCurrent);
        _unitOfWork.SaveChanges();

        var currentUser = await _userResolverService.GetUser();
        _logger.Info("Пользователь {Email} изменил статью с заголовком {Title}", currentUser?.Email, article?.Title);
    }

    public List<Article> GetArticleByUser(User user)
    {
        return ArticleRepository?.GetArticleByUser(user)!;
    }

    public Article GetArticleById(Guid id)
    {
        return ArticleRepository?.GetArticleById(id)!;
    }
}