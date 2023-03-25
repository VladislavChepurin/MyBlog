using Microsoft.EntityFrameworkCore;
using MyBlog.Data.Repositiry;
using MyBlog.Models.Articles;
using MyBlog.Models.Comments;
using MyBlog.Models.Users;

namespace MyBlog.Data.Repository;

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

    public List<Comment> GetAllComment()
    {       
        return Comments.Include(c => c.User).Include(c => c.Article).ToList();
    }     
}
