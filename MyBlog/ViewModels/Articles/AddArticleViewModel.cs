using MyBlog.Models.Tegs;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.ViewModels.Articles
{
    public class AddArticleViewModel
    {
      

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Заголовок", Prompt = "Введите заголовок")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Контент", Prompt = "Введите текст статьи")]
        public string? Content { get; set; }

        public List<Teg>? Tegs { get; set; }

        public IList<Guid>? TegList { get; set; }
        public AddArticleViewModel()
        {
            TegList = new List<Guid>();
        }

        //public AddArticleViewModel(List<Teg>? tegs)
        //{
        //    Tegs = tegs;
        //}
    }
}
