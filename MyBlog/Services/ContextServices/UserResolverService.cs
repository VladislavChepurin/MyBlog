using Microsoft.AspNetCore.Identity;
using MyBlog.Models.Users;
using MyBlog.Services.ContextServices.Interface;

namespace MyBlog.Services.ContextServices;

public class UserResolverService : IUserResolverService
{
    private readonly IHttpContextAccessor _httpContext;
    private readonly UserManager<User> _userManager;

    public UserResolverService(IHttpContextAccessor httpContext, UserManager<User> userManager)
    {
        _httpContext = httpContext;
        _userManager = userManager;
    }

    /// <summary>
    /// Получение Id текущего пользователя
    /// </summary>
    /// <returns></returns>
    public string GetUserId()
    {
        var userId = _userManager.GetUserId(_httpContext.HttpContext!.User);
        return userId!;
    }

    /// <summary>
    /// Получение текущего пользователя
    /// </summary>
    /// <returns></returns>
    public async Task<User> GetUser()
    {
        var user = await _userManager.GetUserAsync(_httpContext.HttpContext!.User);
        return user!;
    }

    /// <summary>
    /// Получение пользователя по Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<User> GetUserById(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        return user!;
    }

    /// <summary>
    /// Обновление пользователя
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<IdentityResult> UpdateUserAction(User user) =>
        await _userManager.UpdateAsync(user);

    /// <summary>
    /// Получение ролей пользователя
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<IList<string>> GetUserRoles(User user) =>
        await _userManager.GetRolesAsync(user);

    /// <summary>
    /// Добавление ролей пользователю
    /// </summary>
    /// <param name="user"></param>
    /// <param name="roles"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task AddRolesUser(User user, IEnumerable<string> roles) =>
        await _userManager.AddToRolesAsync(user, roles);

    /// <summary>
    /// Удаление ролей пользователю
    /// </summary>
    /// <param name="user"></param>
    /// <param name="roles"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task RemoveRolesUser(User user, IEnumerable<string> roles) =>
        await _userManager.RemoveFromRolesAsync(user, roles);

    /// <summary>
    /// Удаление пользователя
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task DeleteUser(User user) =>
        await _userManager.DeleteAsync(user);

}
