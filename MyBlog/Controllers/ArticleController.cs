using AutoMapper;
using BissnesLibrary.ContextServices.Interface;
using BissnesLibrary.ControllerServices.Interface;
using Contracts.Models.Articles;
using Contracts.ViewModels.Articles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyBlog.Controllers;

[Authorize]
[ApiExplorerSettings(IgnoreApi = true)]
public class ArticleController : Controller
{
    private readonly IArticleService _articleService;
    private readonly IMapper _mapper;


    public ArticleController(IArticleService articleService, IMapper mapper)
    {
        _articleService = articleService;
        _mapper = mapper;
    }


    [Route("/[controller]/[action]")]
    public async Task<IActionResult> Index()
    {
        var view = await _articleService.GetModelIndex();
        return View(view);
    }

    [Route("/[controller]/[action]")]
    public async Task<IActionResult> View(Guid id)
    {            
        var view = await _articleService.GetArticleView(id);
        return View(view);
    }

    [Route("/[controller]/[action]")]
    public async Task<IActionResult> Create()
    {
        var view = await _articleService.GetAddArticleView();
        return View(view);
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> Create(AddArticleViewModel model, List<Guid>? tegsCurrent)
    {       
        if (ModelState.IsValid)
        {
            var article = _mapper.Map<Article>(model);
            await _articleService.CreateArticle(article, tegsCurrent);
            return RedirectToAction("Index");
        }
        var view = await _articleService.GetAddArticleView(model);
        return View(view);
    }

    [HttpGet]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> Update(Guid id)
    {
        var view = await _articleService.UpdateArticle(id);
        return View(view);
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> Update(ArticleUpdateViewModel model, List<Guid> tegsCurrent)
    {      
        if (ModelState.IsValid)
        {           
            await _articleService.UpdateArticle(model.Id, model.Title, model.Content, tegsCurrent); 
            return RedirectToAction("Index");
        }
        var view = await _articleService.UpdateArticle(model.Id);
        return View(view);
    }

    [HttpGet]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _articleService.DeleteArticle(id);
        return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> ArticleByUserId(string userId)
    {        
        var view = await _articleService.GetArticleByUser(userId);
        return View(view);
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public IActionResult ArticleById(Guid id)
    {
        var view = _articleService.GetArticleById(id);
        return View(view);
    }
}