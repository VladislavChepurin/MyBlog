using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Extentions;
using MyBlog.Models.Users;
using MyBlog.ViewModels;
using MyBlog.ViewModels.Users;

namespace MyBlog.Controllers.Account;

public class EditController : Controller
{
    private readonly UserManager<User> _userManager;

    public EditController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    /// <summary>
    /// Редактирование пользователя
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [Route("EditUser")]
    [HttpPost]
    public async Task<IActionResult> EditUser(string userId)
    {
        User user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            return View("EditUser", new UserEditViewModel(user));
        }
        return NotFound();
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
            var user = await _userManager.FindByIdAsync(userEdit.UserId);
            user.Convert(userEdit);     
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                var model = new UserPageViewModel
                {
                    UserViewModel = new UserViewModel(user)
                };
                return View("User", model);
            }
            else
            {
                return RedirectToAction("EditUser");
            }
        }
        else
        {
            ModelState.AddModelError("", "Некорректные данные");
            return View("EditUser", userEdit);
        }
    }
}
