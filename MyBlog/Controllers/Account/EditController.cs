using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Services.ControllerServices.Interface;
using MyBlog.ViewModels.Users;

namespace MyBlog.Controllers.Account;

public class EditController : Controller
{
    private readonly IEditService _editService;

    public EditController(IEditService editService)
    {
        _editService = editService;
    }

    /// <summary>
    /// Редактирование пользователя
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpPost]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> EditUser(string userId)
    {
        var view = await _editService.GetEditUserModel(userId);
        return View(view);
    }

    /// <summary>
    /// Обновление пользователя
    /// </summary>
    /// <param name="userEdit"></param>
    /// <returns></returns>
    [Authorize]
    [Route("Update")]
    [HttpPost]
    public async Task<IActionResult> Update(UserEditViewModel userEdit)
    {
        if (ModelState.IsValid)
        {
            var result = await _editService.UpdateUser(userEdit);
            if (result)
            {
                var view = await _editService.GetUserPageModel(userEdit.UserId!);
                return View("UserPage", view);
            }
            else
            {
                return RedirectToAction("EditUser");
            }
        }
        else
        {
            ModelState.AddModelError("", "Некорректные данные");
            return RedirectToAction("EditUser", new { userId = userEdit.UserId });
        }
    }
}
