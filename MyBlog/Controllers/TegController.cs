using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Controllers.Account;
using MyBlog.Data;
using MyBlog.Data.Repository;
using MyBlog.Data.UoW;
using MyBlog.Models.Tegs;
using MyBlog.Models.Users;
using MyBlog.ViewModels.Articles;
using MyBlog.ViewModels.Tegs;

namespace MyBlog.Controllers;

[Authorize]
public class TegController : Controller
{
    private readonly ILogger<RegisterUserController> _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public TegController(ILogger<RegisterUserController> logger, IMapper mapper, ApplicationDbContext context, UserManager<User> userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
        _unitOfWork = unitOfWork;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/[controller]/[action]")]
    public ActionResult Index()
    {
        var repository = _unitOfWork.GetRepository<Teg>() as TegRepository;
        var tegs = repository?.GetAllTeg();
        return View(new TegViewModel(tegs));
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Create() => View();

    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult UpdateTeg(Guid id)
    {
        var repository = _unitOfWork.GetRepository<Teg>() as TegRepository;
        var teg = repository?.GetTegById(id);
        if (teg != null)
        {
            var tegView = new TegUpdateViewModel(teg);
            return View(tegView);
        }
        return RedirectToAction("Index");      
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public ActionResult Create(string content)
    {
        if (!string.IsNullOrEmpty(content))
        {
            var repository = _unitOfWork.GetRepository<Teg>() as TegRepository;
            repository?.CreateTeg(
                new Teg()
                {                 
                    Content = content
                });
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(content);       
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public ActionResult UpdateTeg(string contentTeg, Guid idTeg)
    {
        //var teg = _mapper.Map<Teg>(model);

        var repository = _unitOfWork.GetRepository<Teg>() as TegRepository;
        var teg = repository?.GetTegById(idTeg);
        if (teg != null)
        {
            teg.Content = contentTeg;
            repository?.UpdateTeg(teg);
        }      
        _unitOfWork.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public ActionResult Delete(ArticleViewModel model)
    {
        var teg = _mapper.Map<Teg>(model);
        var repository = _unitOfWork.GetRepository<Teg>() as TegRepository;
        repository?.DeleteTeg(teg);
        _unitOfWork.SaveChanges();
        return View();
    }

    [HttpGet]
    [Route("/[controller]/[action]")]
    public ActionResult GetAllTeg()
    {
        var repository = _unitOfWork.GetRepository<Teg>() as TegRepository;
        var result = repository?.GetAllTeg();
        return View(result);
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public ActionResult GetTegId(Guid id)
    {
        var repository = _unitOfWork.GetRepository<Teg>() as TegRepository;
        var result = repository?.GetTegById(id);
        return View(result);
    }
}
