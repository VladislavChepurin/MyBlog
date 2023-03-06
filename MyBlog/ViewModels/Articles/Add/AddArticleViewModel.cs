using MyBlog.Models.Articles;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.ViewModels.Articles.Add
{
    public class AddArticleViewModel
    {
        public List<Teg>? Tegs { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Заголовок", Prompt = "Введите заголовок")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Контент", Prompt = "Введите текст статьи")]
        public string? Content { get; set; }
    }
}
