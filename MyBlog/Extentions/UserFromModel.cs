using MyBlog.Models.Users;
using MyBlog.ViewModels.Users;

namespace MyBlog.Extentions;

public static class UserFromModel
{
    public static User Convert(this User user, UserEditViewModel usereditvm)
    {
        user.LastName = usereditvm.LastName;
        user.FirstName = usereditvm.FirstName;
        user.Email = usereditvm.Email;            
        return user;
    }
}
