using Microsoft.AspNetCore.Identity;
using MyBlog.Models.Users;

namespace MyBlog.Services.ContextServices.Interface;

public interface IUserResolverService
{
    string GetUserId();

    Task<User> GetUser();

    Task<User> GetUserById(string id);

    Task<IdentityResult> UpdateUserAction(User user);

    Task<IList<string>> GetUserRoles(User user);

    Task AddRolesUser(User user, IEnumerable<string> roles);

    Task RemoveRolesUser(User user, IEnumerable<string> roles);

    Task DeleteUser(User user);
}
