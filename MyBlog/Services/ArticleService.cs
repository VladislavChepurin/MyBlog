﻿using AutoMapper;
using MyBlog.Data.Repository;
using MyBlog.Data.UoW;
using MyBlog.Models.Articles;
using MyBlog.Models.Tegs;
using MyBlog.Models.Users;
using MyBlog.ViewModels.Articles;
using NLog;
using NLog.Web;

namespace MyBlog.Services;

public class ArticleService : IArticleService
{     
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ArticleRepository? ArticleRepository;
    private readonly TegRepository? TegRepository;
    private readonly Logger logger;
    private readonly IUserResolverService _userResolverService;

    public ArticleService(IMapper mapper, IUnitOfWork unitOfWork, IUserResolverService userResolverService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        ArticleRepository = _unitOfWork.GetRepository<Article>() as ArticleRepository;
        TegRepository = _unitOfWork.GetRepository<Teg>() as TegRepository;
        logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
        _userResolverService = userResolverService;
    }

    public async Task<AllArticlesViewModel> GetModelIndex()
    {
        var model = new AllArticlesViewModel(ArticleRepository!.GetAllArticle());
        var currentUser = await _userResolverService.GetUser();
        model.CurrentUser = currentUser?.Id;
        logger.Info("Пользователь {Email} открыл страницу со списком статей", currentUser?.Email);
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
        logger.Info("Пользователь {Email} открыл статью с заголовком {Title}", currentUser?.Email, article.Title);
        return model;
    }

    public AddArticleViewModel GetAddArticleView(AddArticleViewModel model)
    {
        var article = _mapper.Map<Article>(model);
        var tegs = TegRepository?.GetAllTeg();
        return new AddArticleViewModel(tegs, article);
    }

    public AddArticleViewModel GetAddArticleView()
    {      
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
        logger.Info("Пользователь {Email} создал статью с заголовком {Title}", currentUser?.Email, article.Title);
    }

    public async Task DeleteArticle(Guid id)
    {
        var currentUser = await _userResolverService.GetUser();
        var article = ArticleRepository?.GetArticleById(id);
        ArticleRepository?.DeleteArticle(article);
        _unitOfWork.SaveChanges();
        logger.Info("Пользователь {Email} удалил статью с заголовком {Title}", currentUser?.Email, article?.Title);
    }

    public ArticleUpdateViewModel UpdateArticle(Guid id)
    {
        var article = ArticleRepository?.GetArticleById(id);
        return new ArticleUpdateViewModel(article!, TegRepository);
    }

    public void UpdateArticle(ArticleUpdateViewModel model, List<Guid> tegsCurrent)
    {
        var article = ArticleRepository?.GetArticleById(model.Id);
        article!.Content = model.Content;
        article.Title = model.Title;
        ArticleRepository?.UpdateArticle(article);
        TegRepository?.UpdateTegsInArticles(article, tegsCurrent);
        _unitOfWork.SaveChanges();     
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