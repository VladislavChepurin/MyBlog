using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Data.Repository;
using MyBlog.Data.UoW;
using MyBlog.Models.Articles;
using MyBlog.Models.Comments;
using MyBlog.Models.Users;
using MyBlog.ViewModels.Articles;
using MyBlog.ViewModels.Comments;
using NLog;
using NLog.Web;

namespace MyBlog.Controllers;

[Authorize]
public class CommentController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly CommentRepository? CommentRepository;
    private readonly ArticleRepository? ArticleRepository;
    private readonly Logger logger;

    public CommentController(UserManager<User> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;      
        CommentRepository = _unitOfWork.GetRepository<Comment>() as CommentRepository;
        ArticleRepository = _unitOfWork.GetRepository<Article>() as ArticleRepository;
        logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
    }

    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> Index()
    {
        var model = new CommentViewModel(CommentRepository!.GetAllComment());
        var user = User;
        var currentUser = await _userManager.GetUserAsync(user);
        model.CurrentUser = currentUser?.Id;
        logger.Info("Пользователь {Email} открыл страницу со списком статей", currentUser?.Email);
        return View(model);
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public async Task<ActionResult> Create(ArticleViewModel model)
    {
        var user = User;
        var currentUser = await _userManager.GetUserAsync(user);

        if (ModelState.IsValid)
        {           
            var comment = new Comment(model, currentUser!);
            CommentRepository?.CreateComment(comment);
            _unitOfWork.SaveChanges();
            return RedirectToAction("View", "Article", new { id = model.Article!.Id });
        }
        var article = ArticleRepository?.GetArticleById(model.Article!.Id);
        var articleView = new ArticleViewModel(article, currentUser!);       
        return View("~/Views/Article/View.cshtml", articleView);
    }

    [HttpGet]
    [Route("/[controller]/[action]")]
    public ActionResult Update(Guid id)
    {
        var comment = CommentRepository?.GetCommentById(id);
        var view = new CommentUpdateViewModel(comment!);
        return View(view);
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public ActionResult Update(CommentUpdateViewModel model)
    {
        var comment = CommentRepository?.GetCommentById(model.Id);
        if (ModelState.IsValid)
        {
            comment!.Content = model.Content;
            CommentRepository?.UpdateComment(comment);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }
        var view = new CommentUpdateViewModel(comment!);
        return View(view);
    }

    [HttpGet]
    [Route("/[controller]/[action]")]
    public ActionResult Delete(Guid id)
    {      
        var comment = CommentRepository?.GetCommentById(id);
        CommentRepository?.DeleteComment(comment);
        _unitOfWork.SaveChanges();
        return RedirectToAction("Index");
    }
}
