using Microsoft.AspNetCore.Identity;

namespace MyBlog.Models.Users;

public class User : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public string GetFullName()
    {
        return FirstName + " " + LastName;
    }

}
