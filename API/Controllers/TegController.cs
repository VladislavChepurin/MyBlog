using BissnesLibrary.ControllerServices.Interface;
using Contracts.ApiModels.Teg;
using Contracts.ViewModels.Tegs;
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
    /// Create new teg
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
    /// Update teg
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
    /// Delete teg by Id
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
    /// Get all teg in collection
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpGet]
    [Route("GetAllTeg")]
    public IActionResult GetAllTeg()
    {
        return StatusCode(200, _tegService.GetAllTegApi());
    }

    /// <summary>
    /// Get teg by Id
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
}
