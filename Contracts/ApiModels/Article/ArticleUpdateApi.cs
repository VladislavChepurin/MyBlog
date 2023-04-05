using Contracts.Models.Articles;
using Contracts.Models.Tegs;
using System.ComponentModel.DataAnnotations;

namespace Contracts.ViewModels.Articles;

public class ArticleUpdateApi
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Отсутсвует текст заголовка")]
    [DataType(DataType.Text)]
    [StringLength(50, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 5)]
    public string? Title { get; set; }

    [Required(ErrorMessage = "Отсутсвует текст статьи")]
    [DataType(DataType.Text)]   
    [StringLength(2500, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 20)]
    public string? Content { get; set; }

    public List<Guid>? TegsCurrent { get; set; }
    
}
