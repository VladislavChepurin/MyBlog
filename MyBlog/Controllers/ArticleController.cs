using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Data.Repository;
using MyBlog.Data.UoW;
using MyBlog.Models.Articles;
using MyBlog.Models.Comments;
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
    private readonly IValidator<Article> _validator;
    private readonly ArticleRepository? ArticleRepository;
    private readonly TegRepository? TegRepository;

    public ArticleController(IMapper mapper, UserManager<User> userManager, IUnitOfWork unitOfWork, IValidator<Article> validator)
    {
        _mapper = mapper;
        _userManager = userManager;
        _unitOfWork = unitOfWork;
        _validator = validator;
        ArticleRepository = _unitOfWork.GetRepository<Article>() as ArticleRepository;
        TegRepository = _unitOfWork.GetRepository<Teg>() as TegRepository;
    }

    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> Index()
    {
        if (ArticleRepository != null)
        {
            var model = new AllArticlesViewModel(ArticleRepository.GetAllArticle());
            var user = User;
            var currentUser = await _userManager.GetUserAsync(user);
            model.CurrentUser = currentUser.Id;
            return View(model);
        }
        return NotFound();
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/[controller]/[action]")]
    public ActionResult Create()
    {       
        var tegs = TegRepository?.GetAllTeg();
        return View(new AddArticleViewModel() { Tegs = tegs});
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/[controller]/[action]")]
    public ActionResult View(Guid id)
    {            
        var article = ArticleRepository?.GetArticleById(id);
        if (article != null) {
            ++article.CountView;
            ArticleRepository?.Update(article);
            _unitOfWork.SaveChanges();
        }
        var articleView = new ArticleViewModel(article);
        return View(articleView);
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public async Task<ActionResult> Create(AddArticleViewModel model, List<Guid> tegsCurrent)
    {         
        var article = _mapper.Map<Article>(model);   
        ValidationResult result = await _validator.ValidateAsync(article);
        if (result.IsValid)
        {        
            article.Created = DateTime.Now;
            var user = User;
            var currentUser = await _userManager.GetUserAsync(user);
            article.UserId = currentUser.Id;

            ArticleRepository?.CreateArticle(article);
            TegRepository?.AddTegInArticles(article, tegsCurrent);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }               
        var tegs = TegRepository?.GetAllTeg();
        var view = new AddArticleViewModel(model, tegs);        
        return View("CreateArticle", view);
    }

    [HttpGet]
    [Route("/[controller]/[action]")]
    public ActionResult Update(Guid id)
    {           
        var article = ArticleRepository?.GetArticleById(id);
        if (article != null)
        {
            var view = new UpdateArticleViewModel(article, TegRepository);
            return View(view);
        }     
        return NotFound();
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public ActionResult Update(UpdateArticleViewModel model, List<Guid> tegsCurrent)
    {       
        var article = ArticleRepository?.GetArticleById(model.Id);
        if (article != null)
        {
            article.Updated = DateTime.Now;
            article.Content = model.Content;
            article.Title = model.Title;
            ArticleRepository?.Update(article);
            TegRepository?.UpdateTegsInArticles(article, tegsCurrent);
            _unitOfWork.SaveChanges();
        }   
        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("/[controller]/[action]")]
    public ActionResult Delete(Guid id)
    {       
        var article = ArticleRepository?.GetArticleById(id);
        if (article != null)        
        {
            ArticleRepository?.DeleteArticle(article);
            _unitOfWork.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public ActionResult ArticleByUser(User user)
    {        
        var article = ArticleRepository?.GetArticleByUser(user);
        return View(article);
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public ActionResult ArticleById(Guid id)
    {       
        var article = ArticleRepository?.GetArticleById(id);
        return View(article);
    }
}