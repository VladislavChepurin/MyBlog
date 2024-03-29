﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BissnesLibrary.ControllerServices.Interface;
using Contracts.ViewModels.Articles;
using Contracts.ViewModels.Comments;

namespace MyBlog.Controllers;

[Authorize]
[ApiExplorerSettings(IgnoreApi = true)]
public class CommentController : Controller
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [Route("/[controller]/[action]")]
    public async Task<IActionResult> Index()
    {
        var view = await _commentService.GetModelIndex();
        return View(view);
    }

    [Route("/[controller]/[action]")]
    public async Task<IActionResult> View(Guid id)
    {
        var view = await _commentService.GetArticleView(id);
        return View("~/Views/Article/View.cshtml", view);
    }

    [HttpPost]
    [Route("/[controller]/[action]")]
    public async Task<ActionResult> Create(ArticleViewModel model)
    {     
        if (ModelState.IsValid)
        {
            await _commentService.CreateComment(model.Article!.Id, model.CommentContent!);
            return RedirectToAction("View", new { id = model.Article!.Id });
        }
        var view = await _commentService.GetArticleView(model.Article!.Id);
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
