using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Controllers.Account;
using MyBlog.Data;
using MyBlog.Data.Repository;
using MyBlog.Data.UoW;
using MyBlog.Models.Articles;
using MyBlog.Models.Users;
using MyBlog.ViewModels.Articles;

namespace MyBlog.Controllers.Articles;

[Authorize]
public class ArticleController : Controller
{
    private readonly ILogger<RegisterUserController> _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public ArticleController(ILogger<RegisterUserController> logger, IMapper mapper, ApplicationDbContext context, UserManager<User> userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
        _unitOfWork = unitOfWork;
    }                    

    [HttpPost]
    [Route("/[controller]/[action]")]
    public async Task<ActionResult> CreateAsync(ArticleViewModel model)
    {
        var article = _mapper.Map<Article>(model);
        article.Id = Guid.NewGuid();
        article.Created = DateTime.Now;
        article.User = await _userManager.FindByIdAsync(article.UserId);
        var repository = _unitOfWork.GetRepository<Article>() as ArticleRepository;
        repository?.CreateArticle(article);
        _unitOfWork.SaveChanges();
        return View(article);
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public async Task<ActionResult> UpdateAsync(ArticleViewModel model)
    {
        var article = _mapper.Map<Article>(model);
        article.Updated = DateTime.Now;
        article.User = await _userManager.FindByIdAsync(article.UserId);
        var repository = _unitOfWork.GetRepository<Article>() as ArticleRepository;
        repository?.UpdateArticle(article);
        _unitOfWork.SaveChanges();
        return View(article);
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public ActionResult Delete(ArticleViewModel model)
    {
        var article = _mapper.Map<Article>(model);
        var repository = _unitOfWork.GetRepository<Article>() as ArticleRepository;
        repository?.DeleteArticle(article);
        _unitOfWork.SaveChanges();
        return View();
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
