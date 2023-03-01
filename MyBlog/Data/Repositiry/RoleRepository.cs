using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;
using MyBlog.Models.Users;

namespace MyBlog.Data.Repositiry
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ILogger<RoleRepository> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        List<string> roles = new List<string>() {
            "Administrator",
            "Moderator",
            "User"
        };

        public RoleRepository(ILogger<RoleRepository> logger, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task CreateInitRoles()
        {
            if (_roleManager.Roles.ToList().Count == 0)
            {
                foreach (var role in roles)
                {
                    IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(role));
                    if (result.Succeeded)
                    {
                        _logger.LogInformation($"Создана роль {role}");
                    }
                }
            }            
        }


        // метод заглушка после написания бека удалить!
        // Для регистрации временно
        // удалить!------------------------------------------------------------------------------------------------------------------

        public async Task AssignRoles(User user, string codeRole)
        {       
            MD5 MD5Hash = MD5.Create(); //создаем объект для работы с MD5
            byte[] inputBytes = Encoding.ASCII.GetBytes(codeRole); //преобразуем строку в массив байтов
            byte[] hash = MD5Hash.ComputeHash(inputBytes); //получаем хэш в виде массива байтов
            string output = Convert.ToHexString(hash); //преобразуем хэш из массива в строку, состоящую из шестнадцатеричных символов в верхнем регистре        


            switch (output)
            {
                case "DF8DF10A6F943A15E742206AB665496D": // Role: Administrator
                    if ((await _userManager.AddToRoleAsync(user, roles[0])).Succeeded)
                    {
                        _logger.LogInformation($"Пользователю {user.UserName} присвоена роль {roles[0]}");
                    }
                    break;

                case "8BAFB3241D049CEC114B4A93BE26A00E": // Role: Moderator
                    if ((await _userManager.AddToRoleAsync(user, roles[1])).Succeeded)
                    {
                        _logger.LogInformation($"Пользователю {user.UserName} присвоена роль {roles[1]}");
                    }
                    break;

                default:                                 // Role: User
                    if ((await _userManager.AddToRoleAsync(user, roles[2])).Succeeded)
                    {
                        _logger.LogInformation($"Пользователю {user.UserName} присвоена роль {roles[2]}");
                    }
                    break;
            }
        }       
    }
}
