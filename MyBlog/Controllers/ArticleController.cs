using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Controllers.Account;
using MyBlog.Data;
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
    private readonly ILogger<RegisterUserController> _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<Article> _validator;


    public ArticleController(ILogger<RegisterUserController> logger, IMapper mapper, ApplicationDbContext context, UserManager<User> userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork, IValidator<Article> validator)
    {
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/[controller]/[action]")]
    public IActionResult Index()
    {
        if (_unitOfWork.GetRepository<Article>() is ArticleRepository repository)
        {
            var model = new AllArticlesViewModel(repository.GetAllArticle());
            return View(model);
        }
        return NotFound();
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/[controller]/[action]")]
    public ActionResult CreateArticle()
    {
        var repository = _unitOfWork.GetRepository<Teg>() as TegRepository;
        var tegs = repository?.GetAllTeg();
        return View(new AddArticleViewModel() { Tegs = tegs});
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/[controller]/[action]")]
    public ActionResult ViewArticle(Guid id)
    {
        var articleRepository = _unitOfWork.GetRepository<Article>() as ArticleRepository;       
        var article = articleRepository?.GetArticleById(id);       
        var articleView = new ArticleViewModel(article);
        return View(articleView);

    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public async Task<ActionResult> Create(AddArticleViewModel model, List<Guid> tegsCurrent)
    {         
        var article = _mapper.Map<Article>(model);
        var tegRepository = _unitOfWork.GetRepository<Teg>() as TegRepository;
        var articleRepository = _unitOfWork.GetRepository<Article>() as ArticleRepository;

        ValidationResult result = await _validator.ValidateAsync(article);
        if (result.IsValid)
        {        
            article.Created = DateTime.Now;
            var user = User;
            var currentUser = await _userManager.GetUserAsync(user);
            article.UserId = currentUser.Id;
            
            articleRepository?.CreateArticle(article);          
            tegRepository?.AddTegInArticles(article, tegsCurrent);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }
               
        var tegs = tegRepository?.GetAllTeg();
        var view = new AddArticleViewModel() 
        { 
            Title = model.Title,
            Content = model.Content,
            Tegs = tegs };
        return View("CreateArticle", view);
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public ActionResult Update(Article article)
    {
        var tegRepository = _unitOfWork.GetRepository<Teg>() as TegRepository;
        var articleRepository = _unitOfWork.GetRepository<Article>() as ArticleRepository;

        var tegs = tegRepository?.GetAllTeg();
        var view = new AddArticleViewModel()
        {

        };
        return View("UpdateArticle", view);
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public ActionResult Delete(Guid id)
    {
        var repository = _unitOfWork.GetRepository<Article>() as ArticleRepository;        
        var article = repository?.GetArticleById(id);
        if (article != null)        
        {
            repository?.DeleteArticle(article);
            _unitOfWork.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public ActionResult ArticleByUser(User user)
    {
        var repository = _unitOfWork.GetRepository<Article>() as ArticleRepository;
        var article = repository?.GetArticleByUser(user);
        return View(article);
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public ActionResult ArticleById(Guid id)
    {
        var repository = _unitOfWork.GetRepository<Article>() as ArticleRepository;
        var article = repository?.GetArticleById(id);
        return View(article);
    }
}
