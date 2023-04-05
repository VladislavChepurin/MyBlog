using BissnesLibrary.ControllerServices.Interface;
using Contracts.ApiModels.Comment;
using Contracts.Models.Comments;
using Contracts.ViewModels.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    /// <summary>
    /// Create comment
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("CreateComment")]
    public async Task<ActionResult> Create(CreateCommentApi create)
    {   
        if (ModelState.IsValid)
        {            
            await _commentService.CreateComment(create.ArticleId, create.Content!);
            return StatusCode(200, $"New comment added in article id: {create.ArticleId}");
        }
        return StatusCode(403, "The server cannot or will not process the request due to something that is perceived to be a client error");
    }

    /// <summary>
    /// Update comment
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("UpdateComment")]
    public async Task<ActionResult> Update(CommentUpdateViewModel model)
    {
        if (ModelState.IsValid)
        {
            var articleId = await _commentService.UpdateComment(model);
            return StatusCode(200, $"Comment updated in article id: {articleId}");
        }       
        return StatusCode(403, "The server cannot or will not process the request due to something that is perceived to be a client error");
    }

    /// <summary>
    /// Delete comment
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("DeleteComment")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _commentService.Delete(id);
        return StatusCode(200, $"Comment {id} deleted!");
    }

    /// <summary>
    /// Get all comment
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("GetAllComment")]
    public IActionResult GetAllComment()
    {
        var model = _commentService.GetAllComment()
            .Select(u => new Comment
            {
                Id = u.Id,
                Created = u.Created,
                Updated = u.Updated,
                Content = u.Content,
                UserId = u.UserId,
                ArticleId = u.ArticleId
            });
        return StatusCode(200, model);
    }

    /// <summary>
    /// Get comment by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("GetCommentById")]
    public IActionResult GetCommentId(Guid id)
    {
        var comment = _commentService.GetCommentById(id);
        comment.Article = null;
        comment.User = null;

        if (comment != null)
        {
            return StatusCode(200, comment);
        }
        return StatusCode(404, "No such API end point");
    }

    [HttpGet]
    [Route("GetСommentByArticle")]
    public IActionResult GetTegId(Guid id)
    {
        var comment = _commentService.GetCommentByArticle(id)
             .Select(u => new Comment
             {
                 Id = u.Id,
                 Created = u.Created,
                 Updated = u.Updated,
                 Content = u.Content,
                 UserId = u.UserId,
                 ArticleId = u.ArticleId
             });
        if (comment != null)
        {
            return StatusCode(200, comment);
        }
        return StatusCode(404, "No such API end point");
    }
}