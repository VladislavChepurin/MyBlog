using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Data.Repository;
using MyBlog.Data.UoW;
using MyBlog.Models.Comments;
using MyBlog.Models.Users;
using MyBlog.ViewModels.Comments;

namespace MyBlog.Controllers;

[Authorize]
public class CommentController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly CommentRepository? CommentRepository;

    public CommentController(UserManager<User> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;      
        CommentRepository = _unitOfWork.GetRepository<Comment>() as CommentRepository;
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
        return View(model);
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public async Task<ActionResult> Create(Guid idArticle, string content)
    {
        var user = User;
        var currentUser = await _userManager.GetUserAsync(user);
        var comment = new Comment()
        {
            Content = content,
            UserId = currentUser?.Id,
            ArticleId = idArticle
        };
        CommentRepository?.CreateComment(comment);
        _unitOfWork.SaveChanges();
        return RedirectToAction("View", "Article", new { id = idArticle });            
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
