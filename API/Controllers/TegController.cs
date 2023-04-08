using BissnesLibrary.ControllerServices.Interface;
using Contracts.ApiModels.Tegs;
using Contracts.Models.Tegs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]

public class TegController : ControllerBase
{

    private readonly ITegService _tegService;

    public TegController(ITegService tegService)
    {
        _tegService = tegService;
    }

    /// <summary>
    /// Создание нового тега через Api
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost]
    [Route("CreateTeg")]
    public async Task<IActionResult> Create([FromBody] AddTegApi model)
    {
        if (ModelState.IsValid)
        {
            await _tegService.CreateTeg(model.Content!);
            return StatusCode(201, $"Teg {model.Content} added!");
        }
        return StatusCode(403, "The server cannot or will not process the request due to something that is perceived to be a client error");
    }

    /// <summary>
    /// Изменение тега через Api
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [Authorize(Roles = "Administrator, Moderator")]
    [HttpPut]
    [Route("UpdateTeg")]
    public async Task<IActionResult> Update([FromBody] UpdateTegApi model)
    {
        if (ModelState.IsValid)
        {
            await _tegService.UpdateTeg(model.Id, model.Content!);
            return StatusCode(200, $"Teg {model.Id} updated!");
        }
        return StatusCode(403, "The server cannot or will not process the request due to something that is perceived to be a client error");
    }

    /// <summary>
    /// Удаление тега через Api
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Roles = "Administrator, Moderator")]
    [HttpDelete]
    [Route("DeleteTeg")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _tegService.DeleteTeg(id);
        return StatusCode(200, $"Teg {id} deleted!");
    }

    /// <summary>
    /// Получение всех тегов
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet]
    [Route("GetAllTeg")]
    public IActionResult GetAllTeg()
    {
        var model = _tegService.GetAllTeg().Select(t => new Teg
        {
            Id = t.Id,
            Content = t.Content
        });        
        return StatusCode(200, model);
    }

    /// <summary>
    /// Получение тега по его Api
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet]
    [Route("GetTegById")]
    public IActionResult GetTegId(Guid id)
    {
        var teg = _tegService.GetTegId(id);
        if (teg != null)
        {
            return StatusCode(200, _tegService.GetTegId(id));
        }
        return StatusCode(404, "No such API end point");
    }

    /// <summary>
    /// Получение списка тегов к определенной статье через id статьи
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet]
    [Route("GetTegByArticleId")]
    public IActionResult GetTegArticle(Guid id)
    {
        var teg = _tegService.GetTegByArticle(id);
        if (teg != null)
        {
            return StatusCode(200, teg);
        }
        return StatusCode(404, "No such API end point");
    }
}
