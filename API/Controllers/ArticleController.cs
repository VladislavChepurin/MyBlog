using AutoMapper;
using BissnesLibrary.ControllerServices.Interface;
using Contracts.ApiModels.Article;
using Contracts.Models.Articles;
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


}
