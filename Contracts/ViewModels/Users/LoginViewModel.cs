using System.ComponentModel.DataAnnotations;

namespace Contracts.ViewModels.Users;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email", Prompt = "Введите email")]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль", Prompt = "Введите пароль")]
    public string? Password { get; set; }

    [Display(Name = "Запомнить?")]
    public bool RememberMe { get; set; }

    public string? ReturnUrl { get; set; }
}
