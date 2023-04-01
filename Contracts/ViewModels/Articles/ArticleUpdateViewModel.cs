using Contracts.Models.Articles;
using Contracts.Models.Tegs;
using System.ComponentModel.DataAnnotations;

namespace Contracts.ViewModels.Articles;

public class ArticleUpdateViewModel
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Отсутсвует текст заголовка")]
    [DataType(DataType.Text)]
    [Display(Name = "Заголовок", Prompt = "Введите заголовок, минимум 5 символа")]
    [StringLength(50, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 5)]
    public string? Title { get; set; }

    [Required(ErrorMessage = "Отсутсвует текст статьи")]
    [DataType(DataType.Text)]
    [Display(Name = "Контент", Prompt = "Введите текст статьи, минимум 20 символов")]
    [StringLength(2500, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 20)]
    public string? Content { get; set; }

    public List<Teg>? UserTegs { get; set; }

    public IList<Teg>? TegList { get; set; }
    public ArticleUpdateViewModel()
    {
        UserTegs = new List<Teg>();
        TegList = new List<Teg>();
    }

    public ArticleUpdateViewModel(Article article)
    {
        Id = article.Id;
        Title = article.Title;
        Content = article.Content;
        UserTegs = article.Tegs;       
    }
}
