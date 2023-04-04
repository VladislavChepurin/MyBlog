using AutoMapper;
using DataLibrary.Data.Repository;
using DataLibrary.Data.UoW;
using Contracts.Models.Articles;
using Contracts.Models.Tegs;
using Contracts.Models.Users;
using BissnesLibrary.ContextServices.Interface;
using BissnesLibrary.ControllerServices.Interface;
using Contracts.ViewModels.Articles;
using NLog;

namespace BissnesLibrary.ControllerServices;

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
        _logger = LogManager.Setup().GetCurrentClassLogger();
        _userResolverService = userResolverService;
    }

    public async Task<AllArticlesViewModel> GetModelIndex()
    {
        var currentUser = await _userResolverService.GetUser();
        var allArticles = ArticleRepository!.GetAllArticle();
        var model = new AllArticlesViewModel(allArticles, currentUser);       
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
        _logger.Info("Пользователь {Email} открыл статью с индификатоом {Id} и заголовком {Title}", currentUser?.Email, id, article.Title);
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

    public async Task<AddArticleViewModel> GetAddArticleView()
    {
        var currentUser = await _userResolverService.GetUser();
        _logger.Info("Пользователь {Email} открыл страницу создания статей", currentUser?.Email);
        var tegs = TegRepository?.GetAllTeg();
        return new AddArticleViewModel(tegs);
    }

    public async Task CreateArticle(AddArticleViewModel model, List<Guid>? tegsCurrent)
    {
        var article = _mapper.Map<Article>(model);
        var currentUser = await _userResolverService.GetUser();
        article.UserId = currentUser?.Id;
        ArticleRepository?.CreateArticle(article);
        TegRepository?.AddTegInArticles(article, tegsCurrent);
        _unitOfWork.SaveChanges();
        _logger.Info("Пользователь {Email} создал статью с заголовком {Title}", currentUser?.Email, article.Title);
    }


    //public async Task CreateArticle(string? title, string? content, List<Guid>? tegsCurrent)
    //{
    //    var article = new Article(title, content);
    //    var currentUser = await _userResolverService.GetUser();
    //    article.UserId = currentUser?.Id;
    //    ArticleRepository?.CreateArticle(article);
    //    TegRepository?.AddTegInArticles(article, tegsCurrent);
    //    _unitOfWork.SaveChanges();
    //    _logger.Info("Пользователь {Email} создал статью с заголовком {Title}", currentUser?.Email, article.Title);
    //}

    public async Task DeleteArticle(Guid id)
    {
        var currentUser = await _userResolverService.GetUser();
        var article = ArticleRepository?.GetArticleById(id);
        ArticleRepository?.DeleteArticle(article);
        _unitOfWork.SaveChanges();
        _logger.Info("Пользователь {Email} удалил статью с индификатором {Id} и заголовком {Title}", currentUser?.Email, id, article?.Title);
    }

    public async Task<ArticleUpdateViewModel> UpdateArticle(Guid id)
    {
        var article = ArticleRepository?.GetArticleById(id);
        var currentUser = await _userResolverService.GetUser();
        _logger.Info("Пользователь {Email} открыл сраницу редектирования статьи с индификатором {Id} заголовком {Title}", currentUser?.Email, id, article?.Title);
        var model = new ArticleUpdateViewModel(article!) 
            {TegList = TegRepository?.GetAllTeg() };   
        return model;
    }

    public async Task UpdateArticle(ArticleUpdateViewModel model, List<Guid>? tegsCurrent)
    {
        var article = ArticleRepository?.GetArticleById(model.Id);
        article!.Content = model.Content;
        article.Title = model.Title;
        ArticleRepository?.UpdateArticle(article);
        TegRepository?.UpdateTegsInArticles(article, tegsCurrent);
        _unitOfWork.SaveChanges();

        var currentUser = await _userResolverService.GetUser();
        _logger.Info("Пользователь {Email} изменил статью с индификатором {Id} заголовком {Title}", currentUser?.Email, model.Id, article?.Title);
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