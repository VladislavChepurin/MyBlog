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
        Create(comment);
    }

    public void UpdateComment(Comment comment)
    {
        Update(comment);
    }

    public void DeleteComment(Comment comment)
    {
        Delete(comment);
    }
    public List<Comment> GetCommentByArticle(Article article)
    {

        var articles = Set.AsEnumerable().Where(x => x?.ArticleId == article.Id);
        return articles.ToList();
    }

    public List<Comment> GetCommentByUser(User target)
    {
        var articles = Set.AsEnumerable().Where(x => x?.User?.Id == target.Id);
        return articles.ToList();
    }

    public Comment GetCommentById(Guid id)
    {
        var articles = Set.AsEnumerable().Where(x => x?.Id == id).FirstOrDefault();
        return articles;
    }

    public List<Comment> GetAllComment()
    {       
        return Comments.Include(c => c.User).Include(c => c.Article).ToList();
    }
}
