using Microsoft.EntityFrameworkCore;
using DataLibrary.Data.Repositiry;
using Contracts.Models.Articles;
using Contracts.Models.Comments;
using Contracts.Models.Users;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataLibrary.Data.Repository;

#nullable disable
public class CommentRepository : Repository<Comment>
{
    public CommentRepository(ApplicationDbContext db) : base(db)
    {
        
    }

    public void CreateComment(Comment comment)
    {
        comment.Created = DateTime.Now;
        Create(comment);
    }

    public void UpdateComment(Comment comment)
    {
        comment.Updated = DateTime.Now;
        Update(comment);
    }

    public void DeleteComment(Comment comment)
    {
        Delete(comment);
    }
    public List<Comment> GetCommentByArticle(Article article)
    {

        var comments = Set.AsEnumerable().Where(x => x?.ArticleId == article.Id);
        return comments.ToList();
    }

    public List<Comment> GetCommentByUser(User target)
    {
        var comments = Set.AsEnumerable().Where(x => x?.User?.Id == target.Id);
        return comments.ToList();
    }

    public Comment GetCommentById(Guid id)
    {
        var comment = Set.AsEnumerable().FirstOrDefault(x => x?.Id == id);
        return comment;
    }

    public Comment GetCommentByIdApi(Guid id)
    {
        return Set.AsEnumerable().Select(u => new Comment
        {
            Id = u.Id,
            Created = u.Created,
            Updated = u.Updated,
            Content = u.Content,
            UserId = u.UserId,
            ArticleId = u.ArticleId
        }).FirstOrDefault(x => x?.Id == id);
    }

    public List<Comment> GetAllComment()
    {       
        return Comments.Include(c => c.User).Include(c => c.Article).ToList();
    }

    public List<Comment> GetAllCommentApi()
    {
        return Set.AsEnumerable().Select(u => new Comment {
            Id = u.Id,
            Created = u.Created,
            Updated = u.Updated,
            Content = u.Content,
            UserId = u.UserId,
            ArticleId = u.ArticleId
        }).ToList();
    }
}
