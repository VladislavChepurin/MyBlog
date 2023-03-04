using MyBlog.Models.Articles;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.ViewModels.Devises;

public class ArticleViewModel
{
    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    [DataType(DataType.Text)]
    public string? Title { get; set; }

    [Required(ErrorMessage = "Поле обязательно для заполнения")]
    [DataType(DataType.Text)]   
    public string? Content { get; set; }    
    public bool Private { get; set; }
    public string? UserId { get; set; }
    public List<Comment>? Comments { get; set; }
    public List<Teg>? Tegs { get; set; }

    public ArticleViewModel()
    {

    }

    public ArticleViewModel(Article result)
    {
        Title = result.Title;
        Content = result.Content;
        Private = result.Private;
        UserId = result.UserId;
        Tegs = result.Tegs;      
    }
}
