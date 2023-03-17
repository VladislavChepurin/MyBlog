using MyBlog.Data.Repository;
using MyBlog.Models.Articles;
using MyBlog.Models.Tegs;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.ViewModels.Articles
{
    public class UpdateArticleViewModel
    {
        public Guid Id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Заголовок", Prompt = "Введите заголовок, минимум 3 символа")]
        public string? Title { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Контент", Prompt = "Введите текст статьи, минимум 20 символов")]
        public string? Content { get; set; }

        public List<Teg>? UserTegs { get; set; }

        public IList<Teg>? TegList { get; set; }
        public UpdateArticleViewModel()
        {
            UserTegs = new List<Teg>();
            TegList = new List<Teg>();
        }

        public UpdateArticleViewModel(Article article, TegRepository? tegRepository)
        {
            Id = article.Id;
            Title = article.Title;
            Content = article.Content;
            UserTegs = article.Tegs;
            TegList = tegRepository?.GetAllTeg();
        }
    }
}
