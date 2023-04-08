using BissnesLibrary.ControllerServices.Interface;
using Contracts.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]

public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IEditService _editService;

    public AccountController(IAccountService accountService, IEditService editService)
    {
        _accountService = accountService;
        _editService = editService;
    }

    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> LoginAsync(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var succeededLogin = await _accountService.IsLoggedIn(model);

            if (succeededLogin)
            {
                return StatusCode(200, $"Loggin succeed");
            }
            else
            {
                return StatusCode(400, $"Loggin not succeed");
            }
        }
        return StatusCode(403, "The server cannot or will not process the request due to something that is perceived to be a client error");
    }

    [Authorize]
    [Route("Logout")]
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _accountService.Logout();
        return StatusCode(200, $"Logout succeed");
    }

    [Authorize]
    [Route("EditUser")]
    [HttpPost]
    public async Task<IActionResult> Edit(UserEditViewModel userEdit)
    {
        if (ModelState.IsValid)
        {
            var result = await _editService.UpdateUser(userEdit);
            if (result)
            {                
                return StatusCode(200, $"Edit succeed");
            }
            else
            {
                return StatusCode(400, $"Edit failture");
            }         
        }
        return StatusCode(400, $"Edit failture, model is not valid");
    }
}
