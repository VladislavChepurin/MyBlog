using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Services.ControllerServices.Interface;
using MyBlog.ViewModels.Tegs;

namespace MyBlog.Controllers;

[Authorize]
public class TegController : Controller
{
    private readonly ITegService _tegService;

    public TegController(ITegService tegService)
    {
        _tegService = tegService;
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> Index()
    {       
        var view = await _tegService.GetModelIndex(); 
        return View(view);
    }

    [HttpGet]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Update(Guid id)
    {
        var view = await _tegService.UpdateTeg(id);
        return View(view);
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> Update(TegUpdateViewModel model)
    {      
        if (ModelState.IsValid)
        {
            await _tegService.UpdateTeg(model);
            return RedirectToAction("Index");
        }
        var view = _tegService.UpdateTeg(model.Id);
        return View(view);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet]
    public IActionResult Create() => View(new AddTegViewModel());

    [HttpPost]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> Create(AddTegViewModel model)
    {
        if (ModelState.IsValid)
        {
            await _tegService.CreateTeg(model);
            return RedirectToAction("Index");
        }
        return View(new AddTegViewModel(model.Content!));       
    }

    [HttpGet]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _tegService.DeleteTeg(id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("/[controller]/[action]")]
    public ActionResult GetAllTeg()
    {
        var view = _tegService.GetAllTeg();
        return View(view);
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public ActionResult GetTegId(Guid id)
    {       
        var view = _tegService.GetTegId(id);
        return View(view);
    }
}