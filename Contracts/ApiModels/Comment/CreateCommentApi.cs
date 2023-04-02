using System.ComponentModel.DataAnnotations;

namespace Contracts.ApiModels.Comment;

public class CreateCommentApi
{
    [Required(ErrorMessage = "Отсутствует индитификатор")]
    public Guid ArticleId { get; set; }

    [Required(ErrorMessage = "Отсутсвует текст комментария")]
    [DataType(DataType.Text)]
    [StringLength(200, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 5)]
    public string? Content { get; set; }
}
