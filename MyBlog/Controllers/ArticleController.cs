using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Data.Repository;
using MyBlog.Data.UoW;
using MyBlog.Models.Articles;
using MyBlog.Models.Tegs;
using MyBlog.Models.Users;
using MyBlog.ViewModels.Articles;

namespace MyBlog.Controllers;

[Authorize]
public class ArticleController : Controller
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ArticleRepository? ArticleRepository;
    private readonly TegRepository? TegRepository;

    public ArticleController(IMapper mapper, UserManager<User> userManager, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        ArticleRepository = _unitOfWork.GetRepository<Article>() as ArticleRepository;
        TegRepository = _unitOfWork.GetRepository<Teg>() as TegRepository;
    }

    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> Index()
    {
        var model = new AllArticlesViewModel(ArticleRepository!.GetAllArticle());
        var user = User;
        var currentUser = await _userManager.GetUserAsync(user);
        model.CurrentUser = currentUser?.Id;
        return View(model);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/[controller]/[action]")]
    public IActionResult Create()
    {       
        var tegs = TegRepository?.GetAllTeg();
        return View(new AddArticleViewModel() { Tegs = tegs});
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> View(Guid id)
    {            
        var article = ArticleRepository?.GetArticleById(id);

        ++article!.CountView;
        ArticleRepository?.Update(article);
        _unitOfWork.SaveChanges();

        var articleView = new ArticleViewModel(article);
        var user = User;
        var currentUser = await _userManager.GetUserAsync(user);
        articleView.CurrentUser = currentUser?.Id;
        return View(articleView);
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> Create(AddArticleViewModel model, List<Guid> tegsCurrent)
    {
        var article = _mapper.Map<Article>(model);

        if (ModelState.IsValid)
        {           
            var user = User;
            var currentUser = await _userManager.GetUserAsync(user);
            article.UserId = currentUser?.Id;

            ArticleRepository?.CreateArticle(article);
            TegRepository?.AddTegInArticles(article, tegsCurrent);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }
        var tegs = TegRepository?.GetAllTeg();
        var view = new AddArticleViewModel(model, tegs);
        return View("Create", view);
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
    public IActionResult Delete(Guid id)
    {       
        var article = ArticleRepository?.GetArticleById(id);
        ArticleRepository?.DeleteArticle(article);
        _unitOfWork.SaveChanges();
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