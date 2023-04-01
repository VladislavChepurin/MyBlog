using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Contracts.Models.Users;
using BissnesLibrary.ControllerServices.Interface;
using Contracts.ViewModels.Articles;

namespace MyBlog.Controllers;

[Authorize]
public class ArticleController : Controller
{
    private readonly IArticleService _articleService;
    public ArticleController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> Index()
    {
        var view = await _articleService.GetModelIndex();
        return View(view);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> View(Guid id)
    {            
        var view = await _articleService.GetArticleView(id);
        return View(view);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> Create()
    {
        var view = await _articleService.GetAddArticleView();
        return View(view);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPost]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> Create(AddArticleViewModel model, List<Guid> tegsCurrent)
    {       
        if (ModelState.IsValid)
        {
            await _articleService.CreateArticle(model, tegsCurrent);
            return RedirectToAction("Index");
        }
        var view = await _articleService.GetAddArticleView(model);
        return View(view);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> Update(Guid id)
    {
        var view = await _articleService.UpdateArticle(id);
        return View(view);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPost]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> Update(ArticleUpdateViewModel model, List<Guid> tegsCurrent)
    {      
        if (ModelState.IsValid)
        {
            await _articleService.UpdateArticle(model, tegsCurrent); 
            return RedirectToAction("Index");
        }
        var view = await _articleService.UpdateArticle(model.Id);
        return View(view);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpGet]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _articleService.DeleteArticle(id);
        return RedirectToAction("Index");
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPost]
    [Route("/[controller]/[action]")]
    public IActionResult ArticleByUser(User user)
    {
        var view = _articleService.GetArticleByUser(user);
        return View(view);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPost]
    [Route("/[controller]/[action]")]
    public IActionResult ArticleById(Guid id)
    {
        var view = _articleService.GetArticleById(id);
        return View(view);
    }
}