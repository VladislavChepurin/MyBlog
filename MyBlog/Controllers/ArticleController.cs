using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Data.Repository;
using MyBlog.Data.UoW;
using MyBlog.Models.Articles;
using MyBlog.Models.Tegs;
using MyBlog.Models.Users;
using MyBlog.Services;
using MyBlog.ViewModels.Articles;

namespace MyBlog.Controllers;

[Authorize]
public class ArticleController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IArticleService _articleService;
    private readonly ArticleRepository? ArticleRepository;
    private readonly TegRepository? TegRepository;

    public ArticleController(IUnitOfWork unitOfWork, IArticleService articleService)
    {
        _unitOfWork = unitOfWork;
        _articleService = articleService;
        ArticleRepository = _unitOfWork.GetRepository<Article>() as ArticleRepository;
        TegRepository = _unitOfWork.GetRepository<Teg>() as TegRepository;
    }

    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> Index()
    {
        var view = await _articleService.GetModelIndex();
        return View(view);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> View(Guid id)
    {            
        var view = await _articleService.GetArticleView(id);
        return View(view);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/[controller]/[action]")]
    public IActionResult Create()
    {
        var view = _articleService.GetAddArticleView();
        return View(view);
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> Create(AddArticleViewModel model, List<Guid> tegsCurrent)
    {       
        if (ModelState.IsValid)
        {
            await _articleService.CreateArticle(model, tegsCurrent);
            return RedirectToAction("Index");
        }
        var view = _articleService.GetAddArticleView(model);
        return View(view);
    }

    [HttpGet]
    [Route("/[controller]/[action]")]
    public IActionResult Update(Guid id)
    {           
        var article = ArticleRepository?.GetArticleById(id);
        var view = new ArticleUpdateViewModel(article!, TegRepository);
        return View(view);
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public IActionResult Update(ArticleUpdateViewModel model, List<Guid> tegsCurrent)
    {
       var article = ArticleRepository?.GetArticleById(model.Id);

        if (ModelState.IsValid)
        {
            article!.Content = model.Content;
            article.Title = model.Title;
            ArticleRepository?.UpdateArticle(article);
            TegRepository?.UpdateTegsInArticles(article, tegsCurrent);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }
        var view = new ArticleUpdateViewModel(article!, TegRepository);
        return View(view);
    }

    [HttpGet]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _articleService.DeleteArticle(id);
        return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public IActionResult ArticleByUser(User user)
    {        
        var article = ArticleRepository?.GetArticleByUser(user);
        return View(article);
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public IActionResult ArticleById(Guid id)
    {       
        var article = ArticleRepository?.GetArticleById(id);
        return View(article);
    }
}