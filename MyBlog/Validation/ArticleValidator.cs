using FluentValidation;
using MyBlog.Models.Articles;

namespace MyBlog.Validation
{
    public class ArticleValidator : AbstractValidator<Article>
    {
        public ArticleValidator()
        {
            
        }
    }
}
