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
using MyBlog.ViewModels.Devises;

namespace MyBlog.Controllers.Articles;

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


    [HttpPost]
    [Route("/[controller]/[action]")]
    public ActionResult Create(string content)
    {
        var teg = new Teg()
        {
            Content = content
        };
        var repository = _unitOfWork.GetRepository<Teg>() as TegRepository;
        repository?.CreateTeg(
            new Teg()
            {
                Id = Guid.NewGuid(),
                Content = content
            });
        _unitOfWork.SaveChanges();
        return View();
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public ActionResult Update(TegViewModel model)
    {
        var teg = _mapper.Map<Teg>(model);
        var repository = _unitOfWork.GetRepository<Teg>() as TegRepository;
        repository?.UpdateTeg(teg);
        _unitOfWork.SaveChanges();
        return View();
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
