using FluentValidation;
using MyBlog.Models.Articles;

namespace MyBlog.Validation;

public class ArticleValidator : AbstractValidator<Article>
{
    public ArticleValidator()
    {
        RuleFor(x => x.Title).Must(x => x == null || x.Length >= 3).NotEmpty();
        RuleFor(x => x.Content).Must(x => x == null || x.Length >= 20).NotEmpty();
    }
}
