using MyBlog.Models.Users;

namespace MyBlog.Services.ContextServices.Interface;

public interface IUserResolverService
{
    string GetUserId();

    Task<User> GetUser();
}
