using System.ComponentModel.DataAnnotations;

namespace MyBlog.ViewModels.Devises
{
    public class CreateNewArticle
    {
        [Required(ErrorMessage = "Поле Код обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Заводской номер", Prompt = "Введите заводской номер")]
        public string? SerialNumber { get; set; }

        [Required(ErrorMessage = "Поле Название устройства обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Название устройства", Prompt = "Введите название устройства")]
        public string? NameDevices { get; set; }

        [Required(ErrorMessage = "Поле Код партии обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Код партии", Prompt = "Введите код партии")]
        public string? ManufactureCode { get; set; }
    }
}
