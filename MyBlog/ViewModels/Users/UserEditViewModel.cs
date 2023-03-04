using MyBlog.Models.Users;

namespace MyBlog.ViewModels.Users;

public class UserEditViewModel
{
    public string? UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Company { get; set; }
    public string? Email { get; set; }

    public UserEditViewModel()
    {

    }

    public UserEditViewModel(User result)
    {
        UserId = result.Id;
        FirstName = result.FirstName;
        LastName = result.LastName;
        Company = result.Company;
        Email = result.Email;
    }
}
