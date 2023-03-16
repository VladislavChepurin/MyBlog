using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Controllers.Account;
using MyBlog.Data;
using MyBlog.Data.Repository;
using MyBlog.Data.UoW;
using MyBlog.Models.Articles;
using MyBlog.Models.Comments;
using MyBlog.Models.Users;
using MyBlog.ViewModels.Articles;
using MyBlog.ViewModels.Comments;

namespace MyBlog.Controllers;

[Authorize]
public class CommentController : Controller
{
    private readonly ILogger<RegisterUserController> _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;


    public CommentController(ILogger<RegisterUserController> logger, IMapper mapper, ApplicationDbContext context, UserManager<User> userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
        _unitOfWork = unitOfWork;
    }

    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> Index()
    {
        if (_unitOfWork.GetRepository<Comment>() is CommentRepository repository)
        {
            var model = new CommentViewModel(repository.GetAllComment());
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
            var commentRepository = _unitOfWork.GetRepository<Comment>() as CommentRepository;    
            commentRepository?.CreateComment(comment);
            _unitOfWork.SaveChanges();
        }
        return RedirectToAction("ViewArticle", "Article", new { id = idArticle });            
    }


    [HttpPost]
    [Route("/[controller]/[action]")]
    public ActionResult Update(Guid id)
    {
        //var comment = _mapper.Map<Comment>(model);
        //comment.Updated = DateTime.Now;
        //var repository = _unitOfWork.GetRepository<Comment>() as CommentRepository;
        //repository?.UpdateComment(comment);
        //_unitOfWork.SaveChanges();
        return View();
    }

    [HttpGet]
    [Route("/[controller]/[action]")]
    public ActionResult Delete(Guid id)
    {        
        var repository = _unitOfWork.GetRepository<Comment>() as CommentRepository;
        var comment = repository?.GetCommentById(id);
        if (comment != null)
        {
            repository?.DeleteComment(comment);
            _unitOfWork.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public ActionResult ArticleByUser(Article article)
    {
        var repository = _unitOfWork.GetRepository<Comment>() as CommentRepository;
        var comment = repository?.GetCommentByArticle(article);
        return View(comment);
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public ActionResult ArticleByUserId(Guid id)
    {
        var repository = _unitOfWork.GetRepository<Comment>() as CommentRepository;
        var comment = repository?.GetCommentById(id);
        return View(comment);
    }
}
