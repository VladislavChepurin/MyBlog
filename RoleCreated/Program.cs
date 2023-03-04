using Microsoft.AspNetCore.Identity;
using MyBlog.Models.Users;
using System.Data;

namespace RoleCreated
{
    internal class Program
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        List<string> roles = new List<string>() {
            "Administrator",
            "Moderator",
            "User"
        };



        static void Main(string[] args)
        {
            
        }


        public async Task CreateInitRoles(User user)
        {
            if (_roleManager.Roles.ToList().Count == 0)
            {
                foreach (var role in roles)
                {
                    IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(role));
                    if (result.Succeeded)
                    {
                        Console.WriteLine($"Создана роль {role}");
                    }
                }
            }
        }





    }
}