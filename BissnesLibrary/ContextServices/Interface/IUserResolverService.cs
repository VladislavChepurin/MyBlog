using Contracts.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace BissnesLibrary.ContextServices.Interface;

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
