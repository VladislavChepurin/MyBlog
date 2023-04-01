using BissnesLibrary.ControllerServices.Interface;
using Contracts.ViewModels.Tegs;
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
    [HttpPost]
    [Route("CreateTegApi")]
    public async Task<IActionResult> Create([FromBody] AddTegViewModel model)
    {
        if (ModelState.IsValid)
        {
            //await _tegService.CreateTeg(model);
            return StatusCode(201, $"Teg {model.Content} added!");
        }
        return StatusCode(403, "The server cannot or will not process the request due to something that is perceived to be a client error");
    }

    /// <summary>
    /// Update teg
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("UpdateTegApi")]
    public async Task<IActionResult> Update([FromBody] TegUpdateViewModel model)
    {
        if (ModelState.IsValid)
        {
            //await _tegService.UpdateTeg(model);
            return StatusCode(200, $"Teg {model.Id} updated!");
        }
        return StatusCode(403, "The server cannot or will not process the request due to something that is perceived to be a client error");
    }

    /// <summary>
    /// Delete teg by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Route("DeleteTegApi")]
    public async Task<IActionResult> Delete(Guid id)
    {
        //await _tegService.DeleteTeg(id);
        return StatusCode(200, $"Teg {id} deleted!");
    }


    /// <summary>
    /// Get all teg in collection
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("GetAllTegApi")]
    public IActionResult GetAllTeg()
    {
        return StatusCode(200/*, _tegService.GetAllTeg()*/);
    }

    /// <summary>
    /// Get teg by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("GetTegByIdApi")]
    public IActionResult GetTegId(Guid id)
    {
        //var teg = _tegService.GetTegId(id);
        //if (teg != null)
        //{
            return StatusCode(200/*, _tegService.GetTegId(id)*/);
        //}
        //return StatusCode(404, "No such API end point");
    }
}
