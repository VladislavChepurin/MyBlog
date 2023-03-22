using MyBlog.Models.Users;

namespace MyBlog.Services
{
    public interface IUserResolverService
    {
        string GetUserId();

        Task<User> GetUser();
    }
}
