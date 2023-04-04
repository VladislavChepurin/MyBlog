using BissnesLibrary.ControllerServices.Interface;
using Contracts.ApiModels.Article;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]

public class ArticleController : ControllerBase
{
    private readonly IArticleService _articleService;
    public ArticleController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    [HttpPost]
    [Route("CreateArticle")]
    public async Task<ActionResult> Create(CreateArticleApi create)
    {
        if (ModelState.IsValid)
        {
           // await _articleService.CreateArticle(create.Title, create.Content, create.Tegs);
            return StatusCode(200, $"New article added title: {create.Title}");
        }
        return StatusCode(403, "The server cannot or will not process the request due to something that is perceived to be a client error");
    }


}
