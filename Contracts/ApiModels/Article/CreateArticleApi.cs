using System.ComponentModel.DataAnnotations;

namespace Contracts.ApiModels.Article;

public class CreateArticleApi
{
    [Required(ErrorMessage = "Отсутсвует текст заголовка")]
    [DataType(DataType.Text)]
    [StringLength(50, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 5)]
    public string? Title { get; set; }

    [Required(ErrorMessage = "Отсутсвует текст статьи")]
    [DataType(DataType.Text)]
    [StringLength(2500, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 20)]
    public string? Content { get; set; }

    public List<Guid>? Tegs { get; set; } 
}
