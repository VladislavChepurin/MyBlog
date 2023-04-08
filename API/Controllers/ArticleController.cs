using API.Extentions;
using AutoMapper;
using BissnesLibrary.ControllerServices.Interface;
using Contracts.ApiModels.Article;
using Contracts.Models.Articles;
using Contracts.ViewModels.Articles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ArticleController : ControllerBase
{
    private readonly IArticleService _articleService;
    private readonly IMapper _mapper;

    public ArticleController(IArticleService articleService, IMapper mapper)
    {
        _articleService = articleService;
        _mapper = mapper;
    }

    /// <summary>
    /// Создание новой статьи через Api
    /// </summary>
    /// <param name="create"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("CreateArticle")]
    public async Task<ActionResult> Create(CreateArticleApi create)
    {
        if (ModelState.IsValid)
        {
            var article = _mapper.Map<Article>(create);
            await _articleService.CreateArticle(article, create.Tegs);
            return StatusCode(200, $"New article added title: {create.Title}");
        }
        return StatusCode(403, "The server cannot or will not process the request due to something that is perceived to be a client error");
    }

    /// <summary>
    /// Изменение статьи через Api
    /// </summary>
    /// <param name="model"></param>
    /// <param name="tegsCurrent"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("UpdateArticle")]
    public async Task<IActionResult> Update(ArticleUpdateApi model)
    {
        if (ModelState.IsValid)
        {
            await _articleService.UpdateArticle(model.Id, model.Title, model.Content, model.TegsCurrent);
            return StatusCode(200, $"Article id: {model.Id} updated");
        }
        return StatusCode(403, "The server cannot or will not process the request due to something that is perceived to be a client error");
    }

    /// <summary>
    /// Удаление статьи через Api
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("DeleteArticle")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _articleService.DeleteArticle(id);
        return StatusCode(200, $"Article {id} deleted!");
    }

    /// <summary>
    /// Получение статьи через id статьи
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("GetArticleById")]
    public IActionResult GetArticle(Guid id)
    {
        var model = _articleService.GetArticleById(id);      

        if (model != null)
        {
            model.Comments = null;
            model.User = null;
            model.Tegs = null;           
            return StatusCode(200, model);
        }
        return StatusCode(404, "No such API end point");
    }

    /// <summary>
    /// Получение всех статей пользователя через его id
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("ArticleByUserId")]
    public async Task<IActionResult> ArticleByUserId(string userId)
    {
        var model = await _articleService.GetArticleByUser(userId);
        if (model != null)
        {
            return StatusCode(200, model.TrimModelAtrticle());
        }    
        return StatusCode(404, "No such API end point");
    }

    /// <summary>
    /// Получение всех статей
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("GetAllArticle")]
    public IActionResult GetAllArticle()
    {
        var model = _articleService.GetAllArticles();
        return StatusCode(200, model.TrimModelAtrticle());
    }
}
