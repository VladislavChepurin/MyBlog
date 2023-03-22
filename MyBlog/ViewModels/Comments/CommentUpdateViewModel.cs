using MyBlog.Models.Comments;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.ViewModels.Comments;

public class CommentUpdateViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Отсутсвует текст комментария")]
    [DataType(DataType.Text)]
    [Display(Name = "Комментарий", Prompt = "Введите текст комментария, минимум 5 символов")]
    [StringLength(200, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 5)]
    public string? Content { get; set; }

    public CommentUpdateViewModel(Comment comment)
    {
        Id = comment.Id;
        Content = comment.Content;
    }
    public CommentUpdateViewModel()
    {
    }
}
