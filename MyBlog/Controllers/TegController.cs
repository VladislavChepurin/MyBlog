using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Data.Repository;
using MyBlog.Data.UoW;
using MyBlog.Models.Tegs;
using MyBlog.ViewModels.Tegs;

namespace MyBlog.Controllers;

[Authorize]
public class TegController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly TegRepository? TegRepository;

    public TegController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        TegRepository = _unitOfWork.GetRepository<Teg>() as TegRepository;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/[controller]/[action]")]
    public ActionResult Index()
    {       
        var tegs = TegRepository?.GetAllTeg();
        return View(new TegViewModel(tegs));
    }

    [HttpGet]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Update(Guid id)
    {
        var teg = TegRepository?.GetTegById(id);
        if (teg != null)
        {
            var tegView = new TegUpdateViewModel(teg);
            return View(tegView);
        }
        return RedirectToAction("Index");      
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public ActionResult Update(string contentTeg, Guid id)
    {  
        var teg = TegRepository?.GetTegById(id);
        if (teg != null)
        {
            teg.Content = contentTeg;
            TegRepository?.UpdateTeg(teg);
        }
        _unitOfWork.SaveChanges();
        return RedirectToAction("Index");
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Create() => View();

    [HttpPost]
    [Route("/[controller]/[action]")]
    public ActionResult Create(string content)
    {
        if (!string.IsNullOrEmpty(content))
        {
            TegRepository?.CreateTeg(
                new Teg()
                {                 
                    Content = content
                });
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(content);       
    }

    [HttpGet]
    [Route("/[controller]/[action]")]
    public ActionResult Delete(Guid id)
    {   
        var teg = TegRepository?.GetTegById(id);
        TegRepository?.DeleteTeg(teg);
        _unitOfWork.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("/[controller]/[action]")]
    public ActionResult GetAllTeg()
    {       
        var result = TegRepository?.GetAllTeg();
        return View(result);
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public ActionResult GetTegId(Guid id)
    {       
        var result = TegRepository?.GetTegById(id);
        return View(result);
    }
}