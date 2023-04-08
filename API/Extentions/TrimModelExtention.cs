using Contracts.Models.Articles;
using Contracts.Models.Comments;

namespace API.Extentions;

public static class TrimModelExtention
{
    public static List<Article> TrimModelAtrticle (this List<Article> articles)
    {
        return articles.Select(a =>
              new Article
              {
                  Id = a.Id,
                  Title = a.Title,
                  Content = a.Content,
                  Created = a.Created,
                  Updated = a.Updated,
                  UserId = a.UserId,
                  CountView = a.CountView
              }
          ).ToList();
    }

    public static List<Comment> TrimModelComment(this List<Comment> comments)
    {
        return comments.Select(c =>
              new Comment
              {
                  Id = c.Id,
                  Created = c.Created,
                  Updated = c.Updated,
                  Content = c.Content,
                  UserId = c.UserId,
                  ArticleId = c.ArticleId
              }
          ).ToList();
    }
}
