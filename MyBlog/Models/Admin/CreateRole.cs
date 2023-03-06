using Microsoft.AspNetCore.Identity;

namespace MyBlog.Models.Admin
{
    public class CreateRole: IdentityRole
    {      
        public string? Description { get; set; }
    }
}
