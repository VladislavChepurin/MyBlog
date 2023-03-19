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
        if (CommentRepository != null)
        {
            var model = new CommentViewModel(CommentRepository.GetAllComment());
            var user = User;
            var currentUser = await _userManager.GetUserAsync(user);
            model.CurrentUser = currentUser.Id;
            return View(model);
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public async Task<ActionResult> Create(Guid idArticle, string content)
    {
        if (content != null) 
        {
            var user = User;
            var currentUser = await _userManager.GetUserAsync(user);

            var comment = new Comment()
            {
                Content = content,
                Created = DateTime.Now,
                UserId = currentUser.Id,
                ArticleId = idArticle
            };
            CommentRepository?.CreateComment(comment);
            _unitOfWork.SaveChanges();
        }
        return RedirectToAction("View", "Article", new { id = idArticle });            
    }


    [HttpGet]
    [Route("/[controller]/[action]")]
    public ActionResult Update(Guid id)
    {
        var comment = CommentRepository?.GetCommentById(id);
        if (comment != null)
        {
            var view = new CommentUpdateViewModel(comment);
            return View(view);
        }
        return NotFound();
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public ActionResult Update(CommentUpdateViewModel model)
    {
        var comment = CommentRepository?.GetCommentById(model.Id);
        if (comment != null)
        {
            comment.Updated = DateTime.Now;
            comment.Content = model.Content;
            CommentRepository?.UpdateComment(comment);
        }
        _unitOfWork.SaveChanges();
        return RedirectToAction("Index");
    }


    [HttpGet]
    [Route("/[controller]/[action]")]
    public ActionResult Delete(Guid id)
    {      
        var comment = CommentRepository?.GetCommentById(id);
        if (comment != null)
        {
            CommentRepository?.DeleteComment(comment);
            _unitOfWork.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}
