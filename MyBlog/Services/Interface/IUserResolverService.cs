using MyBlog.Models.Users;

namespace MyBlog.Services.Interface
{
    public interface IUserResolverService
    {
        string GetUserId();

        Task<User> GetUser();
    }
}
