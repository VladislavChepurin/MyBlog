using MyBlog.Models.Tegs;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.ViewModels.Articles
{
    public class AddArticleViewModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Заголовок", Prompt = "Введите заголовок, минимум 3 символа")]
        public string? Title { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Контент", Prompt = "Введите текст статьи, минимум 20 символов")]
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
