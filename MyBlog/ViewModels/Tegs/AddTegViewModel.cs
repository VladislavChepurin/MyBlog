using System.ComponentModel.DataAnnotations;

namespace MyBlog.ViewModels.Tegs;

public class AddTegViewModel
{
    [Required(ErrorMessage = "Отсутсвует текст тега")]
    [DataType(DataType.Text)]
    [Display(Name = "Тег", Prompt = "Введите тег, минимум 3 символа")]
    [StringLength(20, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 3)]
    public string? Content { get; set; }

    public AddTegViewModel()
    {        
    }

    public AddTegViewModel(string content)
    {
        Content = content;
    }
}
