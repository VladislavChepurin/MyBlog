using Contracts.Models.Tegs;
using System.ComponentModel.DataAnnotations;

namespace Contracts.ViewModels.Tegs;

public class TegUpdateViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Отсутсвует текст тега")]
    [DataType(DataType.Text)]
    [Display(Name = "Тег", Prompt = "Введите тег, минимум 3 символа")]
    [StringLength(20, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 3)]
    public string? Content { get; set; }

    public TegUpdateViewModel(Teg teg)
    {
        Id = teg.Id;
        Content = teg.Content;
    }
    public TegUpdateViewModel()
    {

    }    
}
