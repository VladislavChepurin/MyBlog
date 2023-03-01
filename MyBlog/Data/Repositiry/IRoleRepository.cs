using MyBlog.Models.Users;

namespace MyBlog.Data.Repositiry
{
    public interface IRoleRepository
    {
        Task CreateInitRoles();
        Task AssignRoles(User user, string codeRole);

    }
}
