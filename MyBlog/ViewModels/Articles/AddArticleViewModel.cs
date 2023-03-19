using MyBlog.Models.Tegs;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.ViewModels.Articles
{
    public class AddArticleViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Заголовок", Prompt = "Введите заголовок, минимум 3 символа")]
        public string? Title { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Контент", Prompt = "Введите текст статьи, минимум 20 символов")]
        [StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 5)]
        public string? Content { get; set; }

        public List<Teg>? Tegs { get; set; }

        public AddArticleViewModel(AddArticleViewModel model, List<Teg>? tegs)
        {
            Title = model.Title;
            Content = model.Content;
            Tegs = tegs;        
        }

        public AddArticleViewModel()
        {
            Tegs = new List<Teg>();
        }
    }
}
