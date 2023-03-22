using Microsoft.AspNetCore.Identity;
using MyBlog.Models.Users;

namespace MyBlog.Services
{
    public class UserResolverService : IUserResolverService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<User> _userManager;

        public UserResolverService(IHttpContextAccessor httpContext, UserManager<User> userManager)
        {
            _httpContext = httpContext;
            _userManager = userManager;
        }

        public string GetUserId()
        {
            var userId = _userManager.GetUserId(_httpContext.HttpContext!.User);
            return userId!;
        }

        public async Task<User> GetUser()
        {
            var user = await _userManager.GetUserAsync(_httpContext.HttpContext!.User);
            return user!;
        }
    }
}
