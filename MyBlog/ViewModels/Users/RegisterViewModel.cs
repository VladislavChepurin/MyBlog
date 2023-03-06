using System.ComponentModel.DataAnnotations;

namespace MyBlog.ViewModels.Users;

public class RegisterViewModel
{    
    [Required(ErrorMessage = "Поле Имя обязательно для заполнения")]
    [DataType(DataType.Text)]
    [Display(Name = "Имя", Prompt = "Введите имя")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Поле Фамилия обязательно для заполнения")]
    [DataType(DataType.Text)]
    [Display(Name = "Фамилия", Prompt = "Введите фамилию")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "Поле Email обязательно для заполнения")]
    [EmailAddress]
    [Display(Name = "Email", Prompt = "Введите адрес электронной почты")]
    public string? EmailReg { get; set; }

    [Required(ErrorMessage = "Поле Пароль обязательно для заполнения")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль", Prompt = "Введите пароль")]
    [StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 5)]
    public string? PasswordReg { get; set; }

    [Required(ErrorMessage = "Обязательно подтвердите пароль")]
    [Compare("PasswordReg", ErrorMessage = "Пароли не совпадают")]
    [DataType(DataType.Password)]
    [Display(Name = "Подтвердить пароль", Prompt = "Введите пароль повторно")]
    public string? PasswordConfirm { get; set; }

    [Required(ErrorMessage = "Поле Никнейм обязательно для заполнения")]
    [DataType(DataType.Text)]
    [Display(Name = "Никнейм", Prompt = "Введите никнейм")]
    public string? Login { get; set; }
}
