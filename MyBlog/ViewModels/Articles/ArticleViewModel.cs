using MyBlog.Models.Articles;
using MyBlog.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MyBlog.ViewModels.Articles
{
    public class ArticleViewModel
    {
        public Article? Article { get; set; }
        public string? CurrentUser { get; set; }

        [Required(ErrorMessage = "Отсутсвует текст комментария")]
        [DataType(DataType.Text)]
        [Display(Name = "Комментарий", Prompt = "Введите текст комментария, минимум 5 символов")]
        [StringLength(200, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 5)]
        public string? CommentContent { get; set; }

        public ArticleViewModel() 
        {
        }

        public ArticleViewModel(Article? article, User user)
        {
            Article = article;
            article!.Comments = Article?.Comments?.OrderBy(d => d.Created).ToList();
            CurrentUser = user?.Id;
        }
    }
}
