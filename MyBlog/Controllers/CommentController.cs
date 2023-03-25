﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Services.ControllerServices.Interface;
using MyBlog.ViewModels.Articles;
using MyBlog.ViewModels.Comments;

namespace MyBlog.Controllers;

[Authorize]
public class CommentController : Controller
{
    private readonly ICommentService _commentService;
    private readonly IArticleService _articleService;

    public CommentController(ICommentService commentService, IArticleService articleService)
    {
        _commentService = commentService;
        _articleService = articleService;    
    }

    [Authorize]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> Index()
    {
        var view = await _commentService.GetModelIndex();
        return View(view);
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public async Task<ActionResult> Create(ArticleViewModel model)
    {     
        if (ModelState.IsValid)
        {
            await _commentService.CreateComment(model);
            return RedirectToAction("View", "Article", new { id = model.Article!.Id });
        }
        var view = _articleService.GetArticleView(model.Article!.Id);
        return View("~/Views/Article/View.cshtml", view);
    }

    [HttpGet]
    [Route("/[controller]/[action]")]
    public async Task<ActionResult> Update(Guid id)
    {
        var view = await _commentService.UpdateComment(id);
        return View(view);
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public async Task<ActionResult> Update(CommentUpdateViewModel model)
    {       
        if (ModelState.IsValid)
        {
            await _commentService.UpdateComment(model);
            return RedirectToAction("Index");
        }
        var view = await _commentService.UpdateComment(model.Id);
        return View(view);
    }

    [HttpGet]
    [Route("/[controller]/[action]")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _commentService.Delete(id);
        return RedirectToAction("Index");
    }
}
