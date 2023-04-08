using AutoMapper;
using BissnesLibrary.ContextServices.Interface;
using BissnesLibrary.ControllerServices.Interface;
using Contracts.Models.Articles;
using Contracts.Models.Tegs;
using Contracts.ViewModels.Articles;
using DataLibrary.Data.Repository;
using DataLibrary.Data.UoW;
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
        var model = new AllArticlesViewModel(GetAllArticles(), currentUser);       
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

// Коротко как работает создание статей:
//    ⠀⠀⠀ ⡠⠊⣦⠀⠀⠀⠀⠀⠀⠀⠀⢴⠑⢄⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⢀⣠⠔⢋⡤⢫⡳⡀⠀⠀⠀⠀⠀⠀⢀⠞⡽⢤⡙⠢⣄⡀⠀⠀⠀⠀
//⠀⠀⠀⢎⣩⢤⠚⠁⠀⠀⠱⡝⢄⠀⠀⠀⠀⣠⢋⠞⠀⠀⠈⠓⡤⣍⣱⠀⠀⠀
//⠀⠀⠀⠀⠉⢣⠳⡀⠀⠀⠀⠘⢎⢣⡀⢀⡔⡱⠃⠀⠀⠀⢀⠞⡔⠉⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠳⡙⣄⠀⢀⡠⢞⣳⣱⢎⣞⡳⢄⡀⠀⢠⢋⠞⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠙⣌⣖⡩⠖⠉⢀⠏⡾⡀⠉⠲⢍⣲⢣⠋⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠈⢮⠣⡀⠀⢸⡸⣇⡇⠀⢀⠜⡵⠁⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠑⠬⣓⣒⡇⣉⣒⣚⡥⠊⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⠞⡴⢣⠳⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⢋⠞⠀⠀⠱⡙⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠀⠀⣀⡴⣡⠋⠀⠀⠀⠀⠙⣌⢦⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⢀⡔⢉⣒⡇⠀⠀⠀⠀⠀⠀⠸⣒⡉⠣⡀⠀⠀⠀⠀⠀⠀⠀
//⠀⠀⠀⠀⠀⠀⠀⠸⢄⣚⠎⠀⠀⠀⠀⠀⠀⠀⠀⠱⣑⡠⠏⠀⠀⠀⠀⠀⠀⠀
    public async Task CreateArticle(Article article, List<Guid>? tegsCurrent)
    {        
        var currentUser = await _userResolverService.GetUser();
        article.UserId = currentUser?.Id;
        ArticleRepository?.CreateArticle(article);
        _unitOfWork.SaveChanges();
        //Иначе в API модель статьи не попадает в контекст и не добавляются теги.
        //Анекдот в том, что в основном проекте такой проблемы нет и костыли не требуются, скорее всего в контекст появляется на стадии View модель.
        var crutch = ArticleRepository?.GetArticleById(article.Id);
        TegRepository?.AddTegInArticles(crutch, tegsCurrent);
        _unitOfWork.SaveChanges();
        _logger.Info("Пользователь {Email} создал статью с заголовком {Title}", currentUser?.Email, article.Title);
    }

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

    public async Task UpdateArticle(Guid id, string? title, string? content, List<Guid>? tegsCurrent)
    {
        var article = ArticleRepository?.GetArticleById(id);
        article!.Content = content;
        article.Title = title;
        ArticleRepository?.UpdateArticle(article);
        TegRepository?.UpdateTegsInArticles(article, tegsCurrent!);
        _unitOfWork.SaveChanges();

        var currentUser = await _userResolverService.GetUser();
        _logger.Info("Пользователь {Email} изменил статью с индификатором {Id} заголовком {Title}", currentUser?.Email, id, article?.Title);
    }

    public async Task<List<Article>> GetArticleByUser(string userId)
    {
        var user = await _userResolverService.GetUserById(userId);

        if (user != null)
            return ArticleRepository?.GetArticleByUser(user)!;
        return null!;
    }

    public Article GetArticleById(Guid id)
    {
        return ArticleRepository?.GetArticleById(id)!;
    }

    public List<Article> GetAllArticles()
    {
        var allArticles = ArticleRepository!.GetAllArticle();
        return allArticles;
    }

}