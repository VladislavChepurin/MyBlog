using Contracts.Models.Users;
using Contracts.ViewModels.Users;

namespace BissnesLibrary.Extentions;

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
