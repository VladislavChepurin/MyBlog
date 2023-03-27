using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Models.Users;
using MyBlog.Services.ControllerServices.Interface;

namespace MyBlog.Controllers.Account;

[Authorize(Roles = "Administrator")]
public class AdminController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<User> _userManager;
    private readonly IAdminService _adminService;

    public AdminController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IAdminService adminService)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _adminService = adminService;
    }

    public IActionResult Index() => View(_userManager.Users.ToList());

    public IActionResult Create() => View();

    public IActionResult RoleList() => View(_roleManager.Roles.ToList());

    [Route("Delete")]
    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        await _adminService.DeleteRole(id);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Create(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }
        return View(name);
    }

    [Route("Edit")]
    [HttpGet]
    public async Task<IActionResult> Edit(string userId)
    {
        var view = await _adminService.GetEditModel(userId);
        return View(view);
    }

    [Route("Edit")]
    [HttpPost]
    public async Task<IActionResult> Edit(string userId, List<string> roles)
    {
        await _adminService.EditRoleUser(userId, roles);
        return RedirectToAction("Index");
    }

    [Route("DeleteUser")]
    [HttpGet]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        await _adminService.DeleteUserAction(userId);
        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("/[controller]/[action]")]
    public async Task<IActionResult> UserPage(string userId)
    {
        var view = await _adminService.GetUserPageModel(userId);
        return View(view);
    }

}