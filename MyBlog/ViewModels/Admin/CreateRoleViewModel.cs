using System.ComponentModel.DataAnnotations;

namespace MyBlog.ViewModels.Admin
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Поле название роли обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Название роли", Prompt = "Введите название роли")]
        public string? NameRole {get; set;}
               
        [DataType(DataType.Text)]
        [Display(Name = "Описание роли", Prompt = "Введите описание роли")]
        public string? Description { get; set;}
    }
}
